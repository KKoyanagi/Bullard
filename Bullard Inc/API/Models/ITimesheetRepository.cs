﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public interface ITimesheetRepository
    {
        Timesheet InsertTimesheet(Timesheet timesheet);
        IEnumerable<Timesheet> GetTimesheets();
        Timesheet GetTimesheetById(int timesheet_id);
        IEnumerable<Timesheet> GetTimesheetsByWeek(int wk_id);
        IEnumerable<Timesheet> GetApprovedTimesheetsByWeek(int wk_id);
        IEnumerable<Timesheet> GetSubmittedTimesheetsByWeek(int wk_id);
        IEnumerable<Timesheet> GetUnapprovedTimesheetsByWeek(int wk_id);
        IEnumerable<Timesheet> GetNotsubmittedTimesheetsByWeek(int wk_id);
        IEnumerable<Timesheet> GetTimesheetsByEmp(int emp_id);
        Timesheet RemoveTimesheet(int timesheet_id);
        Timesheet UpdateTimesheet(Timesheet timesheet);
        Timesheet GetTimesheetCurrent(int emp_id);
        Timesheet SubmitTimesheet(int timesheet_id);
        Timesheet ApproveTimesheet(int timesheet_id);
        Timesheet UnApproveTimesheet(int timesheet_id);

        //void UpdateTimesheet(Timesheet timesheet);
        //void Save();
    }
}
