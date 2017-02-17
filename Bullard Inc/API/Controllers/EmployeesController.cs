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
    public class EmployeesController : Controller
    {
        private IEmployeeRepository employeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpGet]
        public IEnumerable<Employee> GetEmployees()
        {
            return employeeRepository.GetEmployees();
        }

        [HttpGet("{id}", Name = "GetEmployeeById")]
        public IActionResult GetEmployeeById(string id)
        {
            var emp = employeeRepository.GetEmployeeById(Int32.Parse(id));
            if (emp == null)
            {
                return NotFound();
            }
            return new ObjectResult(emp);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Employee employee)
        {
            Debug.WriteLine("Getting Here");
            if (employee == null)
            {
                return BadRequest();
            }
            Debug.WriteLine(employee);
            employeeRepository.InsertEmployee(employee);
            employeeRepository.Save();
            return Created("GetEmployeeById",employee);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Employee employee)
        {
            if(employee==null || employee.Emp_Id != Int32.Parse(id))
            {
                return BadRequest();
            }

            var emp = employeeRepository.GetEmployeeById(Int32.Parse(id));
            if (emp == null)
            {
                return NotFound();
            }
            employeeRepository.UpdateEmployee(emp);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var emp = employeeRepository.GetEmployeeById(Int32.Parse(id));
            if (emp == null)
            {
                return NotFound();
            }

            employeeRepository.RemoveEmployee(Int32.Parse(id));
            return new NoContentResult();
        }
    }
}
