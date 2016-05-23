//shit that always need to be running
using WebApplication1.App_Start; //needed for it to see my FilterConfig clas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Helpers;
using System.Security.Claims;


/*
for issues with antiForgeryToken check
stackoverflow.com/questions/30976980/mvc-5-owin-login-with-claims-and-antiforgerytoken-do-i-miss-a-claimsidentity-pr
AntiForgeryConfig
One way to solve it is to set AntiForgeryConfig to use other ClaimType, like email, since this app uses email instead
of username to login. What is being used to login is involved in this.
*/

namespace WebApplication1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.Email;//this solved the antiforgeryToken error
        }
    }
}
