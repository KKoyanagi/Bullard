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
     }
}

