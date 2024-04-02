using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class Order
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public int Userid { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime Created_ate { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? Exportdate { get; set; }
        [StringLength(255)]
        public string Deliveryaddress { get; set; }
        [StringLength(100)]
        public string Deliveryname { get; set; }
        [StringLength(255)]
        public string Deliveryphone { get; set; }
        [StringLength(255)]
        public string Deliveryemail { get; set; }
        public string DeliveryPaymentMethod { get; set; }
        public int StatusPayment { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime Updated_at { get; set; }
        public int? Updated_by { get; set; }
        public int Status { get; set; }
    }
}