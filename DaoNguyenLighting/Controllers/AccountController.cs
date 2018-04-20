using DaoNguyenLighting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaoNguyenLighting.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Autherize(User usermodel)
        {
            using (DNLTEntities db = new DNLTEntities())
            {
                var ud = db.Users.Where(x => x.Email == usermodel.Email && x.Password == usermodel.Password).FirstOrDefault();
                if (ud == null)
                {
                    ViewBag.Message = "Wrong Email or Password !";
                    return View("Login");
                }
                else
                {
                    Session["UserID"] = ud.UserID;
                    return RedirectToAction("Trending", "Home");                 
                }
            }
        }
        public ActionResult Register()
        {
                return View();
        }

        [HttpPost]
        public ActionResult Register(User usermodel)
        {
            using (DNLTEntities db = new DNLTEntities())
            {
                if (ModelState.IsValid)
                {
                    db.Users.Add(usermodel);
                    db.SaveChanges();
                    Session["UserID"] = usermodel.UserID;
                }
            }
            ModelState.Clear();
            return RedirectToAction("Trending", "Home");
        }

    }
}