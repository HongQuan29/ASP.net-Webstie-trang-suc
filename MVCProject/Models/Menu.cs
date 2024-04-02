using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class Menu
    {
        public int ID { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Type { get; set; }
        [StringLength(255)]
        public string Link { get; set; }
        public int? Tableid { get; set; }
        public int Parentid { get; set; }
        public int Orders { get; set; }
        [StringLength(255)]
        public string Position { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime Created_at { get; set; }
        public int? Created_by { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime Updated_at { get; set; }
        public int? Updated_by { get; set; }
        public int Status { get; set; }
    }
}