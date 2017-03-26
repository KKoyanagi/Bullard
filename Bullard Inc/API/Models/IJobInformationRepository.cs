using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public interface IJobInformationRepository
    {
        JobInformation GetJobInfoById(int job_id);
        JobInformation GetJobsByEmployeeDayId(int employeeDay_id);
    }
}
