using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bullard_Inc.Models
{
    public class Timecard_EmpJobAddEdit
    {
        public Job Job { get; set; }
        public List<ActivityCode> ActivityCodes { get; set; }
        public List<Project> Projects { get; set; }
    }
}
