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
                DateTime dt = DateTime.Today;
                if(week.EndDate < dt)
                {
                    WorkWeek wk = new WorkWeek();
                    switch (dt.DayOfWeek)
                    {
                        case DayOfWeek.Monday:
                            wk.StartDate = dt.AddDays(-1);
                            wk.EndDate = dt.AddDays(5);
                            break;
                        case DayOfWeek.Tuesday:
                            wk.StartDate = dt.AddDays(-2);
                            wk.EndDate = dt.AddDays(4);
                            break;
                        case DayOfWeek.Wednesday:
                            wk.StartDate = dt.AddDays(-3);
                            wk.EndDate = dt.AddDays(3);
                            break;
                        case DayOfWeek.Thursday:
                            wk.StartDate = dt.AddDays(-4);
                            wk.EndDate = dt.AddDays(2);
                            break;
                        case DayOfWeek.Friday:
                            wk.StartDate = dt.AddDays(-5);
                            wk.EndDate = dt.AddDays(1);
                            break;
                        case DayOfWeek.Saturday:
                            wk.StartDate = dt.AddDays(-6);
                            wk.EndDate = dt;
                            break;
                        default:
                            wk.StartDate = dt;
                            wk.EndDate = dt.AddDays(6);
                            break;

                    }
                    week = weeksRepository.InsertWeek(wk);
                   
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
