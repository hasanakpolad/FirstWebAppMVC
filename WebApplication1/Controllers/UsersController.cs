using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.DatabaseContext;
using WebApplication1.Models;
using System.Web.Helpers;
using PagedList;

namespace WebApplication1.Controllers
{
    public class UsersController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        // GET: Users
        public ActionResult Index()
        {
            return View(new List<Users>());
        }

        [HttpPost]
        public JsonResult GetUsers()
        {
            int draw = Convert.ToInt32(Request.Form["draw"]);// etkin sayfa numarası
            int start = Convert.ToInt32(Request["start"]);//listenen ilk kayıtın  index numarası
            int length = Convert.ToInt32(Request["length"]);//sayfadaki toplam listelenecek kayit sayısı
            string search = Request["search[value]"];//arama
            string sortColumnName = Request["columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]"];//Sıralama yapılacak column adı
            string sortDirection = Request["order[0][dir]"];//sıralama türü

            List<Users> model = db.Users.Where(x => x.Name.Contains(search) || x.Surname.Contains(search)).OrderBy(sortColumnName + " " + sortDirection).Skip(start).Take(length).ToList();

           /* model = db.Users.ToList();

            if (!string.IsNullOrEmpty(search))//filter
            {
                model = model.Where(x => x.Name.Contains(search) || x.Surname.Contains(search)).ToList();
            }
           */
            int FiltrelenmisKayitSayisi =db.Users.Count();
            //short
          /*  model = model.OrderBy(sortColumnName + " " + sortDirection).ToList();
            //paging
            model = model.Skip(start).Take(length).ToList();*/

            int toplamKayit = db.Users.Count();

            return Json(new { data = model, draw = Request["draw"], recordsTotal = toplamKayit, recordsFiltered = FiltrelenmisKayitSayisi });
        }


        public ActionResult Form(int? Id)
        {
            Users user = new Users();

            if (Id != null)
                user = db.Users.SingleOrDefault(x => x.Id == Id.Value);

            return View(user);
        }

        public ActionResult Edit(Users user)
        {
            if (user.Id == 0)
            {
                db.Users.Add(user);
            }
            else
            {
                Users _user = db.Users.SingleOrDefault(u => u.Id == user.Id);

                _user.Name = user.Name;
                _user.Surname = user.Surname;
                _user.Phone = user.Phone;
                _user.Mail = user.Mail;
                _user.Rank = user.Rank;
            }


            db.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult Show(int? id)
        {
            return PartialView(db.Users.FirstOrDefault(x => x.Id == id));
        }

        public JsonResult Delete(int Id)
        {
            db.Users.Remove(db.Users.SingleOrDefault(u => u.Id == Id));
            bool result = db.SaveChanges() > 0;

            //return RedirectToAction("Index");
            return Json(result, JsonRequestBehavior.AllowGet);

        }

    }
}