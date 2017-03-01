using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bullard_Inc.Models;
using System.Web.Mvc;

using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Timecard.Controllers
{
    public class TimecardController : Controller
    {

        HttpClient client;
        //The URL of the WEB API Service
        string url = "http://BullardAPI.azurewebsites.net/api/jobs/1";

        //Set the base address and the Header Formatter
        public TimecardController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // Homepage of TimeCard Controller
        // GET: /<controller>/
        public ActionResult Index()
        {
            return View();
        }

        // This action will display the number of Jobs the user has worked on a particular day. 
        // GET: EmployeeInfo
        [Route("timecard/empjobview/{day_id}")]
        public async Task<ActionResult> EmpJobView(int day_id)
        {
            ViewData["day"] = dayToString(day_id); // pass day selected into ViewData

            HttpResponseMessage responseMessage = await client.GetAsync(url);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var Employees = JsonConvert.DeserializeObject<JobModel>(responseData);
                return View(Employees);
            }
            return View("Error");
        }

        public ActionResult Create()
        {
            return View(new JobModel());
        }

        // This action will display a empty Timecard
        [Route("timecard/empjobview/{day_id}/empjobadd")]
        public ActionResult EmpJobAdd(int day_id)
        {
            ViewData["day"] = dayToString(day_id); // pass day selected into ViewData

            var vr = new JobModel()
            {
                employeeDay_Id = day_id, 
                activityCode = "",
                status = "OPEN",
                hours = 0,
                mileage = 0,
                lunch = 0,
                workPerformed = "N/A"
            };

            return View("EmpJobEdit",vr);
        }

        // This action will display the user's selected Timecard
        [Route("timecard/empjobview/{day_id}/empjobedit")]
        public ActionResult EmpJobEdit(int day_id)
        {
            ViewData["day"] = dayToString(day_id); // pass day selected into ViewData

            var vr = new JobModel()
            {
                employeeDay_Id = day_id,
                activityCode = "14 - 081",
                status = "OPEN",
                hours = 8.0,
                mileage = 125,
                lunch = 0.5,
                workPerformed = "Dry Wall"
            };

            return View(vr);
        }

        // This action will display the user's timecard history
        public ActionResult History()
        {
            return View();
        }

        // converts the day_id into a string
        private string dayToString(int day_id)
        {
            switch (day_id)
            {
                case 1: return "MONDAY";
                case 2: return "TUESDAY"; 
                case 3: return "WEDNESDAY"; 
                case 4: return "THURSDAY"; 
                case 5: return "FRIDAY"; 
                case 6: return "SATURDAY"; 
                case 7: return "SUNDAY";
                default: return "N/A"; 
            }
        }
    }
}
