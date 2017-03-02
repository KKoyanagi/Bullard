using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
    
    
}
