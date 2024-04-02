using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class Role
    {
        public int ID { get; set; }
        public int ParentId { get; set; }
        [StringLength(255)]
        public string AccessName { get; set; }
        [StringLength(225)]
        public string Description { get; set; }
        public string GroupID { get; set; }
    }
}