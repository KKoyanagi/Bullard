using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public class WeekRepository : IWeekRepository
    {
        private ApplicationDbContext context;

        public WeekRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<WorkWeek> GetWorkWeeks()
        {
            return context.WorkWeeks.ToList();
        }
        public WorkWeek GetWeekById(int week_id)
        {
            return context.WorkWeeks.Find(week_id);
        }
        public WorkWeek InsertWeek(WorkWeek week)
        {
            context.WorkWeeks.Add(week);
            return week;
        }
        public WorkWeek RemoveWeek(int week_id)
        {
            WorkWeek week = context.WorkWeeks.Find(week_id);
            context.WorkWeeks.Remove(week);
            return week;
        }
        public void UpdateWeek(WorkWeek week)
        {
            context.Entry(week).State = EntityState.Modified;
        }

    }
    
}
