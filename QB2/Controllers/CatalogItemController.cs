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
    public class CatalogItemController : Controller
    {

        DataTable dtCatalogItem = new DataTable();

        // GET: /CatalogItem/
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

            ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "Catalog Item Id" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["CatalogItemIdSortParm"] = sortOrder == "CatalogItemId_asc" ? "CatalogItemId_desc" : "CatalogItemId_asc";
            ViewData["NameSortParm"] = sortOrder == "Name_asc" ? "Name_desc" : "Name_asc";
            ViewData["DescriptionSortParm"] = sortOrder == "Description_asc" ? "Description_desc" : "Description_asc";
            ViewData["SkuSortParm"] = sortOrder == "Sku_asc" ? "Sku_desc" : "Sku_asc";
            ViewData["QbItemIdSortParm"] = sortOrder == "QbItemId_asc" ? "QbItemId_desc" : "QbItemId_asc";

            dtCatalogItem = CatalogItemData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtCatalogItem = CatalogItemData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowCatalogItem in dtCatalogItem.AsEnumerable()
                        select new CatalogItem() {
                            CatalogItemId = rowCatalogItem.Field<Int32>("CatalogItemId")
                           ,Name = rowCatalogItem.Field<String>("Name")
                           ,Description = rowCatalogItem.Field<String>("Description")
                           ,Sku = rowCatalogItem.Field<String>("Sku")
                           ,QbItemId = rowCatalogItem.Field<String>("QbItemId")
                        };

            switch (sortOrder)
            {
                case "CatalogItemId_desc":
                    Query = Query.OrderByDescending(s => s.CatalogItemId);
                    break;
                case "CatalogItemId_asc":
                    Query = Query.OrderBy(s => s.CatalogItemId);
                    break;
                case "Name_desc":
                    Query = Query.OrderByDescending(s => s.Name);
                    break;
                case "Name_asc":
                    Query = Query.OrderBy(s => s.Name);
                    break;
                case "Description_desc":
                    Query = Query.OrderByDescending(s => s.Description);
                    break;
                case "Description_asc":
                    Query = Query.OrderBy(s => s.Description);
                    break;
                case "Sku_desc":
                    Query = Query.OrderByDescending(s => s.Sku);
                    break;
                case "Sku_asc":
                    Query = Query.OrderBy(s => s.Sku);
                    break;
                case "QbItemId_desc":
                    Query = Query.OrderByDescending(s => s.QbItemId);
                    break;
                case "QbItemId_asc":
                    Query = Query.OrderBy(s => s.QbItemId);
                    break;
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.CatalogItemId);
                    break;
            }

            if (command == "Export") {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("Catalog Item Id", typeof(string));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Description", typeof(string));
                dt.Columns.Add("Sku", typeof(string));
                dt.Columns.Add("Qb Item Id", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.CatalogItemId
                       ,item.Name
                       ,item.Description
                       ,item.Sku
                       ,item.QbItemId
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

        // GET: /CatalogItem/Details/<id>
        public ActionResult Details(
                                      Int32? CatalogItemId
                                   )
        {
            if (
                    CatalogItemId == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            CatalogItem CatalogItem = new CatalogItem();
            CatalogItem.CatalogItemId = System.Convert.ToInt32(CatalogItemId);
            CatalogItem = CatalogItemData.Select_Record(CatalogItem);

            if (CatalogItem == null)
            {
                return HttpNotFound();
            }
            return View(CatalogItem);
        }

        // GET: /CatalogItem/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: /CatalogItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
				           "Name"
				   + "," + "Description"
				   + "," + "Sku"
				   + "," + "QbItemId"
				  )] CatalogItem CatalogItem)
        {
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = CatalogItemData.Add(CatalogItem);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Insert");
                }
            }

            return View(CatalogItem);
        }

        // GET: /CatalogItem/Edit/<id>
        public ActionResult Edit(
                                   Int32? CatalogItemId
                                )
        {
            if (
                    CatalogItemId == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CatalogItem CatalogItem = new CatalogItem();
            CatalogItem.CatalogItemId = System.Convert.ToInt32(CatalogItemId);
            CatalogItem = CatalogItemData.Select_Record(CatalogItem);

            if (CatalogItem == null)
            {
                return HttpNotFound();
            }

            return View(CatalogItem);
        }

        // POST: /CatalogItem/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include=
				     " CatalogItemId"
				   + ",Name"
				   + ",Description"
				   + ",Sku"
				   + ",QbItemId"
				  )] CatalogItem CatalogItem)
        {

            CatalogItem oCatalogItem = new CatalogItem();
            oCatalogItem.CatalogItemId = System.Convert.ToInt32(CatalogItem.CatalogItemId);
            oCatalogItem = CatalogItemData.Select_Record(CatalogItem);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = CatalogItemData.Update(oCatalogItem, CatalogItem);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Update");
                }
            }

            return View(CatalogItem);
        }

        // GET: /CatalogItem/Delete/<id>
        public ActionResult Delete(
                                     Int32? CatalogItemId
                                  )
        {
            if (
                    CatalogItemId == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            CatalogItem CatalogItem = new CatalogItem();
            CatalogItem.CatalogItemId = System.Convert.ToInt32(CatalogItemId);
            CatalogItem = CatalogItemData.Select_Record(CatalogItem);

            if (CatalogItem == null)
            {
                return HttpNotFound();
            }
            return View(CatalogItem);
        }

        // POST: /CatalogItem/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int32? CatalogItemId
                                            )
        {

            CatalogItem CatalogItem = new CatalogItem();
            CatalogItem.CatalogItemId = System.Convert.ToInt32(CatalogItemId);
            CatalogItem = CatalogItemData.Select_Record(CatalogItem);

            bool bSucess = false;
            bSucess = CatalogItemData.Delete(CatalogItem);
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
            SelectListItem Item1 = new SelectListItem { Text = "Catalog Item Id", Value = "Catalog Item Id" };
            SelectListItem Item2 = new SelectListItem { Text = "Name", Value = "Name" };
            SelectListItem Item3 = new SelectListItem { Text = "Description", Value = "Description" };
            SelectListItem Item4 = new SelectListItem { Text = "Sku", Value = "Sku" };
            SelectListItem Item5 = new SelectListItem { Text = "Qb Item Id", Value = "Qb Item Id" };

                 if (select == "Catalog Item Id") { Item1.Selected = true; }
            else if (select == "Name") { Item2.Selected = true; }
            else if (select == "Description") { Item3.Selected = true; }
            else if (select == "Sku") { Item4.Selected = true; }
            else if (select == "Qb Item Id") { Item5.Selected = true; }

            list.Add(Item1);
            list.Add(Item2);
            list.Add(Item3);
            list.Add(Item4);
            list.Add(Item5);

            return list.ToList();
        }

        private void ExportData(String Export, GridView gv, DataTable dt)
        {
            if (Export == "Pdf")
            {
                PDFform pdfForm = new PDFform(dt, "Dbo. Catalog Item", "Many");
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
 
