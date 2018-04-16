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
        public ActionResult Autherize(DaoNguyenLighting.Models.User usermodel)
        {
            using (DNLTEntities db = new DNLTEntities())
            {
                var ud = db.Users.Where(x => x.Email == usermodel.Email && x.Password == usermodel.Password).FirstOrDefault();
                if (ud == null)
                {
                    usermodel.LoginErrorMessage = "Wrong Email or Password !";
                    return View("Login", usermodel);
                }
                else
                {
                    Session["UserID"] = ud.UserID;
                    return RedirectToAction("Trending", "Home");
                   
                }
            }
        }
     
    }
}