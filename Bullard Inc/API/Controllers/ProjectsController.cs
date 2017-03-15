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
    public class ProjectsController : Controller
    {
        private IProjectRepository projectsRepository;

        public ProjectsController(IProjectRepository projectsRepository)
        {
            this.projectsRepository = projectsRepository;
        }
        [HttpGet]
        public IEnumerable<Project> GetWeeks()
        {
            return projectsRepository.GetProjects();
        }

        [HttpGet("{id}", Name = "GetProjectById")]
        public IActionResult GetProjectsById(string id)
        {
            try
            {
                var prj = projectsRepository.GetProjectById(Int32.Parse(id));
                if (prj == null)
                {
                    return NotFound();
                }
                return new ObjectResult(prj);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
