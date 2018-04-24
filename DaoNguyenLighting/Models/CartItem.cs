using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaoNguyenLighting.Models
{
    [Serializable]
    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public CartItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
    }
}