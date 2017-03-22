using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bullard_Inc.Models
{
    public class ApprovedView
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Timesheet_Id { get; set; }
        public DateTime DateSubmitted { get; set; }
        public bool Approved { get; set; }
    }
}
