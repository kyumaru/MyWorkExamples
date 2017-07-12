/*
This is MVC, consider the following:
    
Data persistance: available ways to make data persist over requests: 
Application,Session, QueryString, Cookies stay, however, ViewState is gone,
it is for webforms only, since is the way asp.net controls data persits over 
requests, usually used along IsPostBack, gone as well. Remember http is stateless,
meaning upon httpResponse, all data on class instance to serve the request is lost,
instance gets deleted
<a href="http://stackoverflow.com/questions/12367571/how-to-get-session-id-in-c-sharp" </a>

Cookieless sessions: WebForms only and there is a security risk since sessionID i
s embbeded into the URL, not supported in MVC, WepAPI, 
<a href="http://stackoverflow.com/questions/21638446/asp-net-mvc-and-using-cookieless-sessions" </a>
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dataBetweenForms.Controllers
{
    public class HomeController : Controller
    {
        string x = System.Web.HttpContext.Current.Application["totalSessions"].ToString();
        string y = System.Web.HttpContext.Current.Session["mySessionId"].ToString();

        //by default all action methods are GET
        public ActionResult Index()
        {
            //adds to the html in the response
            Response.Write("<p>" + "total sessions: " + x + "</p>" + "<br/>");
            Response.Write( "sessionID: " + y);

            return View();
        }

        [HttpPost]
        public ActionResult Index(string a)
        {
            /*could not get queryString[] to work, for some reason it is null 
                var name = System.Web.HttpContext.Current.Request.QueryString["field1"];
                var email = System.Web.HttpContext.Current.Request.QueryString["field2"];         
            check this http://stackoverflow.com/questions/9358923/accessing-request-querystringfoo-is-null-but-the-url-shows-the-query-string-pa
            QueryStrings https://www.youtube.com/watch?v=1Es0kCfzZZs&index=59&list=PL6n9fhu94yhXQS_p1i-HLIftB9Y7Vnxlo
            
            the server checks what way the values are passed onto it, if its by
            POST method the Form object this Request will be used to contain the data
            as key value pairs, whereas if query string is used, QueryString object
            will hold the data, this happens regardles of the URL being generated 
            */

            var name =Request.Form["field1"];
            var email = Request.Form["field2"];

            //now lets create a queryString to send values into

            var myURI = "~/Home/About?name=" + name + "&email="+email;

            return Redirect(myURI);
        }

        public ActionResult About()
        {           
            var name = Request.QueryString["name"];
            var email = Request.QueryString["email"];

            ViewBag.Message = "name is : " + name + "  email is: " + email;

            Response.Write("total sessions: " + x + "<br/> ");
            Response.Write("sessionID: " + y);

            return View();
        }

        public ActionResult Contact()
        {
            var message = "";          

            if (Request.Cookies!=null)
            {
                if (Request.QueryString["checkCookie"]==null)
                {
                    //create, save cookie to client
                    HttpCookie cookie = new HttpCookie("TestCookie","1");
                    Response.Cookies.Add(cookie);
                    Response.Redirect("~/Home/Contact/?checkCookie=1" );
                }
                else
                {
                    HttpCookie cookie = Request.Cookies["TestCookie"];

                    if (cookie!=null)
                    {
                        message = "success reading TestCookie, cookies are enabled";
                    }
                    else
                    {
                        message = "cookies are not enabled, check browser settings";
                    }

                }

            }
            else
            {
                message = "this browser does not support cookies";
            }

            ViewBag.Message = message;

            Response.Write("total sessions: " + x + "<br/> ");
            Response.Write("sessionID: " + y);

            return View();
        }
    }
}