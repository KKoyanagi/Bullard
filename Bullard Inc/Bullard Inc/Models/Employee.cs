using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Bullard_Inc.Models
{
    public class Employee
    {
        [Key]
        public int Emp_Id { get; set; }
        [StringLength(50)]
        public string AccountName { get; set; }
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(25)]
        public string Phone { get; set; }
        //Look in to data type phone and email
    }

    public class EmpTimesheetsView : Employee
    {
        
        public List<Timesheet> Timesheets { get; set; }
    }
}
