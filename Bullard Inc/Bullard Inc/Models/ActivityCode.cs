using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Bullard_Inc.Models
{
    public class ActivityCode
    {
        [Key]
        public int ActivityCode_Id { get; set; }
        [StringLength(50)]
        public string ActivityDescription { get; set; }
    }
}
