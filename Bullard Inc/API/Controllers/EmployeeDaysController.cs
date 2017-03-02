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
            try
            {
                return employeeDaysRepository.GetEmployeeDaysByTimesheet(Int32.Parse(id));
            }
            catch
            {
                return null;
            }
        }

        [HttpGet("{id}", Name = "GetEmployeeDayById")]
        public IActionResult GetEmployeeDaysById(string id)
        {
            try
            {
                var emp = employeeDaysRepository.GetEmployeeDayById(Int32.Parse(id));
                if (emp == null)
                {
                    return NotFound();
                }
                return new ObjectResult(emp);
            }
            catch
            {
                return BadRequest();
            }
        }

        //This no longer just inserts, it also checks if this is already created
        [HttpPost]
        public IActionResult Create([FromBody] EmployeeDay employeeDay)
        {
            try
            {
                Debug.WriteLine("Getting Here");
                if (employeeDay == null)
                {
                    return BadRequest();
                }
                Debug.WriteLine(employeeDay);
                //Thi
                var empDay = employeeDaysRepository.InsertEmployeeDay(employeeDay);
                employeeDaysRepository.Save();
                return new ObjectResult(empDay);
            }
            catch
            {
                return BadRequest();
            }
        }

        //This might be unneccasary now, since it just hold foreign keys and no data
        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] EmployeeDay employeeDay)
        {
            try
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
                var day = employeeDaysRepository.GetEmployeeDayById(Int32.Parse(id));
                if (day == null)
                {
                    return NotFound();
                }

                employeeDaysRepository.RemoveEmployeeDay(Int32.Parse(id));
                return new NoContentResult();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
