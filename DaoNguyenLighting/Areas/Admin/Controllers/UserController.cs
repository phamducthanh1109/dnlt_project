using DaoNguyenLighting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaoNguyenLighting.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        private DNLTEntities db = new DNLTEntities();
        // GET: Admin/User
        public ActionResult Users()
        {
            if (Session["AdminID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                List<User> list = new List<User>();
                list = db.Users.ToList();
                return View(list);
            }
        }

        public ActionResult Delete(int id)
        {
            var item = db.Users.Where(x => x.UserID == id).First();
            db.Users.Remove(item);
            db.SaveChanges();
            var item2 = db.Users.ToList();
            return View("Users", item2);

        }
    }
}