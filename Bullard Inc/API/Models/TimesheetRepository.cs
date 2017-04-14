using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace API.Models
{
    public class TimesheetRepository : ITimesheetRepository
    {
        private ApplicationDbContext getContext;

        /*public TimesheetRepository(ApplicationDbContext context)
        {
            this.getContext = context;
        }*/
        public TimesheetRepository()
        {
            this.getContext = new ApplicationDbContext();
        }
        public IEnumerable<Timesheet> GetTimesheets()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Timesheets.ToList();
            }
        }
        public Timesheet GetTimesheetById(int timesheet_id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Timesheets.Find(timesheet_id);
            }
        }
        public IEnumerable<Timesheet> GetTimesheetsByWeek(int week_id)
        {
            if (getContext != null)
            {
                getContext.Dispose();
                getContext = new ApplicationDbContext();
            }
            //IQueryable<Timesheet> timesheets;
            var timesheets = from t in getContext.Timesheets
                                 where t.Week_Id == week_id
                                 select t;
                return timesheets;
            
        }
        public Timesheet GetEmpTimesheetByWeek(int emp_id, int week_id)
        {
            if (getContext != null)
            {
                getContext.Dispose();
                getContext = new ApplicationDbContext();
            }
            //IQueryable<Timesheet> timesheets;
            var timesheets = from t in getContext.Timesheets
                             where t.Week_Id == week_id && t.Emp_Id == emp_id
                             select t;
            if( timesheets.Any()){
                return timesheets.First<Timesheet>();
            }
            else
            {
                return null;
            }

        }
        public IEnumerable<Timesheet> GetApprovedTimesheetsByWeek(int week_id)
        {
            if (getContext != null)
            {
                getContext.Dispose();
                getContext = new ApplicationDbContext();
            }
            var timesheets = from t in getContext.Timesheets
                                 where t.Week_Id == week_id && t.Approved == true && t.Submitted == true
                                 select t;
                return timesheets;
            
            
        }
        public IEnumerable<Timesheet> GetSubmittedTimesheetsByWeek(int week_id)
        {
            if (getContext != null)
            {
                getContext.Dispose();
                getContext = new ApplicationDbContext();
            }
            var timesheets = from t in getContext.Timesheets
                                 where t.Week_Id == week_id && t.Submitted == true && t.Approved == false
                                 select t;
                return timesheets;
            
        }
        public IEnumerable<Timesheet> GetUnapprovedTimesheetsByWeek(int week_id)
        {
            if (getContext != null)
            {
                getContext.Dispose();
                getContext = new ApplicationDbContext();
            }

            var timesheets = from t in getContext.Timesheets
                                 where t.Week_Id == week_id && t.Approved == false
                                 select t;
                return timesheets;
            
        }
        public IEnumerable<Timesheet> GetUnapprovedTimesheets()
        {
            if (getContext != null)
            {
                getContext.Dispose();
                getContext = new ApplicationDbContext();
            }

            var timesheets = from t in getContext.Timesheets
                             where t.Approved == false
                             select t;
            return timesheets;

        }
        public IEnumerable<Timesheet> GetNotsubmittedTimesheetsByWeek(int week_id)
        {
            if (getContext != null)
            {
                getContext.Dispose();
                getContext = new ApplicationDbContext();
            }
            var timesheets = from t in getContext.Timesheets
                                 where t.Week_Id == week_id && t.Submitted == false
                                 select t;
                return timesheets;
            
        }
        public IEnumerable<Timesheet> GetTimesheetsByEmp(int emp_id)
        {
            if (getContext != null)
            {
                getContext.Dispose();
                getContext = new ApplicationDbContext();
            }
            var timesheets = from t in getContext.Timesheets
                                 where t.Emp_Id == emp_id
                                 select t;
                return timesheets;
            
        }

        public Timesheet GetTimesheetCurrent(int emp_id)
        {
            if (getContext != null)
            {
                getContext.Dispose();
                getContext = new ApplicationDbContext();
            }
            
                var week = getContext.WorkWeeks.Last();
                var ts = from td in getContext.Timesheets
                         where td.Week_Id == week.Week_Id && td.Emp_Id == emp_id
                         select td;
                if (ts.Any())
                {
                    Debug.WriteLine("testing");
                    return ts.First();
                }
                else
                {
                    Timesheet timesheet = new Timesheet();
                    timesheet.Week_Id = week.Week_Id;
                    timesheet.Emp_Id = emp_id;
                    timesheet.Approved = false;
                    timesheet.Submitted = false;
                    getContext.Timesheets.Add(timesheet);
                    getContext.SaveChanges();
                    return timesheet;
                }
            
        }

        // Checks if there are any timesheets with this emp ID and week ID
        // otherwise create new one 
        public Timesheet InsertTimesheet(Timesheet timesheet)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var ts = from td in getContext.Timesheets
                         where td.Emp_Id == timesheet.Emp_Id && td.Week_Id == timesheet.Week_Id
                         select td;
                if (ts.Any())
                {
                    return ts.First();
                }
                else
                {
                    context.Timesheets.Add(timesheet);
                    context.SaveChanges();
                    return timesheet;
                }
            }
        }
        public Timesheet RemoveTimesheet(int timesheet_id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Timesheet timesheet = context.Timesheets.Find(timesheet_id);
                context.Timesheets.Remove(timesheet);
                context.SaveChanges();
                return timesheet;
            }
        }
        public Timesheet UpdateTimesheet(Timesheet timesheet)
        {
            /*Timesheet ts;
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                ts = context.Timesheets.SingleOrDefault(x => x.Timesheet_Id == timesheet.Timesheet_Id);
                if (ts == null)
                {
                    return null;
                }
            }
            ts.Approved = timesheet.Approved;
            ts.Submitted = timesheet.Submitted;*/
            if (getContext != null)
            {
                getContext.Dispose();
                getContext = new ApplicationDbContext();
            }
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var ts = context.Timesheets.Find(timesheet.Timesheet_Id);
                //context.Entry(timesheet).State = EntityState.Modified;
                if(ts == null)
                {
                    return null;
                }
                context.Entry(ts).Property(u => u.Approved).CurrentValue = timesheet.Approved;
                //context.Entry(ts).Property(u => u.Approved).OriginalValue = timesheet.Approved;
                context.Entry(ts).Property(u => u.Submitted).CurrentValue = timesheet.Submitted;
                //context.Entry(ts).Property(u => u.Submitted).OriginalValue = timesheet.Submitted;*/
                //context.Entry(ts).Property(u => u.Week_Id).CurrentValue = timesheet.Week_Id;
                try
                {
                    context.SaveChanges();
                }
                catch(Exception ex)
                {
                    context.Database.ExecuteSqlCommand("UPDATE dbo.Timesheets SET Approved = {0} WHERE Timesheet_Id = {1}", timesheet.Approved, timesheet.Timesheet_Id);
                    context.Database.ExecuteSqlCommand("UPDATE dbo.Timesheets SET Submitted = {0} WHERE Timesheet_Id = {1}",timesheet.Submitted,timesheet.Timesheet_Id);
                    //context.Database.ExecuteSqlCommand("UPDATE dbo.Timesheets SET Week_Id = {0} WHERE Timesheet_Id = {1}", timesheet.Week_Id, timesheet.Timesheet_Id);
                    Debug.WriteLine(ex.ToString());
                }
                return timesheet;
            }
        }
        public Timesheet SubmitTimesheet(int timesheet_id)
        {
            if (getContext != null)
            {
                getContext.Dispose();
                getContext = new ApplicationDbContext();
            }
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var timesheet = context.Timesheets.Find(timesheet_id);
                if (timesheet == null)
                {
                    return null;
                }
                context.Entry(timesheet).Property(u => u.Submitted).CurrentValue = true;
                context.Entry(timesheet).Property(u => u.DateSubmitted).CurrentValue = DateTime.Now;
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {

                    context.Database.ExecuteSqlCommand("UPDATE dbo.Timesheets SET Submitted = {0} WHERE Timesheet_Id = {1}", true,  timesheet_id);
                    context.Database.ExecuteSqlCommand("UPDATE dbo.Timesheets SET DateSubmitted = {0} WHERE Timesheet_Id = {1}", DateTime.Now, timesheet.Timesheet_Id);
                    Debug.WriteLine(ex.ToString());
                }
                return timesheet;
            }
        }
        public Timesheet UnSubmitTimesheet(int timesheet_id)
        {
            if (getContext != null)
            {
                getContext.Dispose();
                getContext = new ApplicationDbContext();
            }
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var timesheet = context.Timesheets.Find(timesheet_id);
                if (timesheet == null)
                {
                    return null;
                }
                context.Entry(timesheet).Property(u => u.Submitted).CurrentValue = false;
                //context.Entry(timesheet).Property(u => u.DateSubmitted).CurrentValue = DateTime.Now;
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {

                    context.Database.ExecuteSqlCommand("UPDATE dbo.Timesheets SET Submitted = {0} WHERE Timesheet_Id = {1}", false, timesheet_id);
                    //context.Database.ExecuteSqlCommand("UPDATE dbo.Timesheets SET DateSubmitted = {0} WHERE Timesheet_Id = {1}", DateTime.Now, timesheet.Timesheet_Id);
                    Debug.WriteLine(ex.ToString());
                }
                return timesheet;
            }
        }
        public Timesheet ApproveTimesheet(int timesheet_id)
        {
            if (getContext != null)
            {
                getContext.Dispose();
                getContext = new ApplicationDbContext();
            }
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var timesheet = context.Timesheets.Find(timesheet_id);
                if (timesheet == null)
                {
                    return null;
                }
                context.Entry(timesheet).Property(u => u.Approved).CurrentValue = true;
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {

                    context.Database.ExecuteSqlCommand("UPDATE dbo.Timesheets SET Approved = {0} WHERE Timesheet_Id = {1}", true, timesheet_id);
                    //context.Database.ExecuteSqlCommand("UPDATE dbo.Timesheets SET Week_Id = {0} WHERE Timesheet_Id = {1}", timesheet.Week_Id, timesheet.Timesheet_Id);
                    Debug.WriteLine(ex.ToString());
                }
                return timesheet;
            }
        }
        public Timesheet UnApproveTimesheet(int timesheet_id)
        {
            if (getContext != null)
            {
                getContext.Dispose();
                getContext = new ApplicationDbContext();
            }
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var timesheet = context.Timesheets.Find(timesheet_id);
                if (timesheet == null)
                {
                    return null;
                }
                context.Entry(timesheet).Property(u => u.Approved).CurrentValue = false;
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {

                    context.Database.ExecuteSqlCommand("UPDATE dbo.Timesheets SET Approved = {0} WHERE Timesheet_Id = {1}", false, timesheet_id);
                    //context.Database.ExecuteSqlCommand("UPDATE dbo.Timesheets SET Week_Id = {0} WHERE Timesheet_Id = {1}", timesheet.Week_Id, timesheet.Timesheet_Id);
                    Debug.WriteLine(ex.ToString());
                }
                return timesheet;
            }
        }
        /*public void Save()
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
        */

    }
}
