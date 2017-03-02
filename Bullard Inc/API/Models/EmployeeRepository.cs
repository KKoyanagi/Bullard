using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private ApplicationDbContext context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return context.Employees.ToList();
        }
        public Employee GetEmployeeById(int emp_id)
        {
            return context.Employees.Find(emp_id);
        }
        public Employee InsertEmployee(Employee employee)
        {
            context.Employees.Add(employee);
            return employee;
        }
        public Employee RemoveEmployee(int emp_id)
        {
            Employee employee = context.Employees.Find(emp_id);
            context.Employees.Remove(employee);
            return employee;
        }
        public void UpdateEmployee(Employee employee)
        {

            context.Entry(employee).State = EntityState.Modified;
        }
        public void Save()
        {
            context.SaveChanges();
        }
        //Not sure why we need this yet or how to use
        //Believe it is for cleaning up context once no longer needed
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
