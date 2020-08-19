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
    public class ItemOptionController : Controller
    {

        DataTable dtItemOption = new DataTable();

        // GET: /ItemOption/
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

            ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "Item Option Id" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["ItemOptionIdSortParm"] = sortOrder == "ItemOptionId_asc" ? "ItemOptionId_desc" : "ItemOptionId_asc";
            ViewData["DescriptionSortParm"] = sortOrder == "Description_asc" ? "Description_desc" : "Description_asc";

            dtItemOption = ItemOptionData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtItemOption = ItemOptionData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowItemOption in dtItemOption.AsEnumerable()
                        select new ItemOption() {
                            ItemOptionId = rowItemOption.Field<Int32>("ItemOptionId")
                           ,Description = rowItemOption.Field<String>("Description")
                        };

            switch (sortOrder)
            {
                case "ItemOptionId_desc":
                    Query = Query.OrderByDescending(s => s.ItemOptionId);
                    break;
                case "ItemOptionId_asc":
                    Query = Query.OrderBy(s => s.ItemOptionId);
                    break;
                case "Description_desc":
                    Query = Query.OrderByDescending(s => s.Description);
                    break;
                case "Description_asc":
                    Query = Query.OrderBy(s => s.Description);
                    break;
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.ItemOptionId);
                    break;
            }

            if (command == "Export") {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("Item Option Id", typeof(string));
                dt.Columns.Add("Description", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.ItemOptionId
                       ,item.Description
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

        // GET: /ItemOption/Details/<id>
        public ActionResult Details(
                                      Int32? ItemOptionId
                                   )
        {
            if (
                    ItemOptionId == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            ItemOption ItemOption = new ItemOption();
            ItemOption.ItemOptionId = System.Convert.ToInt32(ItemOptionId);
            ItemOption = ItemOptionData.Select_Record(ItemOption);

            if (ItemOption == null)
            {
                return HttpNotFound();
            }
            return View(ItemOption);
        }

        // GET: /ItemOption/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: /ItemOption/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
				           "Description"
				  )] ItemOption ItemOption)
        {
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = ItemOptionData.Add(ItemOption);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Insert");
                }
            }

            return View(ItemOption);
        }

        // GET: /ItemOption/Edit/<id>
        public ActionResult Edit(
                                   Int32? ItemOptionId
                                )
        {
            if (
                    ItemOptionId == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ItemOption ItemOption = new ItemOption();
            ItemOption.ItemOptionId = System.Convert.ToInt32(ItemOptionId);
            ItemOption = ItemOptionData.Select_Record(ItemOption);

            if (ItemOption == null)
            {
                return HttpNotFound();
            }

            return View(ItemOption);
        }

        // POST: /ItemOption/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include=
				     " ItemOptionId"
				   + ",Description"
				  )] ItemOption ItemOption)
        {

            ItemOption oItemOption = new ItemOption();
            oItemOption.ItemOptionId = System.Convert.ToInt32(ItemOption.ItemOptionId);
            oItemOption = ItemOptionData.Select_Record(ItemOption);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = ItemOptionData.Update(oItemOption, ItemOption);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Update");
                }
            }

            return View(ItemOption);
        }

        // GET: /ItemOption/Delete/<id>
        public ActionResult Delete(
                                     Int32? ItemOptionId
                                  )
        {
            if (
                    ItemOptionId == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            ItemOption ItemOption = new ItemOption();
            ItemOption.ItemOptionId = System.Convert.ToInt32(ItemOptionId);
            ItemOption = ItemOptionData.Select_Record(ItemOption);

            if (ItemOption == null)
            {
                return HttpNotFound();
            }
            return View(ItemOption);
        }

        // POST: /ItemOption/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int32? ItemOptionId
                                            )
        {

            ItemOption ItemOption = new ItemOption();
            ItemOption.ItemOptionId = System.Convert.ToInt32(ItemOptionId);
            ItemOption = ItemOptionData.Select_Record(ItemOption);

            bool bSucess = false;
            bSucess = ItemOptionData.Delete(ItemOption);
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
            SelectListItem Item1 = new SelectListItem { Text = "Item Option Id", Value = "Item Option Id" };
            SelectListItem Item2 = new SelectListItem { Text = "Description", Value = "Description" };

                 if (select == "Item Option Id") { Item1.Selected = true; }
            else if (select == "Description") { Item2.Selected = true; }

            list.Add(Item1);
            list.Add(Item2);

            return list.ToList();
        }

        private void ExportData(String Export, GridView gv, DataTable dt)
        {
            if (Export == "Pdf")
            {
                PDFform pdfForm = new PDFform(dt, "Dbo. Item Option", "Many");
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
 
