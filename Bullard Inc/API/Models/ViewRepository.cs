using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class ViewRepository : IViewRepository
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
                tmp.WeekId = ts.Week_Id;
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

        public List<ApprovedView> GetApprovedViews(int week_id)
        {
            /*TimesheetRepository timesheetsRepo = new TimesheetRepository();
            EmployeeRepository employeeRepo = new EmployeeRepository();
            

            List<ApprovedView> views = new List<ApprovedView>();
            IEnumerable<Timesheet> timesheets = timesheetsRepo.GetApprovedTimesheetsByWeek(week_id);
            foreach (var ts in timesheets)
            {
                ApprovedView tmp = new ApprovedView();
                var employee = employeeRepo.GetEmployeeById(ts.Emp_Id);
                tmp.FirstName = employee.FirstName;
                tmp.LastName = employee.LastName;
                tmp.Timesheet_Id = ts.Timesheet_Id;
                tmp.DateSubmitted = ts.DateSubmitted;
                tmp.Approved = ts.Approved;
                tmp.WeekId = ts.Week_Id;
                
                views.Add(tmp);
            }
            return views;*/
            TimesheetRepository timesheetsRepo = new TimesheetRepository();
            EmployeeRepository employeeRepo = new EmployeeRepository();
            JobRepository jobRepo = new JobRepository();
            EmployeeDayRepository empDayRepo = new EmployeeDayRepository();

            List<ApprovedView> views = new List<ApprovedView>();
            IEnumerable<Timesheet> timesheets = timesheetsRepo.GetApprovedTimesheetsByWeek(week_id);
            foreach (var ts in timesheets)
            {
                ApprovedView tmp = new ApprovedView();
                List<Job> jobs = new List<Job>();
                var employee = employeeRepo.GetEmployeeById(ts.Emp_Id);
                tmp.FirstName = employee.FirstName;
                tmp.LastName = employee.LastName;
                tmp.Timesheet_Id = ts.Timesheet_Id;
                tmp.DateSubmitted = ts.DateSubmitted;
                tmp.Approved = ts.Approved;
                List<EmployeeDay> empDays = empDayRepo.GetEmployeeDaysByTimesheet(ts.Timesheet_Id).ToList();
                tmp.EmpDays = empDays;
                tmp.WeekId = ts.Week_Id;
                if (ts.Submitted == true && ts.Approved == true)
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

        public List<PastDueView> GetPastDueViews(int week_id)
        {
            TimesheetRepository timesheetsRepo = new TimesheetRepository();
            EmployeeRepository employeeRepo = new EmployeeRepository();
            WorkWeekRepository weekRepo = new WorkWeekRepository();

            List<PastDueView> views = new List<PastDueView>();
            IEnumerable<Timesheet> timesheets = timesheetsRepo.GetUnapprovedTimesheets();
            foreach (var ts in timesheets)
            {
                DateTime current = DateTime.Now;
                if ((current.DayOfWeek.ToString() == "Friday" && DateTime.Now.ToString("tt") == "PM") || current.DayOfWeek.ToString()=="Saturday")
                {
                    if (ts.Week_Id <= week_id)
                    {
                        PastDueView tmp = new PastDueView();
                        var employee = employeeRepo.GetEmployeeById(ts.Emp_Id);
                        var week = weekRepo.GetWeekById(ts.Week_Id);
                        tmp.FirstName = employee.FirstName;
                        tmp.LastName = employee.LastName;
                        tmp.Timesheet_Id = ts.Timesheet_Id;
                        tmp.StartDate = week.StartDate;
                        tmp.EndDate = week.EndDate;
                        tmp.Submitted = ts.Submitted;

                        views.Add(tmp);
                    }
                }
                else
                {
                    if (ts.Week_Id < week_id)
                    {
                        PastDueView tmp = new PastDueView();
                        var employee = employeeRepo.GetEmployeeById(ts.Emp_Id);
                        var week = weekRepo.GetWeekById(ts.Week_Id);
                        tmp.FirstName = employee.FirstName;
                        tmp.LastName = employee.LastName;
                        tmp.Timesheet_Id = ts.Timesheet_Id;
                        tmp.StartDate = week.StartDate;
                        tmp.EndDate = week.EndDate;
                        tmp.Submitted = ts.Submitted;

                        views.Add(tmp);
                    }
                }
                
                
            }
            return views;
        }
    }
}
