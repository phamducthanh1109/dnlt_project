using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaoNguyenLighting.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Product()
        {
            ViewBag.Message = "Product Page";

            return View();
        }

        public ActionResult Trending()
        {
            ViewBag.Message = "Home Page";

            return View();
        }
        public ActionResult New()
        {
            ViewBag.Message = "News Page";

            return View();
        }
    }
}