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
    public class OrderDetailOptionsController : Controller
    {

        DataTable dtOrderDetailOptions = new DataTable();
        DataTable dtItemOption = new DataTable();

        // GET: /OrderDetailOptions/
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

            ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "Order Option Id" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["OrderOptionIdSortParm"] = sortOrder == "OrderOptionId_asc" ? "OrderOptionId_desc" : "OrderOptionId_asc";
            ViewData["OrderDetailIdSortParm"] = sortOrder == "OrderDetailId_asc" ? "OrderDetailId_desc" : "OrderDetailId_asc";
            ViewData["ItemOptionIdSortParm"] = sortOrder == "ItemOptionId_asc" ? "ItemOptionId_desc" : "ItemOptionId_asc";
            ViewData["PriceSortParm"] = sortOrder == "Price_asc" ? "Price_desc" : "Price_asc";

            dtOrderDetailOptions = OrderDetailOptionsData.SelectAll();
            dtItemOption = OrderDetailOptions_ItemOptionData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtOrderDetailOptions = OrderDetailOptionsData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowOrderDetailOptions in dtOrderDetailOptions.AsEnumerable()
                        join rowItemOption in dtItemOption.AsEnumerable() on rowOrderDetailOptions.Field<Int32>("ItemOptionId") equals rowItemOption.Field<Int32>("ItemOptionId")
                        select new OrderDetailOptions() {
                            OrderOptionId = rowOrderDetailOptions.Field<Int32>("OrderOptionId")
                           ,OrderDetailId = rowOrderDetailOptions.Field<Int32>("OrderDetailId")
                           ,
                            ItemOption = new ItemOption() 
                            {
                                   ItemOptionId = rowItemOption.Field<Int32>("ItemOptionId")
                            }
                           ,Price = rowOrderDetailOptions.Field<Decimal>("Price")
                        };

            switch (sortOrder)
            {
                case "OrderOptionId_desc":
                    Query = Query.OrderByDescending(s => s.OrderOptionId);
                    break;
                case "OrderOptionId_asc":
                    Query = Query.OrderBy(s => s.OrderOptionId);
                    break;
                case "OrderDetailId_desc":
                    Query = Query.OrderByDescending(s => s.OrderDetailId);
                    break;
                case "OrderDetailId_asc":
                    Query = Query.OrderBy(s => s.OrderDetailId);
                    break;
                case "ItemOptionId_desc":
                    Query = Query.OrderByDescending(s => s.ItemOption.ItemOptionId);
                    break;
                case "ItemOptionId_asc":
                    Query = Query.OrderBy(s => s.ItemOption.ItemOptionId);
                    break;
                case "Price_desc":
                    Query = Query.OrderByDescending(s => s.Price);
                    break;
                case "Price_asc":
                    Query = Query.OrderBy(s => s.Price);
                    break;
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.OrderOptionId);
                    break;
            }

            if (command == "Export") {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("Order Option Id", typeof(string));
                dt.Columns.Add("Order Detail Id", typeof(string));
                dt.Columns.Add("Item Option Id", typeof(string));
                dt.Columns.Add("Price", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.OrderOptionId
                       ,item.OrderDetailId
                       ,item.ItemOption.ItemOptionId
                       ,item.Price
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

        // GET: /OrderDetailOptions/Details/<id>
        public ActionResult Details(
                                      Int32? OrderOptionId
                                   )
        {
            if (
                    OrderOptionId == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dtItemOption = OrderDetailOptions_ItemOptionData.SelectAll();

            OrderDetailOptions OrderDetailOptions = new OrderDetailOptions();
            OrderDetailOptions.OrderOptionId = System.Convert.ToInt32(OrderOptionId);
            OrderDetailOptions = OrderDetailOptionsData.Select_Record(OrderDetailOptions);
            OrderDetailOptions.ItemOption = new ItemOption()
            {
                ItemOptionId = (Int32)OrderDetailOptions.ItemOptionId
            };

            if (OrderDetailOptions == null)
            {
                return HttpNotFound();
            }
            return View(OrderDetailOptions);
        }

        // GET: /OrderDetailOptions/Create
        public ActionResult Create()
        {
        // ComboBox
            ViewData["ItemOptionId"] = new SelectList(OrderDetailOptions_ItemOptionData.List(), "ItemOptionId", "ItemOptionId");

            return View();
        }

        // POST: /OrderDetailOptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
				           "OrderDetailId"
				   + "," + "ItemOptionId"
				   + "," + "Price"
				  )] OrderDetailOptions OrderDetailOptions)
        {
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = OrderDetailOptionsData.Add(OrderDetailOptions);
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
            ViewData["ItemOptionId"] = new SelectList(OrderDetailOptions_ItemOptionData.List(), "ItemOptionId", "ItemOptionId", OrderDetailOptions.ItemOptionId);

            return View(OrderDetailOptions);
        }

        // GET: /OrderDetailOptions/Edit/<id>
        public ActionResult Edit(
                                   Int32? OrderOptionId
                                )
        {
            if (
                    OrderOptionId == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OrderDetailOptions OrderDetailOptions = new OrderDetailOptions();
            OrderDetailOptions.OrderOptionId = System.Convert.ToInt32(OrderOptionId);
            OrderDetailOptions = OrderDetailOptionsData.Select_Record(OrderDetailOptions);

            if (OrderDetailOptions == null)
            {
                return HttpNotFound();
            }
        // ComboBox
            ViewData["ItemOptionId"] = new SelectList(OrderDetailOptions_ItemOptionData.List(), "ItemOptionId", "ItemOptionId", OrderDetailOptions.ItemOptionId);

            return View(OrderDetailOptions);
        }

        // POST: /OrderDetailOptions/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include=
				     " OrderOptionId"
				   + ",OrderDetailId"
				   + ",ItemOptionId"
				   + ",Price"
				  )] OrderDetailOptions OrderDetailOptions)
        {

            OrderDetailOptions oOrderDetailOptions = new OrderDetailOptions();
            oOrderDetailOptions.OrderOptionId = System.Convert.ToInt32(OrderDetailOptions.OrderOptionId);
            oOrderDetailOptions = OrderDetailOptionsData.Select_Record(OrderDetailOptions);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = OrderDetailOptionsData.Update(oOrderDetailOptions, OrderDetailOptions);
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
            ViewData["ItemOptionId"] = new SelectList(OrderDetailOptions_ItemOptionData.List(), "ItemOptionId", "ItemOptionId", OrderDetailOptions.ItemOptionId);

            return View(OrderDetailOptions);
        }

        // GET: /OrderDetailOptions/Delete/<id>
        public ActionResult Delete(
                                     Int32? OrderOptionId
                                  )
        {
            if (
                    OrderOptionId == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dtItemOption = OrderDetailOptions_ItemOptionData.SelectAll();

            OrderDetailOptions OrderDetailOptions = new OrderDetailOptions();
            OrderDetailOptions.OrderOptionId = System.Convert.ToInt32(OrderOptionId);
            OrderDetailOptions = OrderDetailOptionsData.Select_Record(OrderDetailOptions);
            OrderDetailOptions.ItemOption = new ItemOption()
            {
                ItemOptionId = (Int32)OrderDetailOptions.ItemOptionId
            };

            if (OrderDetailOptions == null)
            {
                return HttpNotFound();
            }
            return View(OrderDetailOptions);
        }

        // POST: /OrderDetailOptions/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int32? OrderOptionId
                                            )
        {

            OrderDetailOptions OrderDetailOptions = new OrderDetailOptions();
            OrderDetailOptions.OrderOptionId = System.Convert.ToInt32(OrderOptionId);
            OrderDetailOptions = OrderDetailOptionsData.Select_Record(OrderDetailOptions);

            bool bSucess = false;
            bSucess = OrderDetailOptionsData.Delete(OrderDetailOptions);
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
            SelectListItem Item1 = new SelectListItem { Text = "Order Option Id", Value = "Order Option Id" };
            SelectListItem Item2 = new SelectListItem { Text = "Order Detail Id", Value = "Order Detail Id" };
            SelectListItem Item3 = new SelectListItem { Text = "Item Option Id", Value = "Item Option Id" };
            SelectListItem Item4 = new SelectListItem { Text = "Price", Value = "Price" };

                 if (select == "Order Option Id") { Item1.Selected = true; }
            else if (select == "Order Detail Id") { Item2.Selected = true; }
            else if (select == "Item Option Id") { Item3.Selected = true; }
            else if (select == "Price") { Item4.Selected = true; }

            list.Add(Item1);
            list.Add(Item2);
            list.Add(Item3);
            list.Add(Item4);

            return list.ToList();
        }

        private void ExportData(String Export, GridView gv, DataTable dt)
        {
            if (Export == "Pdf")
            {
                PDFform pdfForm = new PDFform(dt, "Dbo. Order Detail Options", "Many");
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
 
