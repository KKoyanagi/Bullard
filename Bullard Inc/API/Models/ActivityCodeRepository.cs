using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace API.Models
{
    public class ActivityCodeRepository : IActivityCodeRepository
    {
        /*private ApplicationDbContext context;

        public ActivityCodeRepository(ApplicationDbContext context)
        {
            this.context = context;
        }*/
        public ActivityCodeRepository() { }
        public IEnumerable<ActivityCode> GetActivityCodes()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.ActivityCodes.ToList();
            }
        }
    }
}
