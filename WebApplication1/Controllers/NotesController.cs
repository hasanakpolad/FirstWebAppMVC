using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.DatabaseContext;

namespace WebApplication1.Controllers
{
    public class NotesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        // GET: Notes
        public ActionResult Index()
        {

            return View(new List<Notes>());
        }
        [HttpPost]
        public JsonResult GetNotes()
        {
            int draw = Convert.ToInt32(Request.Form["draw"]);
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string search = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]"];
            string sortDirection = Request["order[0][dir]"];

            List<Notes> model = db.Notes.Where(x => x.Title.Contains(search) || x.Note.Contains(search)).OrderBy(sortColumnName + " " + sortDirection).Skip(start).Take(length).ToList();

            int toplamKayit = db.Notes.Count();
            int FiltrelenmisKayitSayisi = db.Notes.Count();

            return Json(new { data = model, draw = Request["draw"], recordsTotal = toplamKayit, recordsFiltered = FiltrelenmisKayitSayisi });
        }

        public ActionResult Form(int? Id)
        {
            Notes notes = new Notes();

            if (Id != null)
                notes = db.Notes.SingleOrDefault(x => x.Id == Id.Value);

            return View(notes);
        }

        public ActionResult Edit(Notes notes)
        {
            if (notes.Id == 0)
            {
                db.Notes.Add(notes);
            }
            else
            {
                Notes _notes = db.Notes.SingleOrDefault(u => u.Id == notes.Id);

                _notes.Note = notes.Note;
                _notes.Title = notes.Title;
                _notes.General = notes.General;
                _notes.User = notes.User;
                _notes.Category.Id = notes.Category.Id;
            }


            db.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult Show(int? id)
        {
            return PartialView(db.Notes.FirstOrDefault(x => x.Id == id));
        }
        public JsonResult Delete(int id)
        {
            db.Notes.Remove(db.Notes.SingleOrDefault(x => x.Id == id));
            bool result = db.SaveChanges() > 0;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}