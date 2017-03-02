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
    public class EmployeeDaysController : Controller
    {
        private IEmployeeDayRepository employeeDaysRepository;

        public EmployeeDaysController(IEmployeeDayRepository employeeDaysRepository)
        {
            this.employeeDaysRepository = employeeDaysRepository;
        }

        [HttpGet("timesheet/{id}", Name = "GetEmployeeDaysByTimesheet")]
        public IEnumerable<EmployeeDay> GetEmployeeDaysByTimesheet(string id)
        {
            return employeeDaysRepository.GetEmployeeDaysByTimesheet(Int32.Parse(id));
        }

        [HttpGet("{id}", Name = "GetEmployeeDayById")]
        public IActionResult GetEmployeeDaysById(string id)
        {
            var emp = employeeDaysRepository.GetEmployeeDayById(Int32.Parse(id));
            if (emp == null)
            {
                return NotFound();
            }
            return new ObjectResult(emp);
        }

        [HttpPost]
        public IActionResult Create([FromBody] EmployeeDay employeeDay)
        {
            Debug.WriteLine("Getting Here");
            if (employeeDay == null)
            {
                return BadRequest();
            }
            Debug.WriteLine(employeeDay);
            var emp = employeeDaysRepository.InsertEmployeeDay(employeeDay);
            employeeDaysRepository.Save();
            return new ObjectResult(emp);
        }

        //This might be unneccasary now, since it just hold foreign keys and no data
        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] EmployeeDay employeeDay)
        {
            if (employeeDay == null || employeeDay.EmployeeDay_Id != Int32.Parse(id))
            {
                return BadRequest();
            }

            var day = employeeDaysRepository.GetEmployeeDayById(Int32.Parse(id));
            if (day == null)
            {
                return NotFound();
            }
            day.Timesheet_Id = employeeDay.Timesheet_Id;
            day.Day_Id = employeeDay.Day_Id;
            employeeDaysRepository.UpdateEmployeeDay(day);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var day = employeeDaysRepository.GetEmployeeDayById(Int32.Parse(id));
            if (day == null)
            {
                return NotFound();
            }

            employeeDaysRepository.RemoveEmployeeDay(Int32.Parse(id));
            return new NoContentResult();
        }
    }
}
