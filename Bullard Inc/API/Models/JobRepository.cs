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
        private ApplicationDbContext context;

        public JobRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Job GetJobById(int job_id)
        {
            return context.Jobs.Find(job_id);
        }
        public IEnumerable<Job> GetJobsByEmployeeDayId(int employeeDay_id)
        {
            var jobs = from jb in context.Jobs
                               where jb.EmployeeDay_Id == employeeDay_id
                               select jb;
            return jobs;
        }

        public Job InsertJob(Job job)
        {
            context.Jobs.Add(job);
            return job;
        }

        public Job RemoveJob(int job_id)
        {
            Job job = context.Jobs.Find(job_id);
            context.Jobs.Remove(job);
            return job;
        }
        public void UpdateJob(Job job)
        {
            context.Entry(job).State = EntityState.Modified;
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
