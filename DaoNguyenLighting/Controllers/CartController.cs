using DaoNguyenLighting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaoNguyenLighting.Controllers
{
    public class CartController : Controller
    {
        private const string CartSession = "CartSession";
        private DNLTEntities db = new DNLTEntities();

        // GET: Cart
        public ActionResult Cart()
        {
            return View();
          
        }

        public ActionResult AddToCart(int? id)
        {
           
            if (id == null)
            {
                return View("Cart");
            }

            if (Session[CartSession] == null)
            {
                List<CartItem> list = new List<CartItem>
                {
                    new CartItem(db.Products.Find(id),1)
                };
                Session[CartSession] = list;
            }
            else
            {
                List<CartItem> list = (List<CartItem>)Session[CartSession];
                if (list.Exists(x => x.Product.ProductID == id) && ModelState.IsValid)
                {
                    foreach (var item in list)
                    {
                        if (item.Product.ProductID == id)
                        {
                            item.Quantity++;
                        }
                    }
                }
                else
                {
                    list.Add(new CartItem(db.Products.Find(id), 1));
                    Session[CartSession] = list;
                }
               
            }

            return View("Cart");
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            else
            {
                List<CartItem> list = (List<CartItem>)Session[CartSession];
                list.RemoveAll(x => x.Product.ProductID == id);
                Session[CartSession] = list;
                return View("Cart");
            }
          
        }

        public ActionResult Pay()
        {
            return View();
        }

        public ActionResult Update(FormCollection fc)
        {
            string[] quantities = fc.GetValues("quantity");
            List<CartItem> list = (List<CartItem>)Session[CartSession];
            for (int i = 0; i < list.Count(); i++)
                list[i].Quantity = Convert.ToInt32(quantities[i]);
            Session[CartSession] = list;
            return View("Cart");
        }

        [HttpPost]
        public ActionResult Pay(Order order, OrderDetail detail, FormCollection fc)
        {
            string[] quantities = fc.GetValues("quantity");
            List<CartItem> list = (List<CartItem>)Session[CartSession];
            

            if (Session["UserID"] != null)
            {
                var userID = Convert.ToInt32(Session["UserID"]);
                var userList = db.Users.Where(x => x.UserID == userID).First();
                order.UserID = userID;
                order.Name = userList.Name;
                order.Date = DateTime.Today;
                order.Status = "Processing";    
                db.Orders.Add(order);           
                for (int i = 0; i < list.Count(); i++)
                {
                    list[i].Quantity = Convert.ToInt32(quantities[i]);
                }                 
                Session[CartSession] = list;
                order.OrderTotal = Convert.ToString(list.Sum(x => x.Quantity * x.Product.ProductPrice));
                db.SaveChanges();

                foreach (var item in list)
                {
                    detail.ProductID = item.Product.ProductID;
                    detail.OrderID = order.OrderID;
                    detail.NameProduct = item.Product.ProductName;
                    detail.Quantity = item.Quantity;
                    detail.Price = item.Product.ProductPrice;
                    db.OrderDetails.Add(detail);
                    db.SaveChanges();
                }
                Session[CartSession] = null;
            }
           

            return View();


        }
    }
}