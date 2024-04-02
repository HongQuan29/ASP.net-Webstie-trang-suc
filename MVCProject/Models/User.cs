using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class User
    {
        public int ID { get; set; }
        [StringLength(255)]
        public string Fullname { get; set; }
        [StringLength(225)]
        public string Username { get; set; }
        [StringLength(64)]
        public string Password { get; set; }
        [StringLength(255)]
        public string Email { get; set; }
        [StringLength(5)]
        public string Gender { get; set; }
        [StringLength(20)]
        public string Phone { get; set; }
        [StringLength(100)]
        public string Img { get; set; }
        public int Access { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime Created_at { get; set; }
        public int Created_by { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime Updated_at { get; set; }
        public int Updated_by { get; set; }
        public int Status { get; set; }
    }
}