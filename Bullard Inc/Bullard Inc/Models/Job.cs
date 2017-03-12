using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Bullard_Inc.Models
{
    public class Job
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int Job_Id { get; set; }
        //[ForeignKey("EmployeeDay")]
        public int EmployeeDay_Id { get; set; }
        //Figure out composite key
        //[ForeignKey("Project")]
        public int Project_Id { get; set; }
        public int ActivityCode { get; set; }
        [Range(0, 24)]
        public double Hours { get; set; }
        public int Mileage { get; set; }
        public double Lunch { get; set; }
    }
}
