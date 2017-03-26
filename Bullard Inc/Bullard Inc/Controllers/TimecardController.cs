using System;
using System.Linq;
using System.Threading.Tasks;
using Bullard_Inc.Models;
using System.Web.Mvc;

using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
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
        public ActionResult SignOut()
        {
            // TODO: get request to api/timesheets/employee/current/{id}
            return View();
        }

        // Submit 
        public async Task<ActionResult> Submit(int id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync("timesheets/submit/" + id.ToString());

            return RedirectToAction("Index", "Timecard");

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
            var response = responseMessage.Content.ReadAsStringAsync().Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                Job[] EmployeeDayJob = JsonConvert.DeserializeObject<Job[]>(response);
                return View(EmployeeDayJob);
            }
            // if api call fails, return error
            return RedirectToAction("Error " + response);
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
            var response = responseMessage.Content.ReadAsStringAsync().Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                activityCodes = JsonConvert.DeserializeObject<ActivityCode[]>(response);

                // and then get list of project numbers
                responseMessage = await client.GetAsync(projectsURL);
                if (responseMessage.IsSuccessStatusCode)
                {
                    response = responseMessage.Content.ReadAsStringAsync().Result;
                    projects = JsonConvert.DeserializeObject<Project[]>(response);

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
            return RedirectToAction("Error " + response);
        }

        // SUBMIT JOB ACTION
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
            var response = responseMessage.Content.ReadAsStringAsync().Result;
            if (responseMessage.IsSuccessStatusCode)
            {            
                activityCodes = JsonConvert.DeserializeObject<ActivityCode[]>(response);

                // get list of project numbers
                responseMessage = await client.GetAsync(projectsURL);
                if (responseMessage.IsSuccessStatusCode)
                {
                    response = responseMessage.Content.ReadAsStringAsync().Result;
                    projects = JsonConvert.DeserializeObject<Project[]>(response);

                    // get job
                    responseMessage = await client.GetAsync(empJobEditURL);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        response = responseMessage.Content.ReadAsStringAsync().Result;
                        job = JsonConvert.DeserializeObject<Job>(response);

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
            return RedirectToAction("Error " + response);
        }

        // UPDATE JOB ACTION
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


        public ActionResult Help()
        {
            //ViewBag.Message = "Your contact page.";

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
    }
}
