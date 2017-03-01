using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public class EmployeeDayRepository : IEmployeeDayRepository
    {
        private ApplicationDbContext context;

        public EmployeeDayRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public EmployeeDay GetEmployeeDayById(int employeeDay_id)
        {
            return context.EmployeeDays.Find(employeeDay_id);
        }
        public IEnumerable<EmployeeDay> GetEmployeeDaysByTimesheet(int timesheet_id)
        {
            var employeeDays = from ed in context.EmployeeDays
                             where ed.Timesheet_Id == timesheet_id
                             select ed;
            return employeeDays;
        }
        
        //This now no longer just inserts, it will insert and return new object
        // or if it is already created return the old object
        public EmployeeDay InsertEmployeeDay(EmployeeDay employeeDay)
        {
            var emp = from ed in context.EmployeeDays
                      where ed.Timesheet_Id == employeeDay.Timesheet_Id && ed.Day_Id == employeeDay.Day_Id
                      select ed;
            if (emp.Any())
            {
                return emp.First();
            }
            else
            {
                context.EmployeeDays.Add(employeeDay);
                return employeeDay;
            }  
        }

        public EmployeeDay RemoveEmployeeDay(int employeeDay_id)
        {
            EmployeeDay employeeDay = context.EmployeeDays.Find(employeeDay_id);
            context.EmployeeDays.Remove(employeeDay);
            return employeeDay;
        }
        public void UpdateEmployeeDay(EmployeeDay employeeDay)
        {
            context.Entry(employeeDay).State = EntityState.Modified;
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
