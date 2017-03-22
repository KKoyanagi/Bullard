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
    public class ViewController : Controller
    {
        private IViewRepository viewRepository;

        public ViewController(IViewRepository viewRepository)
        {
            this.viewRepository = viewRepository;
        }

        [HttpGet("pending/{week_id}")]
        public IEnumerable<PendingView> GetPendingViews(string week_id)
        {
            try
            {
                return viewRepository.GetPendingViews(Int32.Parse(week_id));
            }
            catch
            {
                return null;
            }
            
        }
        [HttpGet("approved/{week_id}")]
        public IEnumerable<ApprovedView> GetApprovedViews(string week_id)
        {
            try
            {
                return viewRepository.GetApprovedViews(Int32.Parse(week_id));
            }
            catch
            {
                return null;
            }

        }

        [HttpGet("pastdue/{week_id}")]
        public IEnumerable<PastDueView> GetPastDueViews(string week_id)
        {
            try
            {
                return viewRepository.GetPastDueViews(Int32.Parse(week_id));
            }
            catch
            {
                return null;
            }

        }
    }
}

