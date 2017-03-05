using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class JobsController : Controller
    {
        private IJobRepository jobsRepository;

        public JobsController(IJobRepository jobsRepository)
        {
            this.jobsRepository = jobsRepository;
        }

        [HttpGet("employeeday/{id}", Name = "GetJobsByEmployeeDayId")]
        public IEnumerable<Job> GetJobsByEmployeeDay(string id)
        {
            try
            {
                return jobsRepository.GetJobsByEmployeeDayId(Int32.Parse(id));
            }
            catch
            {
                return null;
            }

        }

        [HttpGet("{id}", Name = "GetJobById")]
        public IActionResult GetJobsById(string id)
        {
            try
            {
                var job = jobsRepository.GetJobById(Int32.Parse(id));
                if (job == null)
                {
                    return NotFound();
                }
                return new ObjectResult(job);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] Job job)
        {
            try
            {
                Debug.WriteLine("Getting Here");
                if (job == null)
                {
                    return BadRequest();
                }
                //Debug.WriteLine(job);
                var jb = jobsRepository.InsertJob(job);
                //jobsRepository.Save();
                return new ObjectResult(jb);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Job job)
        {
            try
            {
                if (job == null)
                {
                    return BadRequest();
                }

                /*var jb = jobsRepository.GetJobById(Int32.Parse(id));
                if (jb == null)
                {
                    return NotFound();
                }
                //Conflicting key error unless use specific jb element
                //so for now using this work around
                //jb
                jb.EmployeeDay_Id = job.EmployeeDay_Id;
                jb.Project_Id = job.Project_Id;
                jb.ActivityCode = job.ActivityCode;
                jb.Hours = job.Hours;
                jb.Mileage = job.Mileage;
                jb.Lunch = job.Lunch;
                //Debug.WriteLine(jb);*/
                var jb = jobsRepository.UpdateJob(job);
                if (jb == null)
                {
                    return NotFound();
                }
                //jobsRepository.Save();
                return new ObjectResult(jb);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                var job = jobsRepository.GetJobById(Int32.Parse(id));
                if (job == null)
                {
                    return NotFound();
                }

                jobsRepository.RemoveJob(Int32.Parse(id));
                //jobsRepository.Save();
                return new NoContentResult();
            }
            catch
            {
                return BadRequest();
            }
        }
    }


}
