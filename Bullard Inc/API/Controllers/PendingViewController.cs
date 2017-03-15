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
    public class PendingViewController : Controller
    {
        private IPendingViewRepository viewRepository;

        public PendingViewController(IPendingViewRepository viewRepository)
        {
            this.viewRepository = viewRepository;
        }

        [HttpGet("{week_id}")]
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
    }
}

