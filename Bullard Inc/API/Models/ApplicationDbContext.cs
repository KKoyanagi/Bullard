using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace API.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base(new DbContextOptionsBuilder().UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=BullardDB;Trusted_Connection=True;MultipleActiveResultSets=true").Options)
        { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WorkWeek> WorkWeeks { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<WorkDay> WorkDays { get; set; }
        public DbSet<Timesheet> Timesheets { get; set; }
        public DbSet<EmployeeDay> EmployeeDays { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<ActivityCode> ActivityCodes { get; set; }

    }
}
