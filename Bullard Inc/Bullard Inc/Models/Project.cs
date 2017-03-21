using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Bullard_Inc.Models
{
    public class Project
    {
        [Key]
        public int Project_Id { get; set; }
        //[Unique]
        //[Required]
        public int Project_Num { get; set; }
        [StringLength(50)]
        public string Location { get; set; }
        [StringLength(50)]
        public string Address { get; set; }
    }
}
