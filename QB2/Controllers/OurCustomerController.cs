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
    public class OurCustomerController : Controller
    {

        DataTable dtOurCustomer = new DataTable();

        // GET: /OurCustomer/
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

            ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "User Id" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["UserIdSortParm"] = sortOrder == "UserId_asc" ? "UserId_desc" : "UserId_asc";
            ViewData["QbCustomerIdSortParm"] = sortOrder == "QbCustomerId_asc" ? "QbCustomerId_desc" : "QbCustomerId_asc";
            ViewData["QuickbooksAccessTokenSortParm"] = sortOrder == "QuickbooksAccessToken_asc" ? "QuickbooksAccessToken_desc" : "QuickbooksAccessToken_asc";
            ViewData["QuickbooksSecretTokenSortParm"] = sortOrder == "QuickbooksSecretToken_asc" ? "QuickbooksSecretToken_desc" : "QuickbooksSecretToken_asc";
            ViewData["QbSalesAccountSortParm"] = sortOrder == "QbSalesAccount_asc" ? "QbSalesAccount_desc" : "QbSalesAccount_asc";
            ViewData["QbSalesTaxSortParm"] = sortOrder == "QbSalesTax_asc" ? "QbSalesTax_desc" : "QbSalesTax_asc";
            ViewData["QbSalesDiscountsSortParm"] = sortOrder == "QbSalesDiscounts_asc" ? "QbSalesDiscounts_desc" : "QbSalesDiscounts_asc";
            ViewData["QbFreightIncomeSortParm"] = sortOrder == "QbFreightIncome_asc" ? "QbFreightIncome_desc" : "QbFreightIncome_asc";
            ViewData["QbCashSortParm"] = sortOrder == "QbCash_asc" ? "QbCash_desc" : "QbCash_asc";
            ViewData["QbCostOfGoodsSortParm"] = sortOrder == "QbCostOfGoods_asc" ? "QbCostOfGoods_desc" : "QbCostOfGoods_asc";
            ViewData["QbUndepositiedFundsSortParm"] = sortOrder == "QbUndepositiedFunds_asc" ? "QbUndepositiedFunds_desc" : "QbUndepositiedFunds_asc";
            ViewData["QbSalesIdSortParm"] = sortOrder == "QbSalesId_asc" ? "QbSalesId_desc" : "QbSalesId_asc";
            ViewData["QbSalesTaxIdSortParm"] = sortOrder == "QbSalesTaxId_asc" ? "QbSalesTaxId_desc" : "QbSalesTaxId_asc";
            ViewData["QbDiscountsIdSortParm"] = sortOrder == "QbDiscountsId_asc" ? "QbDiscountsId_desc" : "QbDiscountsId_asc";
            ViewData["QbFreightIdSortParm"] = sortOrder == "QbFreightId_asc" ? "QbFreightId_desc" : "QbFreightId_asc";
            ViewData["QbCashIdSortParm"] = sortOrder == "QbCashId_asc" ? "QbCashId_desc" : "QbCashId_asc";
            ViewData["QbCostofGoodsIdSortParm"] = sortOrder == "QbCostofGoodsId_asc" ? "QbCostofGoodsId_desc" : "QbCostofGoodsId_asc";
            ViewData["QbUndepositedFundsIdSortParm"] = sortOrder == "QbUndepositedFundsId_asc" ? "QbUndepositedFundsId_desc" : "QbUndepositedFundsId_asc";

            dtOurCustomer = OurCustomerData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtOurCustomer = OurCustomerData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowOurCustomer in dtOurCustomer.AsEnumerable()
                        select new OurCustomer() {
                            UserId = rowOurCustomer.Field<Int32>("UserId")
                           ,QbCustomerId = rowOurCustomer.Field<String>("QbCustomerId")
                           ,QuickbooksAccessToken = rowOurCustomer.Field<String>("QuickbooksAccessToken")
                           ,QuickbooksSecretToken = rowOurCustomer.Field<String>("QuickbooksSecretToken")
                           ,QbSalesAccount = rowOurCustomer.Field<String>("QbSalesAccount")
                           ,QbSalesTax = rowOurCustomer.Field<String>("QbSalesTax")
                           ,QbSalesDiscounts = rowOurCustomer.Field<String>("QbSalesDiscounts")
                           ,QbFreightIncome = rowOurCustomer.Field<String>("QbFreightIncome")
                           ,QbCash = rowOurCustomer.Field<String>("QbCash")
                           ,QbCostOfGoods = rowOurCustomer.Field<String>("QbCostOfGoods")
                           ,QbUndepositiedFunds = rowOurCustomer.Field<String>("QbUndepositiedFunds")
                           ,QbSalesId = rowOurCustomer.Field<Int32?>("QbSalesId")
                           ,QbSalesTaxId = rowOurCustomer.Field<Int32?>("QbSalesTaxId")
                           ,QbDiscountsId = rowOurCustomer.Field<Int32?>("QbDiscountsId")
                           ,QbFreightId = rowOurCustomer.Field<Int32?>("QbFreightId")
                           ,QbCashId = rowOurCustomer.Field<Int32?>("QbCashId")
                           ,QbCostofGoodsId = rowOurCustomer.Field<Int32?>("QbCostofGoodsId")
                           ,QbUndepositedFundsId = rowOurCustomer.Field<Int32?>("QbUndepositedFundsId")
                        };

            switch (sortOrder)
            {
                case "UserId_desc":
                    Query = Query.OrderByDescending(s => s.UserId);
                    break;
                case "UserId_asc":
                    Query = Query.OrderBy(s => s.UserId);
                    break;
                case "QbCustomerId_desc":
                    Query = Query.OrderByDescending(s => s.QbCustomerId);
                    break;
                case "QbCustomerId_asc":
                    Query = Query.OrderBy(s => s.QbCustomerId);
                    break;
                case "QuickbooksAccessToken_desc":
                    Query = Query.OrderByDescending(s => s.QuickbooksAccessToken);
                    break;
                case "QuickbooksAccessToken_asc":
                    Query = Query.OrderBy(s => s.QuickbooksAccessToken);
                    break;
                case "QuickbooksSecretToken_desc":
                    Query = Query.OrderByDescending(s => s.QuickbooksSecretToken);
                    break;
                case "QuickbooksSecretToken_asc":
                    Query = Query.OrderBy(s => s.QuickbooksSecretToken);
                    break;
                case "QbSalesAccount_desc":
                    Query = Query.OrderByDescending(s => s.QbSalesAccount);
                    break;
                case "QbSalesAccount_asc":
                    Query = Query.OrderBy(s => s.QbSalesAccount);
                    break;
                case "QbSalesTax_desc":
                    Query = Query.OrderByDescending(s => s.QbSalesTax);
                    break;
                case "QbSalesTax_asc":
                    Query = Query.OrderBy(s => s.QbSalesTax);
                    break;
                case "QbSalesDiscounts_desc":
                    Query = Query.OrderByDescending(s => s.QbSalesDiscounts);
                    break;
                case "QbSalesDiscounts_asc":
                    Query = Query.OrderBy(s => s.QbSalesDiscounts);
                    break;
                case "QbFreightIncome_desc":
                    Query = Query.OrderByDescending(s => s.QbFreightIncome);
                    break;
                case "QbFreightIncome_asc":
                    Query = Query.OrderBy(s => s.QbFreightIncome);
                    break;
                case "QbCash_desc":
                    Query = Query.OrderByDescending(s => s.QbCash);
                    break;
                case "QbCash_asc":
                    Query = Query.OrderBy(s => s.QbCash);
                    break;
                case "QbCostOfGoods_desc":
                    Query = Query.OrderByDescending(s => s.QbCostOfGoods);
                    break;
                case "QbCostOfGoods_asc":
                    Query = Query.OrderBy(s => s.QbCostOfGoods);
                    break;
                case "QbUndepositiedFunds_desc":
                    Query = Query.OrderByDescending(s => s.QbUndepositiedFunds);
                    break;
                case "QbUndepositiedFunds_asc":
                    Query = Query.OrderBy(s => s.QbUndepositiedFunds);
                    break;
                case "QbSalesId_desc":
                    Query = Query.OrderByDescending(s => s.QbSalesId);
                    break;
                case "QbSalesId_asc":
                    Query = Query.OrderBy(s => s.QbSalesId);
                    break;
                case "QbSalesTaxId_desc":
                    Query = Query.OrderByDescending(s => s.QbSalesTaxId);
                    break;
                case "QbSalesTaxId_asc":
                    Query = Query.OrderBy(s => s.QbSalesTaxId);
                    break;
                case "QbDiscountsId_desc":
                    Query = Query.OrderByDescending(s => s.QbDiscountsId);
                    break;
                case "QbDiscountsId_asc":
                    Query = Query.OrderBy(s => s.QbDiscountsId);
                    break;
                case "QbFreightId_desc":
                    Query = Query.OrderByDescending(s => s.QbFreightId);
                    break;
                case "QbFreightId_asc":
                    Query = Query.OrderBy(s => s.QbFreightId);
                    break;
                case "QbCashId_desc":
                    Query = Query.OrderByDescending(s => s.QbCashId);
                    break;
                case "QbCashId_asc":
                    Query = Query.OrderBy(s => s.QbCashId);
                    break;
                case "QbCostofGoodsId_desc":
                    Query = Query.OrderByDescending(s => s.QbCostofGoodsId);
                    break;
                case "QbCostofGoodsId_asc":
                    Query = Query.OrderBy(s => s.QbCostofGoodsId);
                    break;
                case "QbUndepositedFundsId_desc":
                    Query = Query.OrderByDescending(s => s.QbUndepositedFundsId);
                    break;
                case "QbUndepositedFundsId_asc":
                    Query = Query.OrderBy(s => s.QbUndepositedFundsId);
                    break;
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.UserId);
                    break;
            }

            if (command == "Export") {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("User Id", typeof(string));
                dt.Columns.Add("Qb Customer Id", typeof(string));
                dt.Columns.Add("Quickbooks Access Token", typeof(string));
                dt.Columns.Add("Quickbooks Secret Token", typeof(string));
                dt.Columns.Add("Qb Sales Account", typeof(string));
                dt.Columns.Add("Qb Sales Tax", typeof(string));
                dt.Columns.Add("Qb Sales Discounts", typeof(string));
                dt.Columns.Add("Qb Freight Income", typeof(string));
                dt.Columns.Add("Qb Cash", typeof(string));
                dt.Columns.Add("Qb Cost Of Goods", typeof(string));
                dt.Columns.Add("Qb Undepositied Funds", typeof(string));
                dt.Columns.Add("Qb Sales Id", typeof(string));
                dt.Columns.Add("Qb Sales Tax Id", typeof(string));
                dt.Columns.Add("Qb Discounts Id", typeof(string));
                dt.Columns.Add("Qb Freight Id", typeof(string));
                dt.Columns.Add("Qb Cash Id", typeof(string));
                dt.Columns.Add("Qb Costof Goods Id", typeof(string));
                dt.Columns.Add("Qb Undeposited Funds Id", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.UserId
                       ,item.QbCustomerId
                       ,item.QuickbooksAccessToken
                       ,item.QuickbooksSecretToken
                       ,item.QbSalesAccount
                       ,item.QbSalesTax
                       ,item.QbSalesDiscounts
                       ,item.QbFreightIncome
                       ,item.QbCash
                       ,item.QbCostOfGoods
                       ,item.QbUndepositiedFunds
                       ,item.QbSalesId
                       ,item.QbSalesTaxId
                       ,item.QbDiscountsId
                       ,item.QbFreightId
                       ,item.QbCashId
                       ,item.QbCostofGoodsId
                       ,item.QbUndepositedFundsId
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

        // GET: /OurCustomer/Details/<id>
        public ActionResult Details(
                                      Int32? UserId
                                   )
        {
            if (
                    UserId == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            OurCustomer OurCustomer = new OurCustomer();
            OurCustomer.UserId = System.Convert.ToInt32(UserId);
            OurCustomer = OurCustomerData.Select_Record(OurCustomer);

            if (OurCustomer == null)
            {
                return HttpNotFound();
            }
            return View(OurCustomer);
        }

        // GET: /OurCustomer/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: /OurCustomer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
				           "QbCustomerId"
				   + "," + "QuickbooksAccessToken"
				   + "," + "QuickbooksSecretToken"
				   + "," + "QbSalesAccount"
				   + "," + "QbSalesTax"
				   + "," + "QbSalesDiscounts"
				   + "," + "QbFreightIncome"
				   + "," + "QbCash"
				   + "," + "QbCostOfGoods"
				   + "," + "QbUndepositiedFunds"
				   + "," + "QbSalesId"
				   + "," + "QbSalesTaxId"
				   + "," + "QbDiscountsId"
				   + "," + "QbFreightId"
				   + "," + "QbCashId"
				   + "," + "QbCostofGoodsId"
				   + "," + "QbUndepositedFundsId"
				  )] OurCustomer OurCustomer)
        {
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = OurCustomerData.Add(OurCustomer);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Insert");
                }
            }

            return View(OurCustomer);
        }

        // GET: /OurCustomer/Edit/<id>
        public ActionResult Edit(
                                   Int32? UserId
                                )
        {
            if (
                    UserId == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OurCustomer OurCustomer = new OurCustomer();
            OurCustomer.UserId = System.Convert.ToInt32(UserId);
            OurCustomer = OurCustomerData.Select_Record(OurCustomer);

            if (OurCustomer == null)
            {
                return HttpNotFound();
            }

            return View(OurCustomer);
        }

        // POST: /OurCustomer/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include=
				     " UserId"
				   + ",QbCustomerId"
				   + ",QuickbooksAccessToken"
				   + ",QuickbooksSecretToken"
				   + ",QbSalesAccount"
				   + ",QbSalesTax"
				   + ",QbSalesDiscounts"
				   + ",QbFreightIncome"
				   + ",QbCash"
				   + ",QbCostOfGoods"
				   + ",QbUndepositiedFunds"
				   + ",QbSalesId"
				   + ",QbSalesTaxId"
				   + ",QbDiscountsId"
				   + ",QbFreightId"
				   + ",QbCashId"
				   + ",QbCostofGoodsId"
				   + ",QbUndepositedFundsId"
				  )] OurCustomer OurCustomer)
        {

            OurCustomer oOurCustomer = new OurCustomer();
            oOurCustomer.UserId = System.Convert.ToInt32(OurCustomer.UserId);
            oOurCustomer = OurCustomerData.Select_Record(OurCustomer);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = OurCustomerData.Update(oOurCustomer, OurCustomer);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Update");
                }
            }

            return View(OurCustomer);
        }

        // GET: /OurCustomer/Delete/<id>
        public ActionResult Delete(
                                     Int32? UserId
                                  )
        {
            if (
                    UserId == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            OurCustomer OurCustomer = new OurCustomer();
            OurCustomer.UserId = System.Convert.ToInt32(UserId);
            OurCustomer = OurCustomerData.Select_Record(OurCustomer);

            if (OurCustomer == null)
            {
                return HttpNotFound();
            }
            return View(OurCustomer);
        }

        // POST: /OurCustomer/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int32? UserId
                                            )
        {

            OurCustomer OurCustomer = new OurCustomer();
            OurCustomer.UserId = System.Convert.ToInt32(UserId);
            OurCustomer = OurCustomerData.Select_Record(OurCustomer);

            bool bSucess = false;
            bSucess = OurCustomerData.Delete(OurCustomer);
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
            SelectListItem Item1 = new SelectListItem { Text = "User Id", Value = "User Id" };
            SelectListItem Item2 = new SelectListItem { Text = "Qb Customer Id", Value = "Qb Customer Id" };
            SelectListItem Item3 = new SelectListItem { Text = "Quickbooks Access Token", Value = "Quickbooks Access Token" };
            SelectListItem Item4 = new SelectListItem { Text = "Quickbooks Secret Token", Value = "Quickbooks Secret Token" };
            SelectListItem Item5 = new SelectListItem { Text = "Qb Sales Account", Value = "Qb Sales Account" };
            SelectListItem Item6 = new SelectListItem { Text = "Qb Sales Tax", Value = "Qb Sales Tax" };
            SelectListItem Item7 = new SelectListItem { Text = "Qb Sales Discounts", Value = "Qb Sales Discounts" };
            SelectListItem Item8 = new SelectListItem { Text = "Qb Freight Income", Value = "Qb Freight Income" };
            SelectListItem Item9 = new SelectListItem { Text = "Qb Cash", Value = "Qb Cash" };
            SelectListItem Item10 = new SelectListItem { Text = "Qb Cost Of Goods", Value = "Qb Cost Of Goods" };
            SelectListItem Item11 = new SelectListItem { Text = "Qb Undepositied Funds", Value = "Qb Undepositied Funds" };
            SelectListItem Item12 = new SelectListItem { Text = "Qb Sales Id", Value = "Qb Sales Id" };
            SelectListItem Item13 = new SelectListItem { Text = "Qb Sales Tax Id", Value = "Qb Sales Tax Id" };
            SelectListItem Item14 = new SelectListItem { Text = "Qb Discounts Id", Value = "Qb Discounts Id" };
            SelectListItem Item15 = new SelectListItem { Text = "Qb Freight Id", Value = "Qb Freight Id" };
            SelectListItem Item16 = new SelectListItem { Text = "Qb Cash Id", Value = "Qb Cash Id" };
            SelectListItem Item17 = new SelectListItem { Text = "Qb Costof Goods Id", Value = "Qb Costof Goods Id" };
            SelectListItem Item18 = new SelectListItem { Text = "Qb Undeposited Funds Id", Value = "Qb Undeposited Funds Id" };

                 if (select == "User Id") { Item1.Selected = true; }
            else if (select == "Qb Customer Id") { Item2.Selected = true; }
            else if (select == "Quickbooks Access Token") { Item3.Selected = true; }
            else if (select == "Quickbooks Secret Token") { Item4.Selected = true; }
            else if (select == "Qb Sales Account") { Item5.Selected = true; }
            else if (select == "Qb Sales Tax") { Item6.Selected = true; }
            else if (select == "Qb Sales Discounts") { Item7.Selected = true; }
            else if (select == "Qb Freight Income") { Item8.Selected = true; }
            else if (select == "Qb Cash") { Item9.Selected = true; }
            else if (select == "Qb Cost Of Goods") { Item10.Selected = true; }
            else if (select == "Qb Undepositied Funds") { Item11.Selected = true; }
            else if (select == "Qb Sales Id") { Item12.Selected = true; }
            else if (select == "Qb Sales Tax Id") { Item13.Selected = true; }
            else if (select == "Qb Discounts Id") { Item14.Selected = true; }
            else if (select == "Qb Freight Id") { Item15.Selected = true; }
            else if (select == "Qb Cash Id") { Item16.Selected = true; }
            else if (select == "Qb Costof Goods Id") { Item17.Selected = true; }
            else if (select == "Qb Undeposited Funds Id") { Item18.Selected = true; }

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
            list.Add(Item14);
            list.Add(Item15);
            list.Add(Item16);
            list.Add(Item17);
            list.Add(Item18);

            return list.ToList();
        }

        private void ExportData(String Export, GridView gv, DataTable dt)
        {
            if (Export == "Pdf")
            {
                PDFform pdfForm = new PDFform(dt, "Dbo. Our Customer", "Many");
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
 
