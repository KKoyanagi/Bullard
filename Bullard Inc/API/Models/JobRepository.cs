using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

        public void InsertJob(Job job)
        {
            context.Jobs.Add(job);
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
