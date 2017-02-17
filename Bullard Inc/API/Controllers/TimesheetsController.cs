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
           var emp = timesheetRepository.GetTimesheetById(Int32.Parse(id));
            if (emp == null)
            {
                return NotFound();
            }
            return new ObjectResult(emp);
        }

        [HttpGet("week/{id}", Name = "GetTimesheetByWeek")]
        public IEnumerable<Timesheet> GetTimesheetsByWeek(string id)
        {
            return timesheetRepository.GetTimesheetsByWeek(Int32.Parse(id));
        }

        [HttpGet("employee/{id}", Name = "GetTimesheetByEmp")]
        public IEnumerable<Timesheet> GetTimesheetsByEmp(string id)
        {
            return timesheetRepository.GetTimesheetsByWeek(Int32.Parse(id));
        }
        [HttpPost]
        public IActionResult Create([FromBody] Timesheet timesheet)
        {
            Debug.WriteLine("Getting Here");
            if (timesheet == null)
            {
                return BadRequest();
            }
            Debug.WriteLine(timesheet);
            timesheetRepository.InsertTimesheet(timesheet);
            timesheetRepository.Save();
            return Created("GettimesheetById", timesheet);
        }

        /*[HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Timesheet timesheet)
        {
            if (timesheet == null || timesheet.Emp_Id != Int32.Parse(id))
            {
                return BadRequest();
            }

            var emp = timesheetRepository.GetTimesheetById(Int32.Parse(id));
            if (emp == null)
            {
                return NotFound();
            }
            timesheetRepository.Updatetimesheet(emp);
            return new NoContentResult();
        }*/

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var emp = timesheetRepository.GetTimesheetById(Int32.Parse(id));
            if (emp == null)
            {
                return NotFound();
            }

            timesheetRepository.RemoveTimesheet(Int32.Parse(id));
            return new NoContentResult();
        }
    }
}
