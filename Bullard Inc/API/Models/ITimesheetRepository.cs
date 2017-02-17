using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public interface ITimesheetRepository
    {
        void InsertTimesheet(Timesheet timesheet);
        IEnumerable<Timesheet> GetTimesheets();
        Timesheet GetTimesheetById(int timesheet_id);
        IEnumerable<Timesheet> GetTimesheetsByWeek(int wk_id);
        IEnumerable<Timesheet> GetTimesheetsByEmp(int emp_id);
        Timesheet RemoveTimesheet(int timesheet_id);
        //void UpdateTimesheet(Timesheet timesheet);
        void Save();
    }
}
