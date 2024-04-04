using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class Topic
    {
        public int ID { get; set; }


        [StringLength(255)]
        public string name { get; set; }


        [StringLength(255)]
        public string slug { get; set; }

        public int parentid { get; set; }

        public int orders { get; set; }

        [StringLength(150)]
        public string metakey { get; set; }

        public string metadesc { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime created_at { get; set; }

        public int created_by { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime updated_at { get; set; }

        public int updated_by { get; set; }

        public int status { get; set; }
    }
}