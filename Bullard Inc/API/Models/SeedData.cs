using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using API.Models;
using System;
using System.Linq;
using System.Diagnostics;


namespace API.Models
{
    public static class SeedData
    {
       /* public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any movies.
                try
                {
                    if (!context.Employees.Any())
                    {

                        context.Employees.AddRange(
                            new Employee
                            {

                                Emp_Id = 1,
                                FirstName = "Donald",
                                LastName = "Murchison",
                                Email = "donaldmurchison@csus.edu",
                                Phone = "(916)847-5441"
                            },
                            new Employee
                            {
                                Emp_Id = 2,
                                FirstName = "Frank",
                                LastName = "Reynolds",
                                Email = "warthog@sunny.com",
                                Phone = "(123)123-1234"
                            }

                        );
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                if (!context.WorkDays.Any())
                {
                    context.WorkDays.AddRange(
                        new WorkDay
                        {
                            WorkDay_Id = 1,
                            Day_Name = "Sunday"
                        },
                        new WorkDay
                        {
                            WorkDay_Id = 2,
                            Day_Name = "Monday"
                        },
                        new WorkDay
                        {
                            WorkDay_Id = 3,
                            Day_Name = "Tuesday"
                        },
                        new WorkDay
                        {
                            WorkDay_Id = 4,
                            Day_Name = "Wednesday"
                        },
                        new WorkDay
                        {
                            WorkDay_Id = 5,
                            Day_Name = "Thursday"
                        },
                        new WorkDay
                        {
                            WorkDay_Id = 6,
                            Day_Name = "Friday"
                        },
                        new WorkDay
                        {
                            WorkDay_Id = 7,
                            Day_Name = "Saturday"
                        }
                    );
                }
                if (!context.Projects.Any())
                {
                    context.Projects.AddRange(
                        new Project
                        {
                            //Project_Id = 1,
                            Project_Num = 246,
                            Location = "Sacramento State"
                        }
                    );
                }
                if (!context.WorkWeeks.Any())
                {
                    /*context.WorkWeeks.Add(
                        new WorkWeek
                        {
                            StartDate = Convert.ToDateTime("02/05/2017"),
                            EndDate = Convert.ToDateTime("02/11/2017")
                            //Week_Id = 1,
                        }
                    );
                    context.WorkWeeks.Add(
                        new WorkWeek
                        {
                        //Week_Id = 2,
                        StartDate = Convert.ToDateTime("02/12/2017"),
                          EndDate = Convert.ToDateTime("02/18/2017")
                      }
                    );
                    /*  new WorkWeek
                      {
                          //Week_Id = 2,
                          StartDate = Convert.ToDateTime("02/05/2017"),
                          EndDate = Convert.ToDateTime("02/11/2017")
                      }*/

              /*  }
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                if (!context.Timesheets.Any())
                {
                    //context.ExecuteStoreCommand("DBCC CHECKIDENT('Timsesheets',RESEED,0);");
                    context.Timesheets.AddRange(
                        new Timesheet
                        {
                            //Timesheet_Id = 1,
                            Week_Id = 1,
                            Emp_Id = 1,
                            Approval = false
                        }
                        /*new Timesheet
                        {
                            //Timesheet_Id = 2,
                            Week_Id = 1,
                            Emp_Id = 2,
                            Approval = 0
                        },
                        new Timesheet
                        {
                            //Timesheet_Id = 3,
                            Week_Id = 2,
                            Emp_Id = 1,
                            Approval = 0
                        }*/
           /*         );
                }
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                if (!context.EmployeeDays.Any())
                {
                    context.EmployeeDays.AddRange(
                        new EmployeeDay
                        {
                            Timesheet_Id = 1,
                            Day_Id = 5,
                            ActivityCode = 3000,
                            Hours = 8,
                            Mileage = 0,
                            Job_Id = 1
                        }
                        /*new EmployeeDay
                        {
                            Timesheet_Id = 1,
                            Day_Id = 6,
                            ActivityCode = 3000,
                            Hours = 8,
                            Mileage = 0,
                            Job_Id = 1
                        },
                        new EmployeeDay
                        {
                            Timesheet_Id = 1,
                            Day_Id = 7,
                            ActivityCode = 3000,
                            Hours = 8,
                            Mileage = 0,
                            Job_Id = 1
                        },
                        new EmployeeDay
                        {
                            Timesheet_Id = 1,
                            Day_Id = 1,
                            ActivityCode = 3000,
                            Hours = 8,
                            Mileage = 0,
                            Job_Id = 1
                        },
                        new EmployeeDay
                        {
                            Timesheet_Id = 2,
                            Day_Id = 1,
                            ActivityCode = 3000,
                            Hours = 8,
                            Mileage = 0,
                            Job_Id = 1
                        },
                        new EmployeeDay
                        {
                            Timesheet_Id = 2,
                            Day_Id = 3,
                            ActivityCode = 3000,
                            Hours = 8,
                            Mileage = 0,
                            Job_Id = 1
                        }*/
              /*      );
                }
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                
            }
        }*/
    }
}
