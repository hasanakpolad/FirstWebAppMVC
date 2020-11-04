using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;
using System.Web.WebSockets;
using WebApplication1.Models;
using WebApplication1.Models.DatabaseContext;

namespace WebApplication1.Controllers
{
    public class CategoryController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        // GET: Category
        public ActionResult Index()
        {

            return View(new List<Category>());
        }
        [HttpPost]
        public JsonResult GetCategory()
        {
            int draw = Convert.ToInt32(Request.Form["draw"]);
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string search = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]"];
            string sortDirection = Request["order[0][dir]"];

            List<Category> model = db.Category.Where(x => x.CategoryName.Contains(search)).OrderBy(sortColumnName + " " + sortDirection).Skip(start).Take(length).ToList();

            int toplamKayit = db.Category.Count();
            int FiltrelenmisKayitSayisi = db.Category.Count();

            return Json(new { data = model, draw = Request["draw"], recordsTotal = toplamKayit, recordsFiltered = FiltrelenmisKayitSayisi });
        }
        public ActionResult Form(int? id)
        {
            Category cat = new Category();
            if (id != null)
            {
                cat = db.Category.SingleOrDefault(x => x.Id == id.Value);
            }
            return View(cat);
        }
        public ActionResult Edit(Category cat)
        {
            if (cat.Id == 0)
            {
                db.Category.Add(cat);
            }
            else
            {
                Category _cat = db.Category.SingleOrDefault(u => u.Id == cat.Id);

                _cat.CategoryName = cat.CategoryName;
                _cat.Notes = cat.Notes;
               
            }


            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult Show(int? id)
        {
            return PartialView(db.Category.FirstOrDefault(u => u.Id == id));
        }
        public JsonResult Delete(int? id)
        {
            db.Category.Remove(db.Category.SingleOrDefault(x => x.Id == id));
            bool result= db.SaveChanges()>0;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}