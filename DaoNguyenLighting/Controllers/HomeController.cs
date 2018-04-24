using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DaoNguyenLighting.Models;

namespace DaoNguyenLighting.Controllers
{
    public class HomeController : Controller
    {
        private DNLTEntities db = new DNLTEntities();
        public ActionResult Product()
        {
            List<Product> all = new List<Product>();
            all = db.Products.ToList();
            return View(all);
        }
        public ActionResult ProductDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            else
            {
                var item = db.Products.Where(model => model.ProductID == id).First();
                return View(item);
            }
           
            
        }
        

        public ActionResult Trending()
        {
            return View();
        }
        public ActionResult New()
        {
            ViewBag.Message = "News Page";

            return View();
        }
        public ActionResult Collection()
        {
            return View();
        }

    }
}