using Bogus;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.DatabaseContext;
using Group = WebApplication1.Models.Group;

namespace WebApplication1.Controllers
{
    public class GroupController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        // GET: Group
        public ActionResult Index()
        {

            return View(new List<Group>());
        }
        [HttpPost]
        public JsonResult GetGroups()
        {
            int draw = Convert.ToInt32(Request.Form["draw"]);
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string search = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]"];
            string sortDirection = Request["order[0][dir]"];

            List<Group> model = db.Group.Where(x => x.GroupName.Contains(search) || x.Explain.Contains(search)).OrderBy(sortColumnName + " " + sortDirection).Skip(start).Take(length).ToList();

            int toplamKayit = db.Group.Count();
            int FiltrelenmisKayitSayisi = db.Group.Count();

            return Json(new { data = model, draw = Request["draw"], recordsTotal = toplamKayit, recordsFiltered = FiltrelenmisKayitSayisi });
        }
        public ActionResult Form(int? Id)
        {
            Group group = new Group();
            if (Id != null)
                group = db.Group.SingleOrDefault(x => x.Id == Id.Value);

            return View(group);
        }
        public ActionResult Edit(Group group)
        {
            if (group.Id == 0)
            {
                db.Group.Add(group);
            }
            else
            {
                Group _group = db.Group.SingleOrDefault(u => u.Id == group.Id);

                _group.Explain = group.Explain;
                _group.GroupName = group.GroupName;
            }


            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Show(int? id)
        {
            return PartialView(db.Group.FirstOrDefault(x => x.Id == id));
        }
        public JsonResult Delete(int id)
        {
            db.Group.Remove(db.Group.SingleOrDefault(x => x.Id == id));
            bool result = db.SaveChanges() > 0;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}