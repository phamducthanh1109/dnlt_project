using DaoNguyenLighting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaoNguyenLighting.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private DNLTEntities db = new DNLTEntities();
        // GET: Admin/Login
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Autherize(DaoNguyenLighting.Models.Admin adminmodel)
        {

            var ud = db.Admins.Where(x => x.Name == adminmodel.Name && x.Password == adminmodel.Password).FirstOrDefault();
            if (ud == null)
            {
                ViewBag.Message = "Wrong Email or Password !";
                return View("Login");
            }
            else
            {
                Session["AdminID"] = adminmodel.AdminID;
                return RedirectToAction("Product","Manage");
            }

        }

        public ActionResult Logout()
        {
            Session["AdminID"] = null;
            return View("Login");
        }
    }
}