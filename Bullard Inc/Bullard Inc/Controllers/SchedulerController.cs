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
        string url = "http://BullardAPI.azurewebsites.net/api/";
        //string url = "http://localhost:62367/api/jobs";

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
    }
}