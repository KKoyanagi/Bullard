using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace API.Models
{
    public class Job
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int job_Id { get; set; }
        [ForeignKey("EmployeeDay")]
        public int employeeDay_Id { get; set; }
        //Figure out composite key
        [ForeignKey("Project")]
        public int project_Id { get; set; }
        public int activityCode { get; set; }
        [Range(0, 24)]
        public double hours { get; set; }
        public int mileage { get; set; }
        public double lunch { get; set; }
    }
}
