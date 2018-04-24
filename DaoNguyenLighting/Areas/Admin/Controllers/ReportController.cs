using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using DaoNguyenLighting.Models;

namespace DaoNguyenLighting.Areas.Admin.Controllers
{

    public class ReportController : Controller
    {
        private DNLTEntities db = new DNLTEntities();
        // GET: Admin/Report
        public ActionResult Report()
        {
            if (Session["AdminID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                DateTime dateTime = DateTime.Now;
                var month = dateTime.ToString("MM");
                var year = dateTime.ToString("yyyy");
                var day = dateTime.ToString("dd");
                var realday = year + "-" + month + "-" + day;

                var query =
                         (from p in db.Products
                          let totalQuantity = (from od in db.OrderDetails
                                               join o in db.Orders on od.OrderID equals o.OrderID
                                               where od.ProductID == p.ProductID && o.Date.ToString() == realday
                                               select od.Quantity).Sum()
                          where totalQuantity > 0
                          orderby totalQuantity descending
                          select p).Take(10);

                var productlist = query
                   .Select(a => new
                   {
                       name = a.ProductName,
                       y = a.ProductAmount
                   });


                if (Request.IsAjaxRequest() == false)
                {

                    return View();
                }
                else
                {
                    return Json(productlist);
                }
            }
        }
     
    }
}