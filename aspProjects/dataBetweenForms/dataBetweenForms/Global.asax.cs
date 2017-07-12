using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace dataBetweenForms
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Application.Add("totalSessions", 0);//initial instance of application["sessions"]                    
        }

        //another event at application level, triggers when a session starts
        protected void Session_Start()
        {
            //you will get new session ids for every request if cookies are disabled, check
            //http://stackoverflow.com/questions/2874078/asp-net-session-sessionid-changes-between-requests
            //Session.Add("init", 0);//does not work, perhaps cookies should be enabled
            Session.Add("mySessionId",HttpContext.Current.Session.SessionID);
            var x = (int)HttpContext.Current.Application["totalSessions"];
            HttpContext.Current.Application["totalSessions"] = ++x;
        }

        protected void Session_End()
        {           
            HttpContext.Current.Application["totalSessions"] = 0;
        }
    }
}
