using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bullard_Inc.Models;
using System.Web.Mvc;

namespace Timecard.Controllers
{
    public class TimecardController : Controller
    {

        // GET: /<controller>/
        public ActionResult Index()
        {
            return View();
        }

        [Route("timecard/empjobview/{day}")]
        public ActionResult EmpJobView(string day)
        {
            var vr = new EmpJobEditModel()
            {
                day = day,
                jobNumber = "14 - 081",
                status = "OPEN",
                hours = 8.0,
                miles = 125,
                lunch = 0.5,
                workPreferred = "Dry Wall"
            };
            return View(vr);
        }

        [Route("timecard/empjobview/{day}/empjobadd")]
        public ActionResult EmpJobAdd(string day)
        {
            //  ViewData["day"] = day;
            var vr = new EmpJobEditModel()
            {
                day = day,
                jobNumber = "",
                status = "OPEN",
                hours = 0,
                miles = 0,
                lunch = 0,
                workPreferred = ""
            };
            return View(vr);
        }

        [Route("timecard/empjobview/{day}/empjobedit")]
        public ActionResult EmpJobEdit(string day)
        {
            //  ViewData["day"] = day;
            var vm = new EmpJobEditModel()
            {
                day = day,
                jobNumber = "14 - 081",
                status = "OPEN",
                hours = 8.0,
                miles = 125,
                lunch = 0.5,
                workPreferred = "Dry Wall"
            };
            return View(vm);
        }

        public ActionResult Calendar()
        {
            return View();
        }


    }
}
