using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public interface IJobRepository
    {
        Job InsertJob(Job job);
        //IEnumerable<EmployeeDay> GetEmployeeDays();
        Job GetJobById(int job_id);
        IEnumerable<Job> GetJobsByEmployeeDayId(int employeeDay_id);
        Job RemoveJob(int job_id);
        void UpdateJob(Job job);
        void Save();
    }
}
