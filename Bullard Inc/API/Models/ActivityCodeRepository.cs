using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;


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

        public ActivityCode InsertActivityCode(ActivityCode activityCode)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.ActivityCodes.Add(activityCode);
                context.SaveChanges();
                return activityCode;
            }
        }

        public ActivityCode RemoveActivityCode(int activityCode_id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                ActivityCode activityCode = context.ActivityCodes.Find(activityCode_id);
                context.ActivityCodes.Remove(activityCode);
                context.SaveChanges();
                return activityCode;
            }
        }

        public ActivityCode UpdateActivityCode(ActivityCode activityCode)
        {

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var a = context.ActivityCodes.Find(activityCode.ActivityCode_Id);
                if (a == null)
                {
                    return null;
                }
                context.Entry(a).Property(u => u.ActivityDescription).CurrentValue = activityCode.ActivityDescription;
                try
                {
                    context.SaveChanges();
                }
                catch
                {
                    context.Database.ExecuteSqlCommand("UPDATE dbo.ActivityCodes SET ActivityDescription = {0} WHERE Project_Id = {1}", activityCode.ActivityDescription, activityCode.ActivityCode_Id);
                }
                return a;
            }
        }
    }
}
