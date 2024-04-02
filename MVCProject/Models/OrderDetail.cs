using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class OrderDetail
    {
        [Key]
        public int ID { get; set; }
        public int Orderid { get; set; }
        public int Productid { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int PriceSale { get; set; }
        public double Amount { get; set; }
    }
}