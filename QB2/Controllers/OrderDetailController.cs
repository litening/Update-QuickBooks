using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using PagedList;
using PagedList.Mvc;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using QB2.Models;
using QB2.Data;

namespace QB2.Controllers
{
    public class OrderDetailController : Controller
    {

        DataTable dtOrderDetail = new DataTable();
        DataTable dtOrderHeader = new DataTable();
        DataTable dtCatalogItem = new DataTable();

        // GET: /OrderDetail/
        public ActionResult Index(string sortOrder,  
                                  String SearchField,
                                  String SearchCondition,
                                  String SearchText,
                                  String Export,
                                  int? PageSize,
                                  int? page, 
                                  string command)
        {

            if (command == "Show All") {
                SearchField = null;
                SearchCondition = null;
                SearchText = null;
                Session["SearchField"] = null;
                Session["SearchCondition"] = null;
                Session["SearchText"] = null; } 
            else if (command == "Add New Record") { return RedirectToAction("Create"); } 
            else if (command == "Export") { Session["Export"] = Export; } 
            else if (command == "Search" | command == "Page Size") {
                if (!string.IsNullOrEmpty(SearchText)) {
                    Session["SearchField"] = SearchField;
                    Session["SearchCondition"] = SearchCondition;
                    Session["SearchText"] = SearchText; }
                } 
            if (command == "Page Size") { Session["PageSize"] = PageSize; }

            ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "Order Detail Id" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["OrderDetailIdSortParm"] = sortOrder == "OrderDetailId_asc" ? "OrderDetailId_desc" : "OrderDetailId_asc";
            ViewData["OrderIdSortParm"] = sortOrder == "OrderId_asc" ? "OrderId_desc" : "OrderId_asc";
            ViewData["QuantitySortParm"] = sortOrder == "Quantity_asc" ? "Quantity_desc" : "Quantity_asc";
            ViewData["CatalogItemIdSortParm"] = sortOrder == "CatalogItemId_asc" ? "CatalogItemId_desc" : "CatalogItemId_asc";
            ViewData["PriceSortParm"] = sortOrder == "Price_asc" ? "Price_desc" : "Price_asc";
            ViewData["SpecialInstructionsSortParm"] = sortOrder == "SpecialInstructions_asc" ? "SpecialInstructions_desc" : "SpecialInstructions_asc";
            ViewData["DiscountPercentSortParm"] = sortOrder == "DiscountPercent_asc" ? "DiscountPercent_desc" : "DiscountPercent_asc";

            dtOrderDetail = OrderDetailData.SelectAll();
            dtOrderHeader = OrderDetail_OrderHeaderData.SelectAll();
            dtCatalogItem = OrderDetail_CatalogItemData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtOrderDetail = OrderDetailData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowOrderDetail in dtOrderDetail.AsEnumerable()
                        join rowOrderHeader in dtOrderHeader.AsEnumerable() on rowOrderDetail.Field<Int32>("OrderId") equals rowOrderHeader.Field<Int32>("OrderId")
                        join rowCatalogItem in dtCatalogItem.AsEnumerable() on rowOrderDetail.Field<Int32>("CatalogItemId") equals rowCatalogItem.Field<Int32>("CatalogItemId")
                        select new OrderDetail() {
                            OrderDetailId = rowOrderDetail.Field<Int32>("OrderDetailId")
                           ,
                            OrderHeader = new OrderHeader() 
                            {
                                   OrderId = rowOrderHeader.Field<Int32>("OrderId")
                            }
                           ,Quantity = rowOrderDetail.Field<Decimal>("Quantity")
                           ,
                            CatalogItem = new CatalogItem() 
                            {
                                   CatalogItemId = rowCatalogItem.Field<Int32>("CatalogItemId")
                            }
                           ,Price = rowOrderDetail.Field<Decimal>("Price")
                           ,SpecialInstructions = rowOrderDetail.Field<String>("SpecialInstructions")
                           ,DiscountPercent = rowOrderDetail.Field<Byte>("DiscountPercent")
                        };

            switch (sortOrder)
            {
                case "OrderDetailId_desc":
                    Query = Query.OrderByDescending(s => s.OrderDetailId);
                    break;
                case "OrderDetailId_asc":
                    Query = Query.OrderBy(s => s.OrderDetailId);
                    break;
                case "OrderId_desc":
                    Query = Query.OrderByDescending(s => s.OrderHeader.OrderId);
                    break;
                case "OrderId_asc":
                    Query = Query.OrderBy(s => s.OrderHeader.OrderId);
                    break;
                case "Quantity_desc":
                    Query = Query.OrderByDescending(s => s.Quantity);
                    break;
                case "Quantity_asc":
                    Query = Query.OrderBy(s => s.Quantity);
                    break;
                case "CatalogItemId_desc":
                    Query = Query.OrderByDescending(s => s.CatalogItem.CatalogItemId);
                    break;
                case "CatalogItemId_asc":
                    Query = Query.OrderBy(s => s.CatalogItem.CatalogItemId);
                    break;
                case "Price_desc":
                    Query = Query.OrderByDescending(s => s.Price);
                    break;
                case "Price_asc":
                    Query = Query.OrderBy(s => s.Price);
                    break;
                case "SpecialInstructions_desc":
                    Query = Query.OrderByDescending(s => s.SpecialInstructions);
                    break;
                case "SpecialInstructions_asc":
                    Query = Query.OrderBy(s => s.SpecialInstructions);
                    break;
                case "DiscountPercent_desc":
                    Query = Query.OrderByDescending(s => s.DiscountPercent);
                    break;
                case "DiscountPercent_asc":
                    Query = Query.OrderBy(s => s.DiscountPercent);
                    break;
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.OrderDetailId);
                    break;
            }

            if (command == "Export") {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("Order Detail Id", typeof(string));
                dt.Columns.Add("Order Id", typeof(string));
                dt.Columns.Add("Quantity", typeof(string));
                dt.Columns.Add("Catalog Item Id", typeof(string));
                dt.Columns.Add("Price", typeof(string));
                dt.Columns.Add("Special Instructions", typeof(string));
                dt.Columns.Add("Discount Percent", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.OrderDetailId
                       ,item.OrderHeader.OrderId
                       ,item.Quantity
                       ,item.CatalogItem.CatalogItemId
                       ,item.Price
                       ,item.SpecialInstructions
                       ,item.DiscountPercent
                    );
                }
                gv.DataSource = dt;
                gv.DataBind();
                ExportData(Export, gv, dt);
            }

            int pageNumber = (page ?? 1);
            int? pageSZ = (Convert.ToInt32(Session["PageSize"]) == 0 ? 5 : Convert.ToInt32(Session["PageSize"]));
            return View(Query.ToPagedList(pageNumber, (pageSZ ?? 5)));
        }

        // GET: /OrderDetail/Details/<id>
        public ActionResult Details(
                                      Int32? OrderDetailId
                                   )
        {
            if (
                    OrderDetailId == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dtOrderHeader = OrderDetail_OrderHeaderData.SelectAll();
            dtCatalogItem = OrderDetail_CatalogItemData.SelectAll();

            OrderDetail OrderDetail = new OrderDetail();
            OrderDetail.OrderDetailId = System.Convert.ToInt32(OrderDetailId);
            OrderDetail = OrderDetailData.Select_Record(OrderDetail);
            OrderDetail.OrderHeader = new OrderHeader()
            {
                OrderId = (Int32)OrderDetail.OrderId
            };
            OrderDetail.CatalogItem = new CatalogItem()
            {
                CatalogItemId = (Int32)OrderDetail.CatalogItemId
            };

            if (OrderDetail == null)
            {
                return HttpNotFound();
            }
            return View(OrderDetail);
        }

        // GET: /OrderDetail/Create
        public ActionResult Create()
        {
        // ComboBox
            ViewData["OrderId"] = new SelectList(OrderDetail_OrderHeaderData.List(), "OrderId", "OrderId");
            ViewData["CatalogItemId"] = new SelectList(OrderDetail_CatalogItemData.List(), "CatalogItemId", "CatalogItemId");

            return View();
        }

        // POST: /OrderDetail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
				           "OrderId"
				   + "," + "Quantity"
				   + "," + "CatalogItemId"
				   + "," + "Price"
				   + "," + "SpecialInstructions"
				   + "," + "DiscountPercent"
				  )] OrderDetail OrderDetail)
        {
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = OrderDetailData.Add(OrderDetail);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Insert");
                }
            }
        // ComboBox
            ViewData["OrderId"] = new SelectList(OrderDetail_OrderHeaderData.List(), "OrderId", "OrderId", OrderDetail.OrderId);
            ViewData["CatalogItemId"] = new SelectList(OrderDetail_CatalogItemData.List(), "CatalogItemId", "CatalogItemId", OrderDetail.CatalogItemId);

            return View(OrderDetail);
        }

        // GET: /OrderDetail/Edit/<id>
        public ActionResult Edit(
                                   Int32? OrderDetailId
                                )
        {
            if (
                    OrderDetailId == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OrderDetail OrderDetail = new OrderDetail();
            OrderDetail.OrderDetailId = System.Convert.ToInt32(OrderDetailId);
            OrderDetail = OrderDetailData.Select_Record(OrderDetail);

            if (OrderDetail == null)
            {
                return HttpNotFound();
            }
        // ComboBox
            ViewData["OrderId"] = new SelectList(OrderDetail_OrderHeaderData.List(), "OrderId", "OrderId", OrderDetail.OrderId);
            ViewData["CatalogItemId"] = new SelectList(OrderDetail_CatalogItemData.List(), "CatalogItemId", "CatalogItemId", OrderDetail.CatalogItemId);

            return View(OrderDetail);
        }

        // POST: /OrderDetail/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include=
				     " OrderDetailId"
				   + ",OrderId"
				   + ",Quantity"
				   + ",CatalogItemId"
				   + ",Price"
				   + ",SpecialInstructions"
				   + ",DiscountPercent"
				  )] OrderDetail OrderDetail)
        {

            OrderDetail oOrderDetail = new OrderDetail();
            oOrderDetail.OrderDetailId = System.Convert.ToInt32(OrderDetail.OrderDetailId);
            oOrderDetail = OrderDetailData.Select_Record(OrderDetail);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = OrderDetailData.Update(oOrderDetail, OrderDetail);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Update");
                }
            }
        // ComboBox
            ViewData["OrderId"] = new SelectList(OrderDetail_OrderHeaderData.List(), "OrderId", "OrderId", OrderDetail.OrderId);
            ViewData["CatalogItemId"] = new SelectList(OrderDetail_CatalogItemData.List(), "CatalogItemId", "CatalogItemId", OrderDetail.CatalogItemId);

            return View(OrderDetail);
        }

        // GET: /OrderDetail/Delete/<id>
        public ActionResult Delete(
                                     Int32? OrderDetailId
                                  )
        {
            if (
                    OrderDetailId == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dtOrderHeader = OrderDetail_OrderHeaderData.SelectAll();
            dtCatalogItem = OrderDetail_CatalogItemData.SelectAll();

            OrderDetail OrderDetail = new OrderDetail();
            OrderDetail.OrderDetailId = System.Convert.ToInt32(OrderDetailId);
            OrderDetail = OrderDetailData.Select_Record(OrderDetail);
            OrderDetail.OrderHeader = new OrderHeader()
            {
                OrderId = (Int32)OrderDetail.OrderId
            };
            OrderDetail.CatalogItem = new CatalogItem()
            {
                CatalogItemId = (Int32)OrderDetail.CatalogItemId
            };

            if (OrderDetail == null)
            {
                return HttpNotFound();
            }
            return View(OrderDetail);
        }

        // POST: /OrderDetail/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int32? OrderDetailId
                                            )
        {

            OrderDetail OrderDetail = new OrderDetail();
            OrderDetail.OrderDetailId = System.Convert.ToInt32(OrderDetailId);
            OrderDetail = OrderDetailData.Select_Record(OrderDetail);

            bool bSucess = false;
            bSucess = OrderDetailData.Delete(OrderDetail);
            if (bSucess == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Can Not Delete");
            }
            return null;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private static List<SelectListItem> GetFields(String select)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SelectListItem Item1 = new SelectListItem { Text = "Order Detail Id", Value = "Order Detail Id" };
            SelectListItem Item2 = new SelectListItem { Text = "Order Id", Value = "Order Id" };
            SelectListItem Item3 = new SelectListItem { Text = "Quantity", Value = "Quantity" };
            SelectListItem Item4 = new SelectListItem { Text = "Catalog Item Id", Value = "Catalog Item Id" };
            SelectListItem Item5 = new SelectListItem { Text = "Price", Value = "Price" };
            SelectListItem Item6 = new SelectListItem { Text = "Special Instructions", Value = "Special Instructions" };
            SelectListItem Item7 = new SelectListItem { Text = "Discount Percent", Value = "Discount Percent" };

                 if (select == "Order Detail Id") { Item1.Selected = true; }
            else if (select == "Order Id") { Item2.Selected = true; }
            else if (select == "Quantity") { Item3.Selected = true; }
            else if (select == "Catalog Item Id") { Item4.Selected = true; }
            else if (select == "Price") { Item5.Selected = true; }
            else if (select == "Special Instructions") { Item6.Selected = true; }
            else if (select == "Discount Percent") { Item7.Selected = true; }

            list.Add(Item1);
            list.Add(Item2);
            list.Add(Item3);
            list.Add(Item4);
            list.Add(Item5);
            list.Add(Item6);
            list.Add(Item7);

            return list.ToList();
        }

        private void ExportData(String Export, GridView gv, DataTable dt)
        {
            if (Export == "Pdf")
            {
                PDFform pdfForm = new PDFform(dt, "Dbo. Order Detail", "Many");
                Document document = pdfForm.CreateDocument();
                PdfDocumentRenderer renderer = new PdfDocumentRenderer(true);
                renderer.Document = document;
                renderer.RenderDocument();

                MemoryStream stream = new MemoryStream();
                renderer.PdfDocument.Save(stream, false);

                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename=" + "Report.pdf");
                Response.ContentType = "application/Pdf.pdf";
                Response.BinaryWrite(stream.ToArray());
                Response.Flush();
                Response.End();
            }
            else
            {
                Response.ClearContent();
                Response.Buffer = true;
                if (Export == "Excel")
                {
                    Response.AddHeader("content-disposition", "attachment;filename=" + "Report.xls");
                    Response.ContentType = "application/Excel.xls";
                }
                else if (Export == "Word")
                {
                    Response.AddHeader("content-disposition", "attachment;filename=" + "Report.doc");
                    Response.ContentType = "application/Word.doc";
                }
                Response.Charset = "";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gv.RenderControl(htw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

    }
}
 
