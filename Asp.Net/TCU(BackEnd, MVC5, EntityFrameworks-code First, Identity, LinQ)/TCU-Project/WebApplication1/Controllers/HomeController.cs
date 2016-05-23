using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Security.Claims;
using System.Threading;
using System.Data.Entity;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
   
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

    


    }
}