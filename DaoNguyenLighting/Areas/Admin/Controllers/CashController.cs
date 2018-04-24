using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DaoNguyenLighting.Models;
namespace DaoNguyenLighting.Areas.Admin.Controllers
{
    public class CashController : Controller
    {
        private DNLTEntities db = new DNLTEntities();
        // GET: Admin/Cash
        public ActionResult Cash()
        {
            if (Session["AdminID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                List<Cash> list = new List<Cash>();
                list = db.Cashes.ToList();
                return View(list);
            }
            
        }

        public ActionResult AddCash()
        {
            if (Session["AdminID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult AddCash(Cash cashmodel)
        {
            List<Cash> list = new List<Cash>();
            list = db.Cashes.ToList();
            int idCash = list.Count + 1;
            cashmodel.CashID = idCash;
            cashmodel.Date = DateTime.Now;

            if (cashmodel.Gender == "Pay")
            {
                cashmodel.Cashes = "-" + cashmodel.Cashes;
            }

            if (ModelState.IsValid)
            {
                db.Cashes.Add(cashmodel);
                db.SaveChanges();
            }
            ModelState.Clear();
            return View();

        }

      
    }
}