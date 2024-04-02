using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class Contact
    {
        public int ID { get; set; }
        [StringLength(100)]
        public string Fullname { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(15)]
        public string Phone { get; set; }
        [StringLength(255)]
        public string Title { get; set; }
        [Column(TypeName = "ntext")]
        public string Detail { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? Created_at { get; set; }
        public int? Created_by { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? Updated_at { get; set; }
        public int? Updated_by { get; set; }
        public int Status { get; set; }
    }
}