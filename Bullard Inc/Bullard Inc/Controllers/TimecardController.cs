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
        string url = "http://api20170215085524.azurewebsites.net/api/jobs/1";

        //Set the base address and the Header Formatter
        public TimecardController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        // GET: /<controller>/
        public ActionResult Index()
        {
            return View();
        }

        // GET: EmployeeInfo
        [Route("timecard/empjobview/{day}")]
        public async Task<ActionResult> EmpJobView(string day)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var Employees = JsonConvert.DeserializeObject<EmpJobEditModel>(responseData);
                Employees.day = day;
                return View(Employees);
            }
            return View("Error");
        }

        public ActionResult Create()
        {
            return View(new EmpJobEditModel());
        }

        [Route("timecard/empjobview/{day}/empjobadd")]
        public ActionResult EmpJobAdd(string day)
        {
            //  ViewData["day"] = day;
            var vr = new EmpJobEditModel()
            {
                day = day,
                activityCode = "",
                status = "OPEN",
                hours = 0,
                mileage = 0,
                lunch = 0,
                workPreferred = "N/A"
            };
            return View("EmpJobEdit",vr);
        }

        [Route("timecard/empjobview/{day}/empjobedit")]
        public ActionResult EmpJobEdit(string day)
        {
            //  ViewData["day"] = day;
            var vr = new EmpJobEditModel()
            {
                day = day,
                activityCode = "14 - 081",
                status = "OPEN",
                hours = 8.0,
                mileage = 125,
                lunch = 0.5,
                workPreferred = "Dry Wall"
            };
            return View(vr);
        }

        public ActionResult Calendar()
        {
            return View();
        }


    }
}
