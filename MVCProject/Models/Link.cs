using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class Link
    {
        [Key]
        public int ID { get; set; }
        public string Slug { get; set; }
        public int TableId { get; set; }
        public string Type { get; set; }
        public int ParentId { get; set; }
    }
}