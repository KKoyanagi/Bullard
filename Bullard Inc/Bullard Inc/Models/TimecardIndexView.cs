using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bullard_Inc.Models
{
    public class TimecardIndexView
    {
        public WorkWeek Current_Week;
        public IEnumerable<WorkWeek> Weeks;
        public Timesheet Timesheet;
    }
}