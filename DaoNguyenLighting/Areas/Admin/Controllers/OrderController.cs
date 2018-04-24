using DaoNguyenLighting.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace DaoNguyenLighting.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        private DNLTEntities db = new DNLTEntities();
        // GET: Admin/Order

        public ActionResult Order()
        {
            if (Session["AdminID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                List<Order> list = new List<Order>();
                list = db.Orders.ToList();
                return View(list);
            }

        }

        [HttpPost]
        public ActionResult OrderDetail(MergeModel model)
        {
            var item = db.Orders.Where(x => x.OrderID == model.OrderList.OrderID).First();
            int oID = item.OrderID;
            int uID = Convert.ToInt32(item.UserID);
            if (item.Status == "Processing")
            {
                item.Status = "Complete";
            }
            else if (item.Status == "Complete")
            {
                item.Status = "Processing";
            }
            db.SaveChanges();
            return RedirectToAction("OrderDetail", "Order", new { OrderID = oID, UserID = uID });
        }

        public ActionResult OrderDetail(int OrderID, int UserID)
        {
            if (Session["AdminID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                MergeModel viewmodel = new MergeModel();
                viewmodel.OrderDetailList = db.OrderDetails.Where(x => x.OrderID == OrderID).ToList();
                viewmodel.UserList = db.Users.Where(y => y.UserID == UserID).First();
                viewmodel.OrderList = db.Orders.Where(z => z.OrderID == OrderID).First();
                return View(viewmodel);
            }
        }

        public ActionResult ExportToExcel(int OrderID, int UserID)
        {
            MergeModel viewmodel = new MergeModel();
            //List<OrderDetail> list = new List<OrderDetail>();
            viewmodel.OrderDetailList = db.OrderDetails.Where(x => x.OrderID == OrderID).ToList();
            viewmodel.UserList = db.Users.Where(y => y.UserID == UserID).First();

            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Worksheets.Add("Sheet1");

                var headerRow = new List<string[]>()
                  {
                    new string[] { "Order ID", "Name Product", "Quantity", "Price", "Total" }
                  };

                string headerRange = "A1:" + Char.ConvertFromUtf32(headerRow[0].Length + 64) + "1";
                var ws = excel.Workbook.Worksheets["Sheet1"];
                ws.Cells[headerRange].LoadFromArrays(headerRow);
                FileInfo excelFile = new FileInfo(@"C:\Users\Admin-PC\Desktop\Order.xlsx");
                for (int i = 0; i < viewmodel.OrderDetailList.Count(); i++)
                {
                    ws.Cells[string.Format("A{0}", 2 + i)].Value = i + 1;
                    ws.Cells[string.Format("B{0}", 2 + i)].Value = viewmodel.OrderDetailList[i].NameProduct;
                    ws.Cells[string.Format("C{0}", 2 + i)].Value = viewmodel.OrderDetailList[i].Quantity;
                    ws.Cells[string.Format("D{0}", 2 + i)].Value = viewmodel.OrderDetailList[i].Price;
                    ws.Cells[string.Format("E{0}", 2 + i)].Value = viewmodel.OrderDetailList[i].Price * viewmodel.OrderDetailList[i].Quantity;
                }


                excel.SaveAs(excelFile);
            }

            return RedirectToAction("Order");
        }
    }
}