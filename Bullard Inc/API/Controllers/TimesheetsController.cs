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
    public class TimesheetsController : Controller
    {
        private ITimesheetRepository timesheetRepository;

        public TimesheetsController(ITimesheetRepository timesheetRepository)
        {
            this.timesheetRepository = timesheetRepository;
        }

        [HttpGet]
        public IEnumerable<Timesheet> GetTimesheets()
        {
            return timesheetRepository.GetTimesheets();
        }

        [HttpGet("{id}", Name = "GettimesheetById")]
        public IActionResult GetTimesheetById(string id)
        {
            try
            {
                var ts = timesheetRepository.GetTimesheetById(Int32.Parse(id));
                if (ts == null)
                {
                    return NotFound();
                }
                return new ObjectResult(ts);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("week/{id}", Name = "GetTimesheetByWeek")]
        public IEnumerable<Timesheet> GetTimesheetsByWeek(string id)
        {
            try
            {
                return timesheetRepository.GetTimesheetsByWeek(Int32.Parse(id));
            }
            catch
            {
                return null;
            }
        }
        [HttpGet("approved/week/{id}", Name = "GetApprovedTimesheetByWeek")]
        public IEnumerable<Timesheet> GetApprovedTimesheetsByWeek(string id)
        {
            try
            {
                return timesheetRepository.GetApprovedTimesheetsByWeek(Int32.Parse(id));
            }
            catch
            {
                return null;
            }
        }
        [HttpGet("submitted/week/{id}", Name = "GetSubmittedTimesheetByWeek")]
        public IEnumerable<Timesheet> GetSubmittedTimesheetsByWeek(string id)
        {
            try
            {
                return timesheetRepository.GetSubmittedTimesheetsByWeek(Int32.Parse(id));
            }
            catch
            {
                return null;
            }
        }
        [HttpGet("unapproved/week/{id}", Name = "GetUnapprovedTimesheetByWeek")]
        public IEnumerable<Timesheet> GetUnapprovedTimesheetsByWeek(string id)
        {
            try
            {
                return timesheetRepository.GetUnapprovedTimesheetsByWeek(Int32.Parse(id));
            }
            catch
            {
                return null;
            }
        }
        [HttpGet("notsubmitted/week/{id}", Name = "GetNotsubmittedTimesheetByWeek")]
        public IEnumerable<Timesheet> GetNotsubmittedTimesheetsByWeek(string id)
        {
            try
            {
                return timesheetRepository.GetNotsubmittedTimesheetsByWeek(Int32.Parse(id));
            }
            catch
            {
                return null;
            }
        }
        [HttpGet("employee/{id}", Name = "GetTimesheetByEmp")]
        public IEnumerable<Timesheet> GetTimesheetsByEmp(string id)
        {
            try
            {
                return timesheetRepository.GetTimesheetsByWeek(Int32.Parse(id));
            }
            catch
            {
                return null;
            }
        }
        [HttpGet("submit/{id}", Name = "SubmitTimesheet")]
        public Timesheet SubmitTimesheet(string id)
        {
            try
            {
                return timesheetRepository.SubmitTimesheet(Int32.Parse(id));
            }
            catch
            {
                return null;
            }
        }
        [HttpGet("approve/{id}", Name = "ApproveTimesheet")]
        public Timesheet ApproveTimesheet(string id)
        {
            try
            {
                return timesheetRepository.ApproveTimesheet(Int32.Parse(id));
            }
            catch
            {
                return null;
            }
        }
        [HttpPost]
        public IActionResult Create([FromBody] Timesheet timesheet)
        {
            Debug.WriteLine("Getting Here");
            if (timesheet == null)
            {
                return BadRequest();
            }
            // Debug.WriteLine(timesheet);
            var ts = timesheetRepository.InsertTimesheet(timesheet);
            //timesheetRepository.Save();
            return new ObjectResult(ts);
        }
        [HttpGet("employee/current/{id}")]
        public IActionResult GetCurrentForEmp(string id)
        {
            try
            {
                var ts = timesheetRepository.GetTimesheetCurrent(Int32.Parse(id));
                return new ObjectResult(ts);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Timesheet timesheet)
        {
            try
            {
                if (timesheet == null)
                {
                    return BadRequest();
                }

                //var ts = timesheetRepository.GetTimesheetById(Int32.Parse(id));
                //if (ts == null)
                //{
                 //  return NotFound();
                //}
                //ts.Approved = timesheet.Approved;
                //ts.Submitted = timesheet.Submitted;
                var ts2 = timesheetRepository.UpdateTimesheet(timesheet);
                if (ts2 == null)
                {
                    return NotFound();
                }
                return new ObjectResult(ts2);
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
                var emp = timesheetRepository.GetTimesheetById(Int32.Parse(id));
                if (emp == null)
                {
                    return NotFound();
                }

                timesheetRepository.RemoveTimesheet(Int32.Parse(id));
                return new NoContentResult();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
