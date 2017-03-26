using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class ApprovedView
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Timesheet_Id { get; set; }
        public DateTime DateSubmitted { get; set; }
        public bool Approved { get; set; }
        public int WeekId { get; set; }
    }
}
