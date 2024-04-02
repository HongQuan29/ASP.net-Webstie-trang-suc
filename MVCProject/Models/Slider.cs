using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class Slider
    {
        public int ID { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Url { get; set; }
        [StringLength(100)]
        public string Position { get; set; }
        [StringLength(100)]
        public string Img { get; set; }
        public int? Orders { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime Created_at { get; set; }
        public int? Created_by { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime Updated_at { get; set; }
        public int? Updated_by { get; set; }
        public int Status { get; set; }
    }
}