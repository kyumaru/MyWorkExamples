using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace navigationTechniques.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "press button to make a redirect into serverTransfer page";

            return View();
        }

        [HttpPost]
        public ActionResult About(string[] args)
        {
            return Redirect("~/Home/Contact");
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "click button to make a Server.Trasfer navigation into JS window page";

        
            return View();
        }

        [HttpPost]
        public void Contact(int? a)
        {
            //Server.Tranfer getting a child error in MVC, another stuff just for webForms
            //http://stackoverflow.com/questions/847232/how-do-i-use-server-transfer-method-in-asp-net-mvc
            //Server.TransferRequest uses the same type of POST/Get request passed in
            //So this will land on POST method, since the form in view is using post 
            Server.TransferRequest("~/Home/JSwindow",true);
          
            //return View();
        }


        public ActionResult JSwindow()
        {
            ViewBag.Message = "hello from GET method, click button to make a JS document.window(URI) navigation into Forms page";

            return View();
        }


        [HttpPost]
        public ActionResult JSwindow(int? a)
        {
            ViewBag.Message = "hello from POST method, click button to make a JS document.window(URI) navigation into Forms page";
            
            return View();
            //return new EmptyResult();
        }


 
        public ActionResult Forms()
        {
            ViewBag.Message = "This page is used to pass in parameters from another page while navigation ocurrs";

            Response.Write("the passed in value is: "+ Request.QueryString["name"]);

            return View();
            //return new EmptyResult();
        }

        [HttpPost]
        public ActionResult Forms(int? a)
        {
            ViewBag.Message = "This page is used to pass in parameters from another page while navigation ocurrs";

            return View();
            //return new EmptyResult();
        }


    }
}