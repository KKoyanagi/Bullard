using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bullard_Inc.Models;
using System.Web.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Bullard_Inc.Controllers
{
    public class SchedulerController : Controller
    {
        HttpClient client; 
        string url = "http://bullardapi.azurewebsites.net/api/"; // The URL of the WEB API Service

        public SchedulerController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<ActionResult> Index()
        {
            HttpResponseMessage responseMessage = await client.GetAsync("weeks/current");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                WorkWeek currentWeek = JsonConvert.DeserializeObject<WorkWeek>(responseData);
                responseMessage = await client.GetAsync("pendingview/1");
                if (responseMessage.IsSuccessStatusCode)
                {
                    responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    List<PendingView> views = JsonConvert.DeserializeObject<List<PendingView>>(responseData);
                    return View(views);
                }
            }
            return View("Error");
        }

        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SubmitUser(Employee user)
        {
            // custom url
            string addUserURL = url + "employees";

            // post employee information
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(addUserURL, user);
            System.Net.HttpStatusCode response = responseMessage.StatusCode;
            Debug.WriteLine(responseMessage.Content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return View("AddUser"); // will need to redirect to the employees page
            }

            // if api call fails, return error
            return RedirectToAction("Error" + response);
        }

        [HttpGet]
        [Route("Approve/{id}")]
        public async Task<ActionResult> Approve(int id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync("timesheets/approve/"+ id.ToString());
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult Help()
        {
            //ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}