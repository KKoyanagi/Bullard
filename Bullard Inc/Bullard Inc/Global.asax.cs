using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Bullard_Inc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        void Application_EndRequest(object sender, System.EventArgs e)
        {
            if (((Response.StatusCode == 401)
            && (Request.IsAuthenticated == true)))
            {
                Response.ClearContent();
                Response.Redirect("~/Responses/Unauthorized.aspx");
            }

            if (((Response.StatusCode == 404)
            && (Request.IsAuthenticated == true)))
            {
                Response.ClearContent();
                Response.Redirect("~/Responses/Unauthorized.aspx");
            }

            if (Response.StatusCode == 401)
            {
                Response.ClearContent();
                Response.Redirect("~/Responses/Unauthorized.aspx");
            }
        }
    }
}
