using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace API.Models
{
    public class ProjectRepository : IProjectRepository
    {
       

        public IEnumerable<Project> GetProjects()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Projects.ToList();
            }
        }
        public Project InsertProject(Project project)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Projects.Add(project);
                context.SaveChanges();
                return project;
            }
        }
        public Project GetProjectById(int project_id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Projects.Find(project_id);
            }
        }
        public Project RemoveProject(int project_id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Project project = context.Projects.Find(project_id);
                context.Projects.Remove(project);
                context.SaveChanges();
                return project;
            }
        }
        public Project UpdateProject(Project project)
        {
            
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var p = context.Projects.Find(project.Project_Id);
                if (p == null)
                {
                    return null;
                }
                context.Entry(p).Property(u => u.Project_Num).CurrentValue = project.Project_Num;
                context.Entry(p).Property(u => u.Location).CurrentValue = project.Location;

                try
                {
                    context.SaveChanges();
                }
                catch
                {
                    context.Database.ExecuteSqlCommand("UPDATE dbo.Projects SET Project_Num = {0} WHERE Project_Id = {1}", project.Project_Num, project.Project_Id);
                    context.Database.ExecuteSqlCommand("UPDATE dbo.Projects SET Location = {0} WHERE Project_Id = {1}", project.Location, project.Project_Id);
                }
                return p;
            }
        }
    }
}
