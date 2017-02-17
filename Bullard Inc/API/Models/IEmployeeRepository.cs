using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public interface IEmployeeRepository
    {
        void InsertEmployee(Employee employee);
        IEnumerable<Employee> GetEmployees();
        Employee GetEmployeeById(int emp_id);
        Employee RemoveEmployee(int emp_id);
        void UpdateEmployee(Employee employee);
        void Save();
    }
}
