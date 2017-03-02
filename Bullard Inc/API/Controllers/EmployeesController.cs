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
            try
            {
                var emp = employeeRepository.GetEmployeeById(Int32.Parse(id));
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

        [HttpPost]
        public IActionResult Create([FromBody] Employee employee)
        {
            try
            {
                Debug.WriteLine("Getting Here");
                if (employee == null)
                {
                    return BadRequest();
                }
                Debug.WriteLine(employee);
                var emp = employeeRepository.InsertEmployee(employee);
                employeeRepository.Save();
                return new ObjectResult(emp);
            }
            catch
            {
                return BadRequest();
            }
        }

        
        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Employee employee)
        {
            try
            {
                if (employee == null || employee.Emp_Id != Int32.Parse(id))
                {
                    return BadRequest();
                }

                var emp = employeeRepository.GetEmployeeById(Int32.Parse(id));
                if (emp == null)
                {
                    return NotFound();
                }
                emp.AccountName = employee.AccountName;
                emp.Email = employee.Email;
                emp.FirstName = employee.FirstName;
                emp.LastName = employee.LastName;
                emp.Phone = employee.Phone;
                employeeRepository.UpdateEmployee(emp);
                employeeRepository.Save();
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
                var emp = employeeRepository.GetEmployeeById(Int32.Parse(id));
                if (emp == null)
                {
                    return NotFound();
                }

                employeeRepository.RemoveEmployee(Int32.Parse(id));
                return new NoContentResult();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
