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
            return jobsRepository.GetJobsByEmployeeDayId(Int32.Parse(id));
        }

        [HttpGet("{id}", Name = "GetJobById")]
        public IActionResult GetJobsById(string id)
        {
            var job = jobsRepository.GetJobById(Int32.Parse(id));
            if (job == null)
            {
                return NotFound();
            }
            return new ObjectResult(job);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Job job)
        {
            Debug.WriteLine("Getting Here");
            if (job == null)
            {
                return BadRequest();
            }
            Debug.WriteLine(job);
            jobsRepository.InsertJob(job);
            jobsRepository.Save();
            return Created("GetJobsById", job);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Job job)
        {
            if (job == null || job.Job_Id != Int32.Parse(id))
            {
                return BadRequest();
            }

            var jb = jobsRepository.GetJobById(Int32.Parse(id));
            if (jb == null)
            {
                return NotFound();
            }
            jobsRepository.UpdateJob(job);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var job = jobsRepository.GetJobById(Int32.Parse(id));
            if (job == null)
            {
                return NotFound();
            }

            jobsRepository.RemoveJob(Int32.Parse(id));
            return new NoContentResult();
        }
    }


}
