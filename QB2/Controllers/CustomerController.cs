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
    public class CustomerController : Controller
    {

        DataTable dtCustomer = new DataTable();

        // GET: /Customer/
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

            ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "Customer Id" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["CustomerIdSortParm"] = sortOrder == "CustomerId_asc" ? "CustomerId_desc" : "CustomerId_asc";
            ViewData["FirstNameSortParm"] = sortOrder == "FirstName_asc" ? "FirstName_desc" : "FirstName_asc";
            ViewData["LastNameSortParm"] = sortOrder == "LastName_asc" ? "LastName_desc" : "LastName_asc";
            ViewData["CompanyNameSortParm"] = sortOrder == "CompanyName_asc" ? "CompanyName_desc" : "CompanyName_asc";
            ViewData["Address1SortParm"] = sortOrder == "Address1_asc" ? "Address1_desc" : "Address1_asc";
            ViewData["Address2SortParm"] = sortOrder == "Address2_asc" ? "Address2_desc" : "Address2_asc";
            ViewData["CitySortParm"] = sortOrder == "City_asc" ? "City_desc" : "City_asc";
            ViewData["StateSortParm"] = sortOrder == "State_asc" ? "State_desc" : "State_asc";
            ViewData["ZipSortParm"] = sortOrder == "Zip_asc" ? "Zip_desc" : "Zip_asc";
            ViewData["CountrySortParm"] = sortOrder == "Country_asc" ? "Country_desc" : "Country_asc";
            ViewData["ShipToAddress1SortParm"] = sortOrder == "ShipToAddress1_asc" ? "ShipToAddress1_desc" : "ShipToAddress1_asc";
            ViewData["ShipToAddress2SortParm"] = sortOrder == "ShipToAddress2_asc" ? "ShipToAddress2_desc" : "ShipToAddress2_asc";
            ViewData["ShipToCitySortParm"] = sortOrder == "ShipToCity_asc" ? "ShipToCity_desc" : "ShipToCity_asc";
            ViewData["ShipToStateSortParm"] = sortOrder == "ShipToState_asc" ? "ShipToState_desc" : "ShipToState_asc";
            ViewData["ShipToZipSortParm"] = sortOrder == "ShipToZip_asc" ? "ShipToZip_desc" : "ShipToZip_asc";
            ViewData["ShipToCountrySortParm"] = sortOrder == "ShipToCountry_asc" ? "ShipToCountry_desc" : "ShipToCountry_asc";
            ViewData["ShipToNameSortParm"] = sortOrder == "ShipToName_asc" ? "ShipToName_desc" : "ShipToName_asc";
            ViewData["PhoneSortParm"] = sortOrder == "Phone_asc" ? "Phone_desc" : "Phone_asc";
            ViewData["eMailAddrSortParm"] = sortOrder == "eMailAddr_asc" ? "eMailAddr_desc" : "eMailAddr_asc";
            ViewData["QbCustomerIdSortParm"] = sortOrder == "QbCustomerId_asc" ? "QbCustomerId_desc" : "QbCustomerId_asc";

            dtCustomer = CustomerData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtCustomer = CustomerData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowCustomer in dtCustomer.AsEnumerable()
                        select new Customer() {
                            CustomerId = rowCustomer.Field<Int32>("CustomerId")
                           ,FirstName = rowCustomer.Field<String>("FirstName")
                           ,LastName = rowCustomer.Field<String>("LastName")
                           ,CompanyName = rowCustomer.Field<String>("CompanyName")
                           ,Address1 = rowCustomer.Field<String>("Address1")
                           ,Address2 = rowCustomer.Field<String>("Address2")
                           ,City = rowCustomer.Field<String>("City")
                           ,State = rowCustomer.Field<String>("State")
                           ,Zip = rowCustomer.Field<String>("Zip")
                           ,Country = rowCustomer.Field<String>("Country")
                           ,ShipToAddress1 = rowCustomer.Field<String>("ShipToAddress1")
                           ,ShipToAddress2 = rowCustomer.Field<String>("ShipToAddress2")
                           ,ShipToCity = rowCustomer.Field<String>("ShipToCity")
                           ,ShipToState = rowCustomer.Field<String>("ShipToState")
                           ,ShipToZip = rowCustomer.Field<String>("ShipToZip")
                           ,ShipToCountry = rowCustomer.Field<String>("ShipToCountry")
                           ,ShipToName = rowCustomer.Field<String>("ShipToName")
                           ,Phone = rowCustomer.Field<String>("Phone")
                           ,eMailAddr = rowCustomer.Field<String>("eMailAddr")
                           ,QbCustomerId = rowCustomer.Field<String>("QbCustomerId")
                        };

            switch (sortOrder)
            {
                case "CustomerId_desc":
                    Query = Query.OrderByDescending(s => s.CustomerId);
                    break;
                case "CustomerId_asc":
                    Query = Query.OrderBy(s => s.CustomerId);
                    break;
                case "FirstName_desc":
                    Query = Query.OrderByDescending(s => s.FirstName);
                    break;
                case "FirstName_asc":
                    Query = Query.OrderBy(s => s.FirstName);
                    break;
                case "LastName_desc":
                    Query = Query.OrderByDescending(s => s.LastName);
                    break;
                case "LastName_asc":
                    Query = Query.OrderBy(s => s.LastName);
                    break;
                case "CompanyName_desc":
                    Query = Query.OrderByDescending(s => s.CompanyName);
                    break;
                case "CompanyName_asc":
                    Query = Query.OrderBy(s => s.CompanyName);
                    break;
                case "Address1_desc":
                    Query = Query.OrderByDescending(s => s.Address1);
                    break;
                case "Address1_asc":
                    Query = Query.OrderBy(s => s.Address1);
                    break;
                case "Address2_desc":
                    Query = Query.OrderByDescending(s => s.Address2);
                    break;
                case "Address2_asc":
                    Query = Query.OrderBy(s => s.Address2);
                    break;
                case "City_desc":
                    Query = Query.OrderByDescending(s => s.City);
                    break;
                case "City_asc":
                    Query = Query.OrderBy(s => s.City);
                    break;
                case "State_desc":
                    Query = Query.OrderByDescending(s => s.State);
                    break;
                case "State_asc":
                    Query = Query.OrderBy(s => s.State);
                    break;
                case "Zip_desc":
                    Query = Query.OrderByDescending(s => s.Zip);
                    break;
                case "Zip_asc":
                    Query = Query.OrderBy(s => s.Zip);
                    break;
                case "Country_desc":
                    Query = Query.OrderByDescending(s => s.Country);
                    break;
                case "Country_asc":
                    Query = Query.OrderBy(s => s.Country);
                    break;
                case "ShipToAddress1_desc":
                    Query = Query.OrderByDescending(s => s.ShipToAddress1);
                    break;
                case "ShipToAddress1_asc":
                    Query = Query.OrderBy(s => s.ShipToAddress1);
                    break;
                case "ShipToAddress2_desc":
                    Query = Query.OrderByDescending(s => s.ShipToAddress2);
                    break;
                case "ShipToAddress2_asc":
                    Query = Query.OrderBy(s => s.ShipToAddress2);
                    break;
                case "ShipToCity_desc":
                    Query = Query.OrderByDescending(s => s.ShipToCity);
                    break;
                case "ShipToCity_asc":
                    Query = Query.OrderBy(s => s.ShipToCity);
                    break;
                case "ShipToState_desc":
                    Query = Query.OrderByDescending(s => s.ShipToState);
                    break;
                case "ShipToState_asc":
                    Query = Query.OrderBy(s => s.ShipToState);
                    break;
                case "ShipToZip_desc":
                    Query = Query.OrderByDescending(s => s.ShipToZip);
                    break;
                case "ShipToZip_asc":
                    Query = Query.OrderBy(s => s.ShipToZip);
                    break;
                case "ShipToCountry_desc":
                    Query = Query.OrderByDescending(s => s.ShipToCountry);
                    break;
                case "ShipToCountry_asc":
                    Query = Query.OrderBy(s => s.ShipToCountry);
                    break;
                case "ShipToName_desc":
                    Query = Query.OrderByDescending(s => s.ShipToName);
                    break;
                case "ShipToName_asc":
                    Query = Query.OrderBy(s => s.ShipToName);
                    break;
                case "Phone_desc":
                    Query = Query.OrderByDescending(s => s.Phone);
                    break;
                case "Phone_asc":
                    Query = Query.OrderBy(s => s.Phone);
                    break;
                case "eMailAddr_desc":
                    Query = Query.OrderByDescending(s => s.eMailAddr);
                    break;
                case "eMailAddr_asc":
                    Query = Query.OrderBy(s => s.eMailAddr);
                    break;
                case "QbCustomerId_desc":
                    Query = Query.OrderByDescending(s => s.QbCustomerId);
                    break;
                case "QbCustomerId_asc":
                    Query = Query.OrderBy(s => s.QbCustomerId);
                    break;
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.CustomerId);
                    break;
            }

            if (command == "Export") {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("Customer Id", typeof(string));
                dt.Columns.Add("First Name", typeof(string));
                dt.Columns.Add("Last Name", typeof(string));
                dt.Columns.Add("Company Name", typeof(string));
                dt.Columns.Add("Address1", typeof(string));
                dt.Columns.Add("Address2", typeof(string));
                dt.Columns.Add("City", typeof(string));
                dt.Columns.Add("State", typeof(string));
                dt.Columns.Add("Zip", typeof(string));
                dt.Columns.Add("Country", typeof(string));
                dt.Columns.Add("Ship To Address1", typeof(string));
                dt.Columns.Add("Ship To Address2", typeof(string));
                dt.Columns.Add("Ship To City", typeof(string));
                dt.Columns.Add("Ship To State", typeof(string));
                dt.Columns.Add("Ship To Zip", typeof(string));
                dt.Columns.Add("Ship To Country", typeof(string));
                dt.Columns.Add("Ship To Name", typeof(string));
                dt.Columns.Add("Phone", typeof(string));
                dt.Columns.Add("E Mail Addr", typeof(string));
                dt.Columns.Add("Qb Customer Id", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.CustomerId
                       ,item.FirstName
                       ,item.LastName
                       ,item.CompanyName
                       ,item.Address1
                       ,item.Address2
                       ,item.City
                       ,item.State
                       ,item.Zip
                       ,item.Country
                       ,item.ShipToAddress1
                       ,item.ShipToAddress2
                       ,item.ShipToCity
                       ,item.ShipToState
                       ,item.ShipToZip
                       ,item.ShipToCountry
                       ,item.ShipToName
                       ,item.Phone
                       ,item.eMailAddr
                       ,item.QbCustomerId
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

        // GET: /Customer/Details/<id>
        public ActionResult Details(
                                      Int32? CustomerId
                                   )
        {
            if (
                    CustomerId == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            Customer Customer = new Customer();
            Customer.CustomerId = System.Convert.ToInt32(CustomerId);
            Customer = CustomerData.Select_Record(Customer);

            if (Customer == null)
            {
                return HttpNotFound();
            }
            return View(Customer);
        }

        // GET: /Customer/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: /Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
				           "FirstName"
				   + "," + "LastName"
				   + "," + "CompanyName"
				   + "," + "Address1"
				   + "," + "Address2"
				   + "," + "City"
				   + "," + "State"
				   + "," + "Zip"
				   + "," + "Country"
				   + "," + "ShipToAddress1"
				   + "," + "ShipToAddress2"
				   + "," + "ShipToCity"
				   + "," + "ShipToState"
				   + "," + "ShipToZip"
				   + "," + "ShipToCountry"
				   + "," + "ShipToName"
				   + "," + "Phone"
				   + "," + "eMailAddr"
				   + "," + "QbCustomerId"
				  )] Customer Customer)
        {
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = CustomerData.Add(Customer);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Insert");
                }
            }

            return View(Customer);
        }

        // GET: /Customer/Edit/<id>
        public ActionResult Edit(
                                   Int32? CustomerId
                                )
        {
            if (
                    CustomerId == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Customer Customer = new Customer();
            Customer.CustomerId = System.Convert.ToInt32(CustomerId);
            Customer = CustomerData.Select_Record(Customer);

            if (Customer == null)
            {
                return HttpNotFound();
            }

            return View(Customer);
        }

        // POST: /Customer/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include=
				     " CustomerId"
				   + ",FirstName"
				   + ",LastName"
				   + ",CompanyName"
				   + ",Address1"
				   + ",Address2"
				   + ",City"
				   + ",State"
				   + ",Zip"
				   + ",Country"
				   + ",ShipToAddress1"
				   + ",ShipToAddress2"
				   + ",ShipToCity"
				   + ",ShipToState"
				   + ",ShipToZip"
				   + ",ShipToCountry"
				   + ",ShipToName"
				   + ",Phone"
				   + ",eMailAddr"
				   + ",QbCustomerId"
				  )] Customer Customer)
        {

            Customer oCustomer = new Customer();
            oCustomer.CustomerId = System.Convert.ToInt32(Customer.CustomerId);
            oCustomer = CustomerData.Select_Record(Customer);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = CustomerData.Update(oCustomer, Customer);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Update");
                }
            }

            return View(Customer);
        }

        // GET: /Customer/Delete/<id>
        public ActionResult Delete(
                                     Int32? CustomerId
                                  )
        {
            if (
                    CustomerId == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            Customer Customer = new Customer();
            Customer.CustomerId = System.Convert.ToInt32(CustomerId);
            Customer = CustomerData.Select_Record(Customer);

            if (Customer == null)
            {
                return HttpNotFound();
            }
            return View(Customer);
        }

        // POST: /Customer/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int32? CustomerId
                                            )
        {

            Customer Customer = new Customer();
            Customer.CustomerId = System.Convert.ToInt32(CustomerId);
            Customer = CustomerData.Select_Record(Customer);

            bool bSucess = false;
            bSucess = CustomerData.Delete(Customer);
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
            SelectListItem Item1 = new SelectListItem { Text = "Customer Id", Value = "Customer Id" };
            SelectListItem Item2 = new SelectListItem { Text = "First Name", Value = "First Name" };
            SelectListItem Item3 = new SelectListItem { Text = "Last Name", Value = "Last Name" };
            SelectListItem Item4 = new SelectListItem { Text = "Company Name", Value = "Company Name" };
            SelectListItem Item5 = new SelectListItem { Text = "Address1", Value = "Address1" };
            SelectListItem Item6 = new SelectListItem { Text = "Address2", Value = "Address2" };
            SelectListItem Item7 = new SelectListItem { Text = "City", Value = "City" };
            SelectListItem Item8 = new SelectListItem { Text = "State", Value = "State" };
            SelectListItem Item9 = new SelectListItem { Text = "Zip", Value = "Zip" };
            SelectListItem Item10 = new SelectListItem { Text = "Country", Value = "Country" };
            SelectListItem Item11 = new SelectListItem { Text = "Ship To Address1", Value = "Ship To Address1" };
            SelectListItem Item12 = new SelectListItem { Text = "Ship To Address2", Value = "Ship To Address2" };
            SelectListItem Item13 = new SelectListItem { Text = "Ship To City", Value = "Ship To City" };
            SelectListItem Item14 = new SelectListItem { Text = "Ship To State", Value = "Ship To State" };
            SelectListItem Item15 = new SelectListItem { Text = "Ship To Zip", Value = "Ship To Zip" };
            SelectListItem Item16 = new SelectListItem { Text = "Ship To Country", Value = "Ship To Country" };
            SelectListItem Item17 = new SelectListItem { Text = "Ship To Name", Value = "Ship To Name" };
            SelectListItem Item18 = new SelectListItem { Text = "Phone", Value = "Phone" };
            SelectListItem Item19 = new SelectListItem { Text = "E Mail Addr", Value = "E Mail Addr" };
            SelectListItem Item20 = new SelectListItem { Text = "Qb Customer Id", Value = "Qb Customer Id" };

                 if (select == "Customer Id") { Item1.Selected = true; }
            else if (select == "First Name") { Item2.Selected = true; }
            else if (select == "Last Name") { Item3.Selected = true; }
            else if (select == "Company Name") { Item4.Selected = true; }
            else if (select == "Address1") { Item5.Selected = true; }
            else if (select == "Address2") { Item6.Selected = true; }
            else if (select == "City") { Item7.Selected = true; }
            else if (select == "State") { Item8.Selected = true; }
            else if (select == "Zip") { Item9.Selected = true; }
            else if (select == "Country") { Item10.Selected = true; }
            else if (select == "Ship To Address1") { Item11.Selected = true; }
            else if (select == "Ship To Address2") { Item12.Selected = true; }
            else if (select == "Ship To City") { Item13.Selected = true; }
            else if (select == "Ship To State") { Item14.Selected = true; }
            else if (select == "Ship To Zip") { Item15.Selected = true; }
            else if (select == "Ship To Country") { Item16.Selected = true; }
            else if (select == "Ship To Name") { Item17.Selected = true; }
            else if (select == "Phone") { Item18.Selected = true; }
            else if (select == "E Mail Addr") { Item19.Selected = true; }
            else if (select == "Qb Customer Id") { Item20.Selected = true; }

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
            list.Add(Item19);
            list.Add(Item20);

            return list.ToList();
        }

        private void ExportData(String Export, GridView gv, DataTable dt)
        {
            if (Export == "Pdf")
            {
                PDFform pdfForm = new PDFform(dt, "Dbo. Customer", "Many");
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
 
