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
        //The URL of the WEB API Service
        //string url = "http://localhost:62367/api/";
        string url = "http://bullardapi.azurewebsites.net/api/";

        public SchedulerController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public ActionResult AddUser()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Help()
        {
            return View();
        }
        public async Task<ActionResult> GetPending()
        {
            HttpResponseMessage responseMessage = await client.GetAsync("weeks/current");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                WorkWeek currentWeek = JsonConvert.DeserializeObject<WorkWeek>(responseData);
                responseMessage = await client.GetAsync("view/pending/1");
                if (responseMessage.IsSuccessStatusCode)
                {
                    responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    List<PendingView> views = JsonConvert.DeserializeObject<List<PendingView>>(responseData);
                    return PartialView("_PendingPanel", views);
                }
            }
            return PartialView();
        }
        public async Task<ActionResult> GetPastDue()
        {
            HttpResponseMessage responseMessage = await client.GetAsync("weeks/current");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                WorkWeek currentWeek = JsonConvert.DeserializeObject<WorkWeek>(responseData);
                responseMessage = await client.GetAsync("view/pastdue/3");
                if (responseMessage.IsSuccessStatusCode)
                {
                    responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    List<PastDueView> views = JsonConvert.DeserializeObject<List<PastDueView>>(responseData);
                    return PartialView("_PastDuePanel", views);
                }
            }
            return PartialView();
        }
        public async Task<ActionResult> GetApproved()
        {
            HttpResponseMessage responseMessage = await client.GetAsync("weeks/current");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                WorkWeek currentWeek = JsonConvert.DeserializeObject<WorkWeek>(responseData);
                responseMessage = await client.GetAsync("view/approved/1");
                if (responseMessage.IsSuccessStatusCode)
                {
                    responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    List<ApprovedView> views = JsonConvert.DeserializeObject<List<ApprovedView>>(responseData);
                    return PartialView("_ApprovedPanel", views);
                }
            }
            return PartialView();
        }

        [HttpGet]
        [Route("Approve/{id}")]
        public async Task<ActionResult> Approve(int id)
        {

            HttpResponseMessage responseMessage = await client.GetAsync("timesheets/approve/"+ id.ToString());
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}