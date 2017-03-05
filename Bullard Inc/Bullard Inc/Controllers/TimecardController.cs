using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bullard_Inc.Models;
using System.Web.Mvc;

using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
//using Microsoft.AspNet.WebApi.Client;
using System.Net.Http.Formatting;
using System.Diagnostics;

namespace Timecard.Controllers
{
    public class TimecardController : Controller
    {

        HttpClient client;
        //The URL of the WEB API Service
        string url = "http://BullardAPI.azurewebsites.net/api/jobs";

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
            // get request to api/timesheets/employee/current/{id}
            // return View(timesheet)
            return View();
        }

        // This action will display the number of Jobs the user has worked on a particular day. 
        // GET: EmployeeInfo
        [Route("timecard/empjobview/{day_id}")]
        public async Task<ActionResult> EmpJobView(int day_id)
        {
            // Make a Post request to api/employeedays
            // with the timesheet_id and day_id in the body
            // api will return an employeeday object
            // Deserialize and the use employeeDay_id to make a get request
            // Make get request to api/jobs/employeedays/{employeeDay_id}
            // this returns a list of jobs - List<job> jobs

            ViewData["day"] = dayToString(day_id); // pass day selected into ViewData

            HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + 1);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var Employees = JsonConvert.DeserializeObject<JobModel>(responseData);
                return View(Employees);
            }
            return View("Error");
        }

        [Route("timecard/empjobview/{day_id}/empjobadd")]
        public ActionResult empJobAdd(int day_id)
        {
            ViewData["day"] = day_id;
            return View(new JobModel());
        }

        [HttpPost]
        [Route("timecard/empjobview/{day_id}/empjobadd")]
        public async Task<ActionResult> empJobAdd(int day_id, JobModel job)
        {
            Job vr = new Job();
            vr.EmployeeDay_Id = 2;
            vr.Project_Id = 1;
            vr.ActivityCode = 3050;
            vr.Hours = 12;
            vr.Mileage = 8;
            vr.Lunch = 0;
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, vr);
            System.Net.HttpStatusCode response = responseMessage.StatusCode;
            Debug.WriteLine(responseMessage.Content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("/empjobview/" + day_id);
            }
            return RedirectToAction("Error" + response);
        }
        // This action will display a empty Timecard
        /*[HttpPost]
        [Route("timecard/empjobview/{day}/empjobadd")]
        public async Task<RedirectToRouteResult> EmpJobAdd()
        {
            //  if (ModelState.IsValid)
            // {
            JobModel vr = new JobModel();
            vr.employeeDay_Id = 3;
            vr.project_ID = 9;
            vr.job_ID = 7;
            vr.activityCode = 3050;
            vr.hours = 12;
            vr.mileage = 8;
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, vr);
            System.Net.HttpStatusCode response = responseMessage.StatusCode;
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("timecard/empjobview/{day}");
            }
            return RedirectToAction("Error " + response);
            // }
        }
        */
        // This action will display the user's selected Timecard
        [Route("timecard/empjobview/{day_id}/empjobedit")]
        public ActionResult EmpJobEdit(int day_id)
        {
            ViewData["day"] = dayToString(day_id); // pass day selected into ViewData

            var vr = new JobModel()
            {
                employeeDay_Id = day_id,
                activityCode = 14 - 081,
                //status = "OPEN",
                hours = 8.0,
                mileage = 125,
                lunch = 0.5,
                //workPerformed = "Dry Wall"
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
