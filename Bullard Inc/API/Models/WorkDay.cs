using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class WorkDay
    {
        [Key]
        public int WorkDay_Id { get; set; }
        [StringLength(10)]
        public string Day_Name { get; set; }

    }
}
