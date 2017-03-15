using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class PendingViewRepository : IPendingViewRepository
    {
        public List<PendingView> GetPendingViews(int week_id)
        {
            TimesheetRepository timesheetsRepo = new TimesheetRepository();
            EmployeeRepository employeeRepo = new EmployeeRepository();
            JobRepository jobRepo = new JobRepository();
            EmployeeDayRepository empDayRepo = new EmployeeDayRepository();
            
            List<PendingView> views = new List<PendingView>();
            IEnumerable<Timesheet> timesheets = timesheetsRepo.GetSubmittedTimesheetsByWeek(week_id);
            foreach(var ts in timesheets)
            {
                PendingView tmp = new PendingView();
                List<Job> jobs = new List<Job>();
                var employee = employeeRepo.GetEmployeeById(ts.Emp_Id);
                tmp.FirstName = employee.FirstName;
                tmp.LastName = employee.LastName;
                tmp.Timesheet_Id = ts.Timesheet_Id;
                tmp.DateSubmitted = ts.DateSubmitted;
                tmp.Approved = ts.Approved;
                List<EmployeeDay> empDays = empDayRepo.GetEmployeeDaysByTimesheet(ts.Timesheet_Id).ToList();
                tmp.EmpDays = empDays;
                if (ts.Submitted == true && ts.Approved == false)
                {
                    foreach (var day in empDays)
                    {
                        jobs.AddRange(jobRepo.GetJobsByEmployeeDayId(day.EmployeeDay_Id));
                    }
                    tmp.Jobs = jobs;
                }
                views.Add(tmp);
            }
            return views;
        }
    }
}
