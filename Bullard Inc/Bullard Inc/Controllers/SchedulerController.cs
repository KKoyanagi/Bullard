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

        public ActionResult Help()
        {
            return View();
        }

        public ActionResult SignOut()
        {
            return View();
        }
        public async Task<ActionResult> Index(string weekid = "0")
        {
            SchedulerIndexView view = new SchedulerIndexView();
            
            if (weekid == "0")
            {
                HttpResponseMessage responseMessage = await client.GetAsync("weeks/current");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                    view.Current_Week = JsonConvert.DeserializeObject<WorkWeek>(responseData);
                    
                    
                }
            }
            else
            {
                HttpResponseMessage responseMessage = await client.GetAsync("weeks/" +weekid);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                    view.Current_Week= JsonConvert.DeserializeObject<WorkWeek>(responseData);

                }
            }
            HttpResponseMessage responseMessage1 = await client.GetAsync("weeks");
            if (responseMessage1.IsSuccessStatusCode)
            {
                var responseData = responseMessage1.Content.ReadAsStringAsync().Result;

                view.Weeks = JsonConvert.DeserializeObject<IEnumerable<WorkWeek>>(responseData);


            }
            return View(view);
        }

        public ActionResult ChangeWeek(int week)
        {
           
            return RedirectToAction("Index", "Scheduler", new { weekid = week.ToString() });
        }

        public async Task<ActionResult> GetPending(int week)
        {
            
            HttpResponseMessage responseMessage = await client.GetAsync("view/pending/"+week.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                List<PendingView> views = JsonConvert.DeserializeObject<List<PendingView>>(responseData);
                return PartialView("_PendingPanel", views);
            }
            return PartialView();
        }

        public async Task<ActionResult> GetPastDue(int week)
        {
           
                HttpResponseMessage responseMessage = await client.GetAsync("view/pastdue/"+week.ToString());
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    List<PastDueView> views = JsonConvert.DeserializeObject<List<PastDueView>>(responseData);
                    return PartialView("_PastDuePanel", views);
                }
            
            return PartialView();
        }
        public async Task<ActionResult> GetApproved(int week)
        {
            
                HttpResponseMessage responseMessage = await client.GetAsync("view/approved/" + week.ToString());
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    List<ApprovedView> views = JsonConvert.DeserializeObject<List<ApprovedView>>(responseData);
                    return PartialView("_ApprovedPanel", views);
                }
            
            return PartialView();
        }

        
        public async Task<ActionResult> Approve(int id, int week)
        {

            HttpResponseMessage responseMessage = await client.GetAsync("timesheets/approve/"+ id.ToString());
            return await GetPending(week);
        }

        
        public async Task<ActionResult> UnApprove(int id, int week)
        {

            HttpResponseMessage responseMessage = await client.GetAsync("timesheets/unapprove/" + id.ToString());
            return await GetApproved(week);
        }
    }
}