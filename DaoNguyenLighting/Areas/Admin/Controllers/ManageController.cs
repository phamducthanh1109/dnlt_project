using DaoNguyenLighting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.UI;

namespace DaoNguyenLighting.Areas.Admin.Controllers
{
    public class ManageController : Controller
    {
        private DNLTEntities db = new DNLTEntities();
        // GET: Admin/Manage
        public ActionResult Product()
        {
            if (Session["AdminID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                List<Product> list = new List<Product>();
                list = db.Products.ToList();
                return View(list);
            }
        }

        public ActionResult AddProduct()
        {
           
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddProduct(Product productmodel, HttpPostedFileBase ImageFile)
        {

            //Ten file
            //string fileName = Path.GetFileNameWithoutExtension(productmodel.ImageFile.FileName);

            string fileName;
            //Dinh dang file
            string extension = Path.GetExtension(ImageFile.FileName);

            //ID Product
            List<Product> list = new List<Product>();
            list = db.Products.ToList();
            int idImage = list.Count + 1;
            productmodel.ProductID = idImage;
            //Ghep ten
            fileName = idImage + DateTime.Now.ToString("_ddMMyy_hmmss") + extension;
            productmodel.ImagePath = "/Image/Beautiful/" + fileName;
            productmodel.Date = DateTime.Now;

            fileName = Path.Combine(Server.MapPath("~/Image/Beautiful/"), fileName);
            ImageFile.SaveAs(fileName);

                if (ModelState.IsValid)
                {                  
                    db.Products.Add(productmodel);
                    db.SaveChanges();
                    ViewBag.AddProductSuccess = true;
                }
                ModelState.Clear();

            return View();
        }

        public ActionResult Delete(int id)
        {
            var item = db.Products.Where(x => x.ProductID == id).First();
            db.Products.Remove(item);
            db.SaveChanges();
            var item2 = db.Products.ToList();
            return View("Product", item2);
        }

        public ActionResult EditProduct(int id)
        {
            if (Session["AdminID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                var item = db.Products.Where(model => model.ProductID == id).First();
                return View(item);
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditProduct(Product editproduct, int id)
        {
            var item = db.Products.Where(model => model.ProductID == id).First();
            item.ImagePath = editproduct.ImagePath;
            item.ProductCode = editproduct.ProductCode;
            item.ProductName = editproduct.ProductName;
            item.ProductPrice = editproduct.ProductPrice;
            item.ProductContent = editproduct.ProductContent;
            item.ProductAmount = editproduct.ProductAmount;
            db.SaveChanges();
            return View();
        }
    }
}