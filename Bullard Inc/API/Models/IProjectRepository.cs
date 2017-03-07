using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public interface IProjectRepository
    {
        Project InsertProject(Project project);
        IEnumerable<Project> GetProjects();
        Project GetProjectById(int project_Id);
        Project RemoveProject(int project_Id);
        Project UpdateProject(Project project);
    }
}
