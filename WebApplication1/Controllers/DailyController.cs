using Bogus;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.DatabaseContext;

namespace WebApplication1.Controllers
{
    public class DailyController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        // GET: Daily
        public ActionResult Index()
        {

            return View(new List<Daily>());
        }
        [HttpPost]
        public JsonResult GetDailys()
        {
            int draw = Convert.ToInt32(Request.Form["draw"]);
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string search = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]"];
            string sortDirection = Request["order[0][dir]"];

            List<Daily> model = db.Daily.Where(x => x.Note.Contains(search) || x.Status.Contains(search)).OrderBy(sortColumnName + " " + sortDirection).Skip(start).Take(length).ToList();

            int toplamKayit = db.Daily.Count();
            int FiltrelenmisKayitSayisi = db.Daily.Count();

            return Json(new { data = model, draw = Request["draw"], recordsTotal = toplamKayit, recordsFiltered = FiltrelenmisKayitSayisi });
        }
        public ActionResult Form(int? id)
        {
            Daily daily = new Daily();
            if (id != null)
            {
                daily = db.Daily.SingleOrDefault(x => x.Id == id.Value);
            }
            return View(daily);
        }
        [HttpPost]
        public ActionResult Edit(Daily daily)
        {
            if (daily.Id == 0)
            {
                db.Daily.Add(daily);
            }
            else
            {
                Daily _daily = db.Daily.SingleOrDefault(u => u.Id == daily.Id);
                _daily.Note = daily.Note;
                _daily.Status = daily.Status;
                _daily.StartTime = daily.StartTime;
                _daily.FinishTime = daily.FinishTime;
                _daily.User= daily.User;
            }
            db.SaveChanges();

            return RedirectToAction("Index");
        }
       
        [HttpPost]
        public ActionResult Show(int? id)
        {
            return PartialView(db.Daily.FirstOrDefault(x => x.Id == id));
        }
        public JsonResult Delete(int id)
        {
            db.Daily.Remove(db.Daily.SingleOrDefault(x => x.Id == id));
            bool result = db.SaveChanges() > 0;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}