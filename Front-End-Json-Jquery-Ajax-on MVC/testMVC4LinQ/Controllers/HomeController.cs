using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace myWebApplication.Controllers
{

    public class HomeController : Controller
    {
        internal static List<myModel> myModels = new List<myModel>();//attemp to upload as model

        //GET
        public ActionResult Index()
        {
            myModels.Clear();//static variables persist in appDomain
            return View();
        }
        /*
        http post request to an mvc controller method, the binder maps the received
        http post request values to variables, in this case model, 
        so far in order to make this work when sending json, a model
        had to be used, when using a string, object etc, it was always 
        null, notice the code in the front end as well, if the input type
        for the submit button is submit, upon pressing, it will send 2 requests
        1 jquery-ajax 2 the standard httpPost request
            */
        [HttpPost]
        public ActionResult Index(myModel model)
        {
            if (Request.IsAjaxRequest())
            {

                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                //return RedirectToAction("Index");
                myModels.Add(new myModel {
                    name = model.name,
                    email = model.email,
                    password = model.password,
                    date = model.date
                });

                return Json(new { 
                                 //name = model.UserId.ToString() });
                                 name=model.name,
                                 email=model.email,
                                 password=model.password,
                                 date=model.date,
                                 modelList=myModels
                                
                });
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);

        }

    }

    public class myModel
    {
        //public int modelId { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string date { get; set; }
        //public int count { get; set; }
    }


}