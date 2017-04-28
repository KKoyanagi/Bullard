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
    public class ActivityCodesController : Controller
    {
          private IActivityCodeRepository activityCodeRepository;

          public ActivityCodesController(IActivityCodeRepository activityCodeRepository)
          {
             this.activityCodeRepository = activityCodeRepository;
          }

          [HttpGet]
          public IEnumerable<ActivityCode> GetActivityCodes()
          {
             return activityCodeRepository.GetActivityCodes();
          }

        [HttpPost]
        public IActionResult Create([FromBody] ActivityCode activityCode)
        {
            try
            {
                Debug.WriteLine("Getting Here");
                if (activityCode == null)
                {
                    return BadRequest();
                }
                //Debug.WriteLine(activityCode);
                var ac = activityCodeRepository.InsertActivityCode(activityCode);
                //activityCodeRepository.Save();
                return new ObjectResult(ac);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] ActivityCode activityCode)
        {
            try
            {
                if (activityCode == null)
                {
                    return BadRequest();
                }

                var ac = activityCodeRepository.UpdateActivityCode(activityCode);
                if (ac == null)
                {
                    return NotFound();
                }

                return new ObjectResult(ac);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}

