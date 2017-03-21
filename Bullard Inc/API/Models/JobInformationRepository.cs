using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class JobInformationRepository : IJobInformationRepository
    {
        public JobInformation GetJobInfoById(int job_id)
        {
            JobRepository jobRepo = new JobRepository();
            ActivityCodeRepository ACRepo = new ActivityCodeRepository();
            ProjectRepository ProjectRepo = new ProjectRepository();
            JobInformation jobInfo = new JobInformation();
            List<Job> tmp = new List<Job>();
            tmp.Add(jobRepo.GetJobById(job_id));
            jobInfo.Jobs = tmp;
            jobInfo.ActivityCodes = ACRepo.GetActivityCodes().ToList();
            jobInfo.Projects = ProjectRepo.GetProjects().ToList();
            return jobInfo;


        }

        public JobInformation GetJobsByEmployeeDayId(int employeeDay_id)
        {
            JobRepository jobRepo = new JobRepository();
            ActivityCodeRepository ACRepo = new ActivityCodeRepository();
            ProjectRepository ProjectRepo = new ProjectRepository();
            JobInformation jobInfo = new JobInformation();
            jobInfo.Jobs = jobRepo.GetJobsByEmployeeDayId(employeeDay_id).ToList(); ;
            jobInfo.ActivityCodes = ACRepo.GetActivityCodes().ToList();
            jobInfo.Projects = ProjectRepo.GetProjects().ToList();
            return jobInfo;
        }
    }
}
