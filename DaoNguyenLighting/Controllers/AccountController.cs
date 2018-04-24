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
        private DNLTEntities db = new DNLTEntities();
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Autherize(User usermodel)
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

                    if (Session["CartSession"] != null)
                    {
                        return RedirectToAction("Cart", "Cart");
                    }
                    else
                    {
                        return RedirectToAction("Product", "Home");
                    }
            }
            
        }
        public ActionResult Register()
        {
            ModelState.Clear();
            return View();
        }
        public ActionResult Logout()
        {
            Session["UserID"] = null;
            return RedirectToAction("Product", "Home");
        }

        [HttpPost]
        public ActionResult Register(User usermodel)
        {

            if (ModelState.IsValid)
            {
                db.Users.Add(usermodel);
                db.SaveChanges();
                Session["UserID"] = usermodel.UserID;
            }
            ModelState.Clear();
            if (Session["CartSession"] != null)
            {
                return RedirectToAction("Cart", "Cart");
            }
            else
            {
                return RedirectToAction("Product", "Home");
            }
           
        }

    }
}