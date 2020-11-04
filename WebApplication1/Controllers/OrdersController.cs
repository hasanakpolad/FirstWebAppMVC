using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.DatabaseContext;

namespace WebApplication1.Controllers
{
    public class OrdersController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        // GET: Orders
        public ActionResult Index()
        {
            return View(new List<Orders>());
        }

        [HttpPost]
        public JsonResult GetOrders()
        {
            int draw = Convert.ToInt32(Request.Form["draw"]);// etkin sayfa numarası
            int start = Convert.ToInt32(Request["start"]);//listenen ilk kayıtın  index numarası
            int length = Convert.ToInt32(Request["length"]);//sayfadaki toplam listelenecek kayit sayısı
            string search = Request["search[value]"];//arama
            string sortColumnName = Request["columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]"];//Sıralama yapılacak column adı
            string sortDirection = Request["order[0][dir]"];//sıralama türü

            List<Orders> model = db.Orders.Where(x => x.Title.Contains(search) || x.Explain.Contains(search)).OrderBy(sortColumnName + " " + sortDirection).Skip(start).Take(length).ToList();

            int FiltrelenmisKayitSayisi = db.Orders.Count();

            int toplamKayit = db.Orders.Count();

            return Json(new { data = model, draw = Request["draw"], recordsTotal = toplamKayit, recordsFiltered = FiltrelenmisKayitSayisi });

        }

        public ActionResult Form(int? id)
        {
            Orders order = new Orders();
            if (id != null)
                order = db.Orders.SingleOrDefault(x => x.Id == id.Value);
            return View(order);
        }
        public ActionResult Edit(Orders order)
        {
            if (order.Id == 0)
            {
                db.Orders.Add(order);
            }
            else
            {
                Orders _order = db.Orders.SingleOrDefault(u => u.Id == order.Id);

                _order.Title = order.Title;
                _order.Explain = order.Explain;
                _order.Status = order.Status;
                _order.User = order.User;
                _order.CreateTime = order.CreateTime;
                _order.CreatorId = order.CreatorId;
                _order.StartTime = order.StartTime;
                _order.FinishTime = order.FinishTime;
            }


            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult Show(int? id)
        {
            return PartialView(db.Orders.FirstOrDefault(x => x.Id == id));
        }
        public JsonResult Delete(int id)
        {
            db.Orders.Remove(db.Orders.SingleOrDefault(x => x.Id == id));
            bool result = db.SaveChanges() > 0;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}