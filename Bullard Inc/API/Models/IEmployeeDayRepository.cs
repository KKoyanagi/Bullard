﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public interface IEmployeeDayRepository
    {
        EmployeeDay InsertEmployeeDay(EmployeeDay employeeDay);
        //IEnumerable<EmployeeDay> GetEmployeeDays();
        EmployeeDay GetEmployeeDayById(int employeeDay_id);
        IEnumerable<EmployeeDay> GetEmployeeDaysByTimesheet(int timesheet_id);
        EmployeeDay RemoveEmployeeDay(int employeeDay_id);
        //EmployeeDay UpdateEmployeeDay(EmployeeDay employeeDay);
        //void Save();
    }
}
