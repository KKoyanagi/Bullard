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
        string url = "http://bullardapi.azurewebsites.net/api/";  //The URL of the WEB API Service

        // Set the base address and the Header Formatter
        public TimecardController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // Homepage of TimeCard Controller
        public ActionResult Index()
        {
            // TODO: get request to api/timesheets/employee/current/{id}
            return View();
        }

        // This action will display the number of Jobs the user has worked on a particular day. 
        [Route("timecard/empjobview/{day_id}")]
        public async Task<ActionResult> EmpJobView(int day_id)
        {
            /* TODO: 
                - Make a POST request to api/employeedays
                    with the "timesheet_id" and "day_id" in the body
                - API will return an employeeday object.
                - Deserialize and the use employeeDay_id to make a get request
                - Make get request to api/jobs/employeedays/{employeeDay_id}
                    this returns a list of jobs - List<job> jobs
            */

            // pass in day information into the view
            ViewData["day_id"] = day_id;
            ViewData["dayString"] = dayToString(day_id);

            // custom url
            string empJobViewURL = url + "jobs/employeeday/" + day_id; 

            HttpResponseMessage responseMessage = await client.GetAsync(empJobViewURL);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                Job[] EmployeeDayJob = JsonConvert.DeserializeObject<Job[]>(responseData);
                return View(EmployeeDayJob);
            }
            // if api call fails, return error
            return View("Error");
        }

        // ADD JOB ACTION
        [Route("timecard/empjobview/{day_id}/empjobadd")]
        public async Task<ActionResult> empJobAdd(int day_id)
        {
            // pass in day information into the view
            ViewData["day_id"] = day_id;
            ViewData["dayString"] = dayToString(day_id);

            // values for view model: Timecard_EmpJobAddEdit
            ActivityCode[] activityCodes;
            Project[] projects;

            // custom urls
            string activityCodesURL = url + "activitycodes";
            string projectsURL = url + "projects";

            // API CALLS
            // get list of activity codes 
            HttpResponseMessage responseMessage = await client.GetAsync(activityCodesURL);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                activityCodes = JsonConvert.DeserializeObject<ActivityCode[]>(responseData);

                // and then get list of project numbers
                responseMessage = await client.GetAsync(projectsURL);
                if (responseMessage.IsSuccessStatusCode)
                {
                    responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    projects = JsonConvert.DeserializeObject<Project[]>(responseData);

                    // if successful on both API calls
                    // initialize view model: Timecard_EmpJobAddEdit
                    Timecard_EmpJobAddEdit employeeJob = new Timecard_EmpJobAddEdit
                    {
                        Job = new Job(),
                        ActivityCodes = activityCodes.ToList(),
                        Projects = projects.ToList()
                    };
                    return View(employeeJob);
                }
            }

            // if either api call fails, return error. 
            return View("Error");
        }

        // ADD JOB ACTION SUBMIT TIMECARD
        [HttpPost]
        [Route("timecard/empjobview/{day_id}/empjobsubmit")]
        public async Task<ActionResult> EmpJobSubmit(int day_id, Job job)  
        {
            // custom url
            string empJobAddURL = url + "jobs"; 

            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(empJobAddURL, job);
            System.Net.HttpStatusCode response = responseMessage.StatusCode;
            Debug.WriteLine(responseMessage.Content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("/empjobview/" + day_id);
            }
            return RedirectToAction("Error" + response);
        }
   

        // EDIT JOB ACTION
        [Route("timecard/empjobview/{day_id}/empjobedit/")]
        public async Task<ActionResult> EmpJobEdit(int day_id, int? id)
         {
            // pass in day information into the view
            ViewData["day_id"] = day_id;
            ViewData["dayString"] = dayToString(day_id);

            // values for view model: Timecard_EmpJobAddEdit
            ActivityCode[] activityCodes;
            Project[] projects;
            Job job; 

            // custom urls
            string activityCodesURL = url + "activitycodes";
            string projectsURL = url + "projects";
            string empJobEditURL = url + "jobs/" + id;

            // API CALLS
            // get list of activity codes 
            HttpResponseMessage responseMessage = await client.GetAsync(activityCodesURL);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                activityCodes = JsonConvert.DeserializeObject<ActivityCode[]>(responseData);

                // get list of project numbers
                responseMessage = await client.GetAsync(projectsURL);
                if (responseMessage.IsSuccessStatusCode)
                {
                    responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    projects = JsonConvert.DeserializeObject<Project[]>(responseData);

                    // get job
                    responseMessage = await client.GetAsync(empJobEditURL);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        job = JsonConvert.DeserializeObject<Job>(responseData);

                        // initialize view model: Timecard_EmpJobAddEdit
                        Timecard_EmpJobAddEdit employeeJob = new Timecard_EmpJobAddEdit
                        {
                            Job = job,
                            ActivityCodes = activityCodes.ToList(),
                            Projects = projects.ToList()
                        };

                        return View(employeeJob);
                    }
                }
            }

            // if  api call fail, return error. 
            return View("Error");
         }

        // EDIT JOB ACTION UPDATE
        [Route("timecard/empjobview/{day_id}/empjobupdate/")]
        public async Task<ActionResult> EmpJobUpdate(int day_id, [Bind(Include = "Job_Id,EmployeeDay_Id,Project_Id,ActivityCode,Hours,Mileage,Lunch,")] Job job)
        {
            // custom url
            string empJobUpdateURL = url + "jobs/"; 

            HttpResponseMessage responseMessage = await client.PutAsJsonAsync(empJobUpdateURL, job);
            System.Net.HttpStatusCode response = responseMessage.StatusCode;
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("/empjobview/" + day_id);
            }
            return RedirectToAction("Error " + response);
        }
        

        // This action will display the user's timecard history
        public ActionResult History()
        {
            return View();
        }

        // --------------- LOGIC MODULES ---------------

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
        public ActionResult Help()
        {
            //ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
