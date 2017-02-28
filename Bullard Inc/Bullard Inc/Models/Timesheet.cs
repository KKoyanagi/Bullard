using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Bullard_Inc.Models
{
    public class Timesheet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int Timesheet_Id { get; set; }
        [ForeignKey("WorkWeek")]
        public int Week_Id { get; set; }
        [ForeignKey("Employee")]
        public int Emp_Id { get; set; }
        [Range(0,1)]
        public Boolean Approved { get; set; }
        [Range(0, 1)]
        public Boolean Submitted { get; set; }
    }
}
