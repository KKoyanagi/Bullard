using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class PendingView
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Timesheet_Id { get; set; }
        public DateTime DateSubmitted { get; set; }
        public List<Job> Jobs { get; set; }
    }
}
