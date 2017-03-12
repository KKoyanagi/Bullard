using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;


namespace Bullard_Inc.Models
{
    public class EmployeeDay
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int EmployeeDay_Id { get; set; }
        //[ForeignKey("Timesheet")]
        public int Timesheet_Id { get; set; }
        //Figure out composite key
        //[ForeignKey("WorkDay")]
        public int Day_Id { get; set; }
        
    }

    //public class EmployeeDayMap : EntityTypeConfiguration<EmployeeDay>
}

