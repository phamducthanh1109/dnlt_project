using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaoNguyenLighting.Models
{
    public class MergeModel
    {
        public User UserList { get; set; }
        public Order OrderList { get; set; }
        public List<OrderDetail> OrderDetailList { get; set; }
    }
}