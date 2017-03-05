using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace API.Models
{
    public class JobRepository : IJobRepository
    {
        private ApplicationDbContext getContext;
        /*
        public JobRepository(ApplicationDbContext context)
        {
            this.context = context;
        }*/
        public JobRepository()
        {
            this.getContext = new ApplicationDbContext();
        }
        public Job GetJobById(int job_id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Jobs.Find(job_id);
            }
        }
        public IEnumerable<Job> GetJobsByEmployeeDayId(int employeeDay_id)
        {
            if (getContext != null)
            {
                getContext.Dispose();
                getContext = new ApplicationDbContext();
            }
            var jobs = from jb in getContext.Jobs
                           where jb.EmployeeDay_Id == employeeDay_id
                           select jb;
                return jobs;
            
        }

        public Job InsertJob(Job job)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Jobs.Add(job);
                context.SaveChanges();
                return job;
            }
        }

        public Job RemoveJob(int job_id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Job job = context.Jobs.Find(job_id);
                context.Jobs.Remove(job);
                context.SaveChanges();
                return job;
            }
        }
        public Job UpdateJob(Job job)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var jb = context.Jobs.Find(job.Job_Id);
                if (jb == null)
                {
                    return null;
                }
                context.Entry(jb).Property(u => u.Hours).CurrentValue = job.Hours;
                context.Entry(jb).Property(u => u.Mileage).CurrentValue = job.Mileage;
                context.Entry(jb).Property(u => u.Lunch).CurrentValue = job.Lunch;
                context.Entry(jb).Property(u => u.ActivityCode).CurrentValue = job.ActivityCode;
                context.Entry(jb).Property(u => u.Project_Id).CurrentValue = job.Project_Id;
                try
                {
                    context.SaveChanges();
                }
                catch
                {
                    context.Database.ExecuteSqlCommand("UPDATE dbo.Jobs SET Hours = {0} WHERE Job_Id = {1}", job.Hours, job.Job_Id);
                    context.Database.ExecuteSqlCommand("UPDATE dbo.Jobs SET Mileage = {0} WHERE Job_Id = {1}", job.Mileage, job.Job_Id);
                    context.Database.ExecuteSqlCommand("UPDATE dbo.Jobs SET Lunch = {0} WHERE Job_Id = {1}", job.Lunch, job.Job_Id);
                    context.Database.ExecuteSqlCommand("UPDATE dbo.Jobs SET ActivityCode = {0} WHERE Job_Id = {1}", job.ActivityCode, job.Job_Id);
                    context.Database.ExecuteSqlCommand("UPDATE dbo.Jobs SET Project_Id = {0} WHERE Job_Id = {1}", job.Project_Id, job.Job_Id);
                }
                return jb;
            }
            //Job jb = context.Jobs.Find(job.Job_Id);
            //context.Jobs.Remove(jb);
            //context.SaveChanges();
            //context.Jobs.Add(job);
            /*try
            {
                // Attempt to save changes to the database
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var databaseEntity = context.Jobs.AsNoTracking().Single(jb => jb.Job_Id == ((Job)entry.Entity).Job_Id);
                var databaseEntry = context.Entry(databaseEntity);
                foreach (var property in entry.Metadata.GetProperties())
                {
                    Debug.WriteLine("{0} {1}", entry.Property(property.Name), entry.Property(property.Name).CurrentValue);
                    databaseEntry.Property(property.Name).CurrentValue = entry.Property(property.Name).CurrentValue;
                }
                context.SaveChanges();
            }*/



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
        }*/

    }
}
