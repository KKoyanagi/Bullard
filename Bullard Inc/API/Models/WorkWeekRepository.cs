using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public class WorkWeekRepository : IWorkWeekRepository
    {
        /*private ApplicationDbContext context;

        public WeekRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        */
        public WorkWeekRepository() { }
        public IEnumerable<WorkWeek> GetWorkWeeks()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.WorkWeeks.ToList();
            }
        }
        public WorkWeek GetWeekById(int week_id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.WorkWeeks.Find(week_id);
            }
        }
        public WorkWeek InsertWeek(WorkWeek week)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.WorkWeeks.Add(week);
                context.SaveChanges();
                return week;
            }
        }
        public WorkWeek RemoveWeek(int week_id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                WorkWeek week = context.WorkWeeks.Find(week_id);
                context.WorkWeeks.Remove(week);
                context.SaveChanges();
                return week;
            }
        }
        public WorkWeek UpdateWeek(WorkWeek week)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var wk = context.WorkWeeks.Find(week.Week_Id);
                if (wk == null)
                {
                    return null;
                }
                context.Entry(wk).Property(u => u.StartDate).CurrentValue = week.StartDate;
                context.Entry(wk).Property(u => u.EndDate).CurrentValue = week.EndDate;
                try
                {
                    context.SaveChanges();
                }
                catch
                {
                    context.Database.ExecuteSqlCommand("UPDATE dbo.WorkWeeks SET StartDate = {0} WHERE Week_Id = {1}", week.StartDate, week.Week_Id);
                    context.Database.ExecuteSqlCommand("UPDATE dbo.WorkWeeks SET EndDate = {0} WHERE Week_Id = {1}", week.EndDate, week.Week_Id);
                }
                return week;
            }
        }

    }
    
}
