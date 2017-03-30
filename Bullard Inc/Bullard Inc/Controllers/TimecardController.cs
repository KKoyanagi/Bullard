﻿using System;
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
            static int EmpDayId;
            static int TimesheetId;
            static DateTime SundayDate;
            static DateTime SaturdayDate;

        // Set the base address and the Header Formatter
        public TimecardController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

        // Homepage of TimeCard Controller
        public async Task<ActionResult> Index()
        {
            string currentEmpURL = url + "timesheets/employee/current/" + 1;
            Timesheet currentTimesheet;
            ViewData["weekDate"] = currentWeekDate();

            // TODO: get request to api/timesheets/employee/current/{id}
            HttpResponseMessage responseMessage = await client.GetAsync(currentEmpURL);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                currentTimesheet = JsonConvert.DeserializeObject<Timesheet>(responseData);
                TimesheetId = currentTimesheet.Timesheet_Id;

            }
            else
            {
                return View("Error2");
            }
            return View(currentTimesheet);
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
            string empJobTimesheetURL = url + "employeedays/";
            string empJobURL = url + "jobs/employeeday/";
            EmployeeDay empDayTest = new EmployeeDay();
            EmployeeDay NewEmployeeDay;
            empDayTest.Day_Id = day_id;
            empDayTest.Timesheet_Id = TimesheetId;
            ViewData["weekDate"] = currentWeekDate();
            //empDayTest.EmployeeDay_Id = 1;

            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(empJobTimesheetURL, empDayTest);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                NewEmployeeDay = JsonConvert.DeserializeObject<EmployeeDay>(responseData);
                EmpDayId = NewEmployeeDay.EmployeeDay_Id;

            }
            else
            {
                return View("Error1");
            }
            HttpResponseMessage responseMessage2 = await client.GetAsync(empJobURL + "/" + NewEmployeeDay.EmployeeDay_Id);
            if (responseMessage2.IsSuccessStatusCode)
            {
                var responseData2 = responseMessage2.Content.ReadAsStringAsync().Result;

                Job[] EmployeeDayJob = JsonConvert.DeserializeObject<Job[]>(responseData2);
                EmpDayId = NewEmployeeDay.EmployeeDay_Id;
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
            ViewData["empDayId"] = EmpDayId;
            ViewData["weekDate"] = currentWeekDate();

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
            ViewData["weekDate"] = currentWeekDate();

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
        private string currentWeekDate()
        {
            string SundayDate = DateTime.Today.AddDays((int)(DateTime.Today.DayOfWeek) * -1).ToShortDateString();
            string SaturdayDate = DateTime.Today.AddDays(((int)(DateTime.Today.DayOfWeek) * -1) + 6).ToShortDateString();
            string weekDates = SundayDate + "-" + SaturdayDate;
            return weekDates;
        }
    }
}
