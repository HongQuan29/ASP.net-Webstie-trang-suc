using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace MVCProject.Models
{
    public class Product
    {
        public int ID { get; set; }
        public int CatId { get; set; }
        public int Submenu { get; set; }
        public string Name { get; set; }
        [StringLength(255)]
        public string Slug { get; set; }
        [StringLength(100)]
        public string Img { get; set; }
        [Column(TypeName = "ntext")]
        public string Detail { get; set; }
        public int Number { get; set; }
        public double Price { get; set; }
        public double Pricesale { get; set; }
        [StringLength(150)]
        public string Metakey { get; set; }
        public string Metadesc { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime Created_at { get; set; }
        public int Created_by { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime Updated_at { get; set; }
        public int Updated_by { get; set; }
        public int Status { get; set; }
        public int Sold { get; set; }
    }
}