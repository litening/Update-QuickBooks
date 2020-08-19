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
    public class OrderHeaderController : Controller
    {

        DataTable dtOrderHeader = new DataTable();
        DataTable dtOurCustomer = new DataTable();
        DataTable dtCustomer = new DataTable();

        // GET: /OrderHeader/
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

            ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "Order Id" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["OrderIdSortParm"] = sortOrder == "OrderId_asc" ? "OrderId_desc" : "OrderId_asc";
            ViewData["UserIdSortParm"] = sortOrder == "UserId_asc" ? "UserId_desc" : "UserId_asc";
            ViewData["OrderDateSortParm"] = sortOrder == "OrderDate_asc" ? "OrderDate_desc" : "OrderDate_asc";
            ViewData["CustomerIdSortParm"] = sortOrder == "CustomerId_asc" ? "CustomerId_desc" : "CustomerId_asc";
            ViewData["OrderTotalSortParm"] = sortOrder == "OrderTotal_asc" ? "OrderTotal_desc" : "OrderTotal_asc";
            ViewData["SalesTaxSortParm"] = sortOrder == "SalesTax_asc" ? "SalesTax_desc" : "SalesTax_asc";
            ViewData["SalesTaxCodeSortParm"] = sortOrder == "SalesTaxCode_asc" ? "SalesTaxCode_desc" : "SalesTaxCode_asc";
            ViewData["ShippingChargeSortParm"] = sortOrder == "ShippingCharge_asc" ? "ShippingCharge_desc" : "ShippingCharge_asc";
            ViewData["QbUpdatedSortParm"] = sortOrder == "QbUpdated_asc" ? "QbUpdated_desc" : "QbUpdated_asc";
            ViewData["SalesTaxAmtSortParm"] = sortOrder == "SalesTaxAmt_asc" ? "SalesTaxAmt_desc" : "SalesTaxAmt_asc";
            ViewData["DiscountAmountSortParm"] = sortOrder == "DiscountAmount_asc" ? "DiscountAmount_desc" : "DiscountAmount_asc";
            ViewData["StatusSortParm"] = sortOrder == "Status_asc" ? "Status_desc" : "Status_asc";
            ViewData["bTestOrderSortParm"] = sortOrder == "bTestOrder_asc" ? "bTestOrder_desc" : "bTestOrder_asc";

            dtOrderHeader = OrderHeaderData.SelectAll();
            dtOurCustomer = OrderHeader_OurCustomerData.SelectAll();
            dtCustomer = OrderHeader_CustomerData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtOrderHeader = OrderHeaderData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowOrderHeader in dtOrderHeader.AsEnumerable()
                        join rowOurCustomer in dtOurCustomer.AsEnumerable() on rowOrderHeader.Field<Int32>("UserId") equals rowOurCustomer.Field<Int32>("UserId")
                        join rowCustomer in dtCustomer.AsEnumerable() on rowOrderHeader.Field<Int32?>("CustomerId") equals rowCustomer.Field<Int32>("CustomerId")
                        select new OrderHeader() {
                            OrderId = rowOrderHeader.Field<Int32>("OrderId")
                           ,
                            OurCustomer = new OurCustomer() 
                            {
                                   UserId = rowOurCustomer.Field<Int32>("UserId")
                            }
                           ,OrderDate = rowOrderHeader.Field<DateTime>("OrderDate")
                           ,
                            Customer = new Customer() 
                            {
                                   CustomerId = rowCustomer.Field<Int32>("CustomerId")
                            }
                           ,OrderTotal = rowOrderHeader.Field<Decimal>("OrderTotal")
                           ,SalesTax = rowOrderHeader.Field<Decimal>("SalesTax")
                           ,SalesTaxCode = rowOrderHeader.Field<Int32?>("SalesTaxCode")
                           ,ShippingCharge = rowOrderHeader.Field<Decimal>("ShippingCharge")
                           ,QbUpdated = rowOrderHeader.Field<Boolean>("QbUpdated")
                           ,SalesTaxAmt = rowOrderHeader.Field<Decimal?>("SalesTaxAmt")
                           ,DiscountAmount = rowOrderHeader.Field<Decimal?>("DiscountAmount")
                           ,Status = rowOrderHeader.Field<Byte?>("Status")
                           ,bTestOrder = rowOrderHeader.Field<Boolean?>("bTestOrder")
                        };

            switch (sortOrder)
            {
                case "OrderId_desc":
                    Query = Query.OrderByDescending(s => s.OrderId);
                    break;
                case "OrderId_asc":
                    Query = Query.OrderBy(s => s.OrderId);
                    break;
                case "UserId_desc":
                    Query = Query.OrderByDescending(s => s.OurCustomer.UserId);
                    break;
                case "UserId_asc":
                    Query = Query.OrderBy(s => s.OurCustomer.UserId);
                    break;
                case "OrderDate_desc":
                    Query = Query.OrderByDescending(s => s.OrderDate);
                    break;
                case "OrderDate_asc":
                    Query = Query.OrderBy(s => s.OrderDate);
                    break;
                case "CustomerId_desc":
                    Query = Query.OrderByDescending(s => s.Customer.CustomerId);
                    break;
                case "CustomerId_asc":
                    Query = Query.OrderBy(s => s.Customer.CustomerId);
                    break;
                case "OrderTotal_desc":
                    Query = Query.OrderByDescending(s => s.OrderTotal);
                    break;
                case "OrderTotal_asc":
                    Query = Query.OrderBy(s => s.OrderTotal);
                    break;
                case "SalesTax_desc":
                    Query = Query.OrderByDescending(s => s.SalesTax);
                    break;
                case "SalesTax_asc":
                    Query = Query.OrderBy(s => s.SalesTax);
                    break;
                case "SalesTaxCode_desc":
                    Query = Query.OrderByDescending(s => s.SalesTaxCode);
                    break;
                case "SalesTaxCode_asc":
                    Query = Query.OrderBy(s => s.SalesTaxCode);
                    break;
                case "ShippingCharge_desc":
                    Query = Query.OrderByDescending(s => s.ShippingCharge);
                    break;
                case "ShippingCharge_asc":
                    Query = Query.OrderBy(s => s.ShippingCharge);
                    break;
                case "QbUpdated_desc":
                    Query = Query.OrderByDescending(s => s.QbUpdated);
                    break;
                case "QbUpdated_asc":
                    Query = Query.OrderBy(s => s.QbUpdated);
                    break;
                case "SalesTaxAmt_desc":
                    Query = Query.OrderByDescending(s => s.SalesTaxAmt);
                    break;
                case "SalesTaxAmt_asc":
                    Query = Query.OrderBy(s => s.SalesTaxAmt);
                    break;
                case "DiscountAmount_desc":
                    Query = Query.OrderByDescending(s => s.DiscountAmount);
                    break;
                case "DiscountAmount_asc":
                    Query = Query.OrderBy(s => s.DiscountAmount);
                    break;
                case "Status_desc":
                    Query = Query.OrderByDescending(s => s.Status);
                    break;
                case "Status_asc":
                    Query = Query.OrderBy(s => s.Status);
                    break;
                case "bTestOrder_desc":
                    Query = Query.OrderByDescending(s => s.bTestOrder);
                    break;
                case "bTestOrder_asc":
                    Query = Query.OrderBy(s => s.bTestOrder);
                    break;
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.OrderId);
                    break;
            }

            if (command == "Export") {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("Order Id", typeof(string));
                dt.Columns.Add("User Id", typeof(string));
                dt.Columns.Add("Order Date", typeof(string));
                dt.Columns.Add("Customer Id", typeof(string));
                dt.Columns.Add("Order Total", typeof(string));
                dt.Columns.Add("Sales Tax", typeof(string));
                dt.Columns.Add("Sales Tax Code", typeof(string));
                dt.Columns.Add("Shipping Charge", typeof(string));
                dt.Columns.Add("Qb Updated", typeof(string));
                dt.Columns.Add("Sales Tax Amt", typeof(string));
                dt.Columns.Add("Discount Amount", typeof(string));
                dt.Columns.Add("Status", typeof(string));
                dt.Columns.Add("B Test Order", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.OrderId
                       ,item.OurCustomer.UserId
                       ,item.OrderDate
                       ,item.Customer.CustomerId
                       ,item.OrderTotal
                       ,item.SalesTax
                       ,item.SalesTaxCode
                       ,item.ShippingCharge
                       ,item.QbUpdated
                       ,item.SalesTaxAmt
                       ,item.DiscountAmount
                       ,item.Status
                       ,item.bTestOrder
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

        // GET: /OrderHeader/Details/<id>
        public ActionResult Details(
                                      Int32? OrderId
                                   )
        {
            if (
                    OrderId == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dtOurCustomer = OrderHeader_OurCustomerData.SelectAll();
            dtCustomer = OrderHeader_CustomerData.SelectAll();

            OrderHeader OrderHeader = new OrderHeader();
            OrderHeader.OrderId = System.Convert.ToInt32(OrderId);
            OrderHeader = OrderHeaderData.Select_Record(OrderHeader);
            OrderHeader.OurCustomer = new OurCustomer()
            {
                UserId = (Int32)OrderHeader.UserId
            };
            OrderHeader.Customer = new Customer()
            {
                CustomerId = (Int32)OrderHeader.CustomerId
            };

            if (OrderHeader == null)
            {
                return HttpNotFound();
            }
            return View(OrderHeader);
        }

        // GET: /OrderHeader/Create
        public ActionResult Create()
        {
        // ComboBox
            ViewData["UserId"] = new SelectList(OrderHeader_OurCustomerData.List(), "UserId", "UserId");
            ViewData["CustomerId"] = new SelectList(OrderHeader_CustomerData.List(), "CustomerId", "CustomerId");

            return View();
        }

        // POST: /OrderHeader/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
				           "UserId"
				   + "," + "OrderDate"
				   + "," + "CustomerId"
				   + "," + "OrderTotal"
				   + "," + "SalesTax"
				   + "," + "SalesTaxCode"
				   + "," + "ShippingCharge"
				   + "," + "QbUpdated"
				   + "," + "SalesTaxAmt"
				   + "," + "DiscountAmount"
				   + "," + "Status"
				   + "," + "bTestOrder"
				  )] OrderHeader OrderHeader)
        {
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = OrderHeaderData.Add(OrderHeader);
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
            ViewData["UserId"] = new SelectList(OrderHeader_OurCustomerData.List(), "UserId", "UserId", OrderHeader.UserId);
            ViewData["CustomerId"] = new SelectList(OrderHeader_CustomerData.List(), "CustomerId", "CustomerId", OrderHeader.CustomerId);

            return View(OrderHeader);
        }

        // GET: /OrderHeader/Edit/<id>
        public ActionResult Edit(
                                   Int32? OrderId
                                )
        {
            if (
                    OrderId == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OrderHeader OrderHeader = new OrderHeader();
            OrderHeader.OrderId = System.Convert.ToInt32(OrderId);
            OrderHeader = OrderHeaderData.Select_Record(OrderHeader);

            if (OrderHeader == null)
            {
                return HttpNotFound();
            }
        // ComboBox
            ViewData["UserId"] = new SelectList(OrderHeader_OurCustomerData.List(), "UserId", "UserId", OrderHeader.UserId);
            ViewData["CustomerId"] = new SelectList(OrderHeader_CustomerData.List(), "CustomerId", "CustomerId", OrderHeader.CustomerId);

            return View(OrderHeader);
        }

        // POST: /OrderHeader/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include=
				     " OrderId"
				   + ",UserId"
				   + ",OrderDate"
				   + ",CustomerId"
				   + ",OrderTotal"
				   + ",SalesTax"
				   + ",SalesTaxCode"
				   + ",ShippingCharge"
				   + ",QbUpdated"
				   + ",SalesTaxAmt"
				   + ",DiscountAmount"
				   + ",Status"
				   + ",bTestOrder"
				  )] OrderHeader OrderHeader)
        {

            OrderHeader oOrderHeader = new OrderHeader();
            oOrderHeader.OrderId = System.Convert.ToInt32(OrderHeader.OrderId);
            oOrderHeader = OrderHeaderData.Select_Record(OrderHeader);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = OrderHeaderData.Update(oOrderHeader, OrderHeader);
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
            ViewData["UserId"] = new SelectList(OrderHeader_OurCustomerData.List(), "UserId", "UserId", OrderHeader.UserId);
            ViewData["CustomerId"] = new SelectList(OrderHeader_CustomerData.List(), "CustomerId", "CustomerId", OrderHeader.CustomerId);

            return View(OrderHeader);
        }

        // GET: /OrderHeader/Delete/<id>
        public ActionResult Delete(
                                     Int32? OrderId
                                  )
        {
            if (
                    OrderId == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dtOurCustomer = OrderHeader_OurCustomerData.SelectAll();
            dtCustomer = OrderHeader_CustomerData.SelectAll();

            OrderHeader OrderHeader = new OrderHeader();
            OrderHeader.OrderId = System.Convert.ToInt32(OrderId);
            OrderHeader = OrderHeaderData.Select_Record(OrderHeader);
            OrderHeader.OurCustomer = new OurCustomer()
            {
                UserId = (Int32)OrderHeader.UserId
            };
            OrderHeader.Customer = new Customer()
            {
                CustomerId = (Int32)OrderHeader.CustomerId
            };

            if (OrderHeader == null)
            {
                return HttpNotFound();
            }
            return View(OrderHeader);
        }

        // POST: /OrderHeader/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int32? OrderId
                                            )
        {

            OrderHeader OrderHeader = new OrderHeader();
            OrderHeader.OrderId = System.Convert.ToInt32(OrderId);
            OrderHeader = OrderHeaderData.Select_Record(OrderHeader);

            bool bSucess = false;
            bSucess = OrderHeaderData.Delete(OrderHeader);
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
            SelectListItem Item1 = new SelectListItem { Text = "Order Id", Value = "Order Id" };
            SelectListItem Item2 = new SelectListItem { Text = "User Id", Value = "User Id" };
            SelectListItem Item3 = new SelectListItem { Text = "Order Date", Value = "Order Date" };
            SelectListItem Item4 = new SelectListItem { Text = "Customer Id", Value = "Customer Id" };
            SelectListItem Item5 = new SelectListItem { Text = "Order Total", Value = "Order Total" };
            SelectListItem Item6 = new SelectListItem { Text = "Sales Tax", Value = "Sales Tax" };
            SelectListItem Item7 = new SelectListItem { Text = "Sales Tax Code", Value = "Sales Tax Code" };
            SelectListItem Item8 = new SelectListItem { Text = "Shipping Charge", Value = "Shipping Charge" };
            SelectListItem Item9 = new SelectListItem { Text = "Qb Updated", Value = "Qb Updated" };
            SelectListItem Item10 = new SelectListItem { Text = "Sales Tax Amt", Value = "Sales Tax Amt" };
            SelectListItem Item11 = new SelectListItem { Text = "Discount Amount", Value = "Discount Amount" };
            SelectListItem Item12 = new SelectListItem { Text = "Status", Value = "Status" };
            SelectListItem Item13 = new SelectListItem { Text = "B Test Order", Value = "B Test Order" };

                 if (select == "Order Id") { Item1.Selected = true; }
            else if (select == "User Id") { Item2.Selected = true; }
            else if (select == "Order Date") { Item3.Selected = true; }
            else if (select == "Customer Id") { Item4.Selected = true; }
            else if (select == "Order Total") { Item5.Selected = true; }
            else if (select == "Sales Tax") { Item6.Selected = true; }
            else if (select == "Sales Tax Code") { Item7.Selected = true; }
            else if (select == "Shipping Charge") { Item8.Selected = true; }
            else if (select == "Qb Updated") { Item9.Selected = true; }
            else if (select == "Sales Tax Amt") { Item10.Selected = true; }
            else if (select == "Discount Amount") { Item11.Selected = true; }
            else if (select == "Status") { Item12.Selected = true; }
            else if (select == "B Test Order") { Item13.Selected = true; }

            list.Add(Item1);
            list.Add(Item2);
            list.Add(Item3);
            list.Add(Item4);
            list.Add(Item5);
            list.Add(Item6);
            list.Add(Item7);
            list.Add(Item8);
            list.Add(Item9);
            list.Add(Item10);
            list.Add(Item11);
            list.Add(Item12);
            list.Add(Item13);

            return list.ToList();
        }

        private void ExportData(String Export, GridView gv, DataTable dt)
        {
            if (Export == "Pdf")
            {
                PDFform pdfForm = new PDFform(dt, "Dbo. Order Header", "Many");
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
 
