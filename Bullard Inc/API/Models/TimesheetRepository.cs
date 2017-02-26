using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public class TimesheetRepository : ITimesheetRepository
    {
        private ApplicationDbContext context;

        public TimesheetRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Timesheet> GetTimesheets()
        {
            return context.Timesheets.ToList();
        }
        public Timesheet GetTimesheetById(int timesheet_id)
        {
            return context.Timesheets.Find(timesheet_id);
        }
        public IEnumerable<Timesheet> GetTimesheetsByWeek(int week_id)
        {
            var timesheets = from t in context.Timesheets
                             where t.Week_Id == week_id
                             select t;
            return timesheets;
        }
        public IEnumerable<Timesheet> GetApprovedTimesheetsByWeek(int week_id)
        {
            var timesheets = from t in context.Timesheets
                             where t.Week_Id == week_id && t.Approved == true
                             select t;
            return timesheets;
        }
        public IEnumerable<Timesheet> GetSubmittedTimesheetsByWeek(int week_id)
        {
            var timesheets = from t in context.Timesheets
                             where t.Week_Id == week_id && t.Submitted == true
                             select t;
            return timesheets;
        }
        public IEnumerable<Timesheet> GetUnapprovedTimesheetsByWeek(int week_id)
        {
            var timesheets = from t in context.Timesheets
                             where t.Week_Id == week_id && t.Approved == false
                             select t;
            return timesheets;
        }
        public IEnumerable<Timesheet> GetNotsubmittedTimesheetsByWeek(int week_id)
        {
            var timesheets = from t in context.Timesheets
                             where t.Week_Id == week_id && t.Submitted == false
                             select t;
            return timesheets;
        }
        public IEnumerable<Timesheet> GetTimesheetsByEmp(int emp_id)
        {
            var timesheets = from t in context.Timesheets
                             where t.Emp_Id == emp_id
                             select t;
            return timesheets;
        }
        public void InsertTimesheet(Timesheet timesheet)
        {
            context.Timesheets.Add(timesheet);
        }
        public Timesheet RemoveTimesheet(int timesheet_id)
        {
            Timesheet timesheet = context.Timesheets.Find(timesheet_id);
            context.Timesheets.Remove(timesheet);
            return timesheet;
        }
        public void UpdateTimehseet(Timesheet timesheet)
        {
            context.Entry(timesheet).State = EntityState.Modified;
        }
        public void Save()
        {
            context.SaveChanges();
        }
        //Not sure why we need this yet or how to use
        //Believe it is for cleaning up context once no longer needed
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
