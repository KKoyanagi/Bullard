using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class WeeksController : Controller
    {
        private IWorkWeekRepository weeksRepository;

        public WeeksController(IWorkWeekRepository weeksRepository)
        {
            this.weeksRepository = weeksRepository;
        }
        [HttpGet]
        public IEnumerable<WorkWeek> GetWeeks()
        {
            return weeksRepository.GetWorkWeeks();
        }

        [HttpGet("{id}", Name = "GetWeekById")]
        public IActionResult GetWeeksById(string id)
        {
            try
            {
                var week = weeksRepository.GetWeekById(Int32.Parse(id));
                if (week == null)
                {
                    return NotFound();
                }
                return new ObjectResult(week);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("current", Name = "GetCurrentWeek")]
        public IActionResult GetCurrentWeek()
        {
            try
            {
                var week = weeksRepository.GetWorkWeeks().Last();
                if (week == null)
                {
                    return NotFound();
                }
                return new ObjectResult(week);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
