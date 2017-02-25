using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class ActivityCode
    {
        [Key]
        public int ActivityCode_Id { get; set; }
        [StringLength(50)]
        public string ActivityDescription { get; set; }
    }
}
