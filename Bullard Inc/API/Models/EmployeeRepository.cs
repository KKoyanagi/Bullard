using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        //private ApplicationDbContext context;
        private ApplicationDbContext getContext;

        //public EmployeeRepository(ApplicationDbContext context)
        //{
        //    this.context = context;
        //}
        public EmployeeRepository()
        {
            this.getContext = new ApplicationDbContext();
        }
        public IEnumerable<Employee> GetEmployees()
        {
            using(ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Employees.ToList();
            }
            
        }
        public Employee GetEmployeeById(int emp_id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Employees.Find(emp_id);
            }
        }
        public Employee GetEmployeeByName(string name)
        {
            if (getContext != null)
            {
                getContext.Dispose();
                getContext = new ApplicationDbContext();
            }
            //IQueryable<Timesheet> timesheets;
            var emp = from t in getContext.Employees
                             where t.AccountName == name
                             select t;
            if (emp.Any())
            {
                return emp.First<Employee>();
            }
            else
            {
                return null;
            }

        }
        public Employee InsertEmployee(Employee employee)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Employees.Add(employee);
                context.SaveChanges();
                return employee;
            }
        }
        public Employee RemoveEmployee(int emp_id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Employee employee = context.Employees.Find(emp_id);
                context.Employees.Remove(employee);
                context.SaveChanges();
                return employee;
            }
        }
        public Employee UpdateEmployee(Employee employee)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var ts = context.Employees.Find(employee.Emp_Id);
                
                if (ts == null)
                {
                    return null;
                }
                context.Entry(ts).Property(u => u.FirstName).CurrentValue = employee.FirstName;
                context.Entry(ts).Property(u => u.LastName).CurrentValue = employee.LastName;
                context.Entry(ts).Property(u => u.AccountName).CurrentValue = employee.AccountName;
                context.Entry(ts).Property(u => u.Email).CurrentValue = employee.Email;
                context.Entry(ts).Property(u => u.Phone).CurrentValue = employee.Phone;
                
                
                try
                {
                    context.SaveChanges();
                }
                catch 
                {
                    context.Database.ExecuteSqlCommand("UPDATE dbo.Employees SET FirstName = {0} WHERE Emp_Id = {1}", employee.FirstName, employee.Emp_Id);
                    context.Database.ExecuteSqlCommand("UPDATE dbo.Employees SET LastName = {0} WHERE Emp_Id = {1}", employee.LastName, employee.Emp_Id);
                    context.Database.ExecuteSqlCommand("UPDATE dbo.Employees SET AccountName = {0} WHERE Emp_Id = {1}", employee.AccountName, employee.Emp_Id);
                    context.Database.ExecuteSqlCommand("UPDATE dbo.Employees SET Email = {0} WHERE Emp_Id = {1}", employee.Email, employee.Emp_Id);
                    context.Database.ExecuteSqlCommand("UPDATE dbo.Employees SET Phone = {0} WHERE Emp_Id = {1}", employee.Phone, employee.Emp_Id);

                }
                return employee;
            }
        }
        /*public void Save()
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
        */
    }
}
