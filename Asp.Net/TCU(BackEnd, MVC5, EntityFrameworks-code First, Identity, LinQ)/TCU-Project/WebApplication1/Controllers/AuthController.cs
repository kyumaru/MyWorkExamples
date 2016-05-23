using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Security.Claims;
using WebApplication1.CustomLibraries;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {

        // GET: Auth
        //Imagine the auth filter as a wrapper around all controller, actions and views in the application
        //when user is anonymous(not authenticated) the
        //auth filer was configured to redirect here,
        //without [AllowAnonymous] the auth filter will keep
        //redirecting to /auth/login until error arise 

        /* everything under your
            AuthController can be accessed by
            anyone. This also by-passes the
            AuthorizeAttribute filter telling
            it that it shouldn’t be a always a 
            page for authorized users.
         */
        
        //browser iniates http requests, 
        [HttpGet]//when a get request occurs, client ask for data and consumes it, server sends such data, i.e a form
        public ActionResult Login()
        {          
            return View();
        }
        
        [HttpPost]///when a post request occurs, client sends data, server consumes such data, i.e form data 
        public ActionResult Login(LoginData model)// server consumes an User Models objet
        {
            if (!ModelState.IsValid) //Checks if input fields have the correct format
            {
                return View(model); //Returns the view with the input values so that the user doesn't have to retype again
            }

            using (var db = new MainDbContext())
            {
                //this linq query

                /*
                    notice that db.Users is an entity set (a table in entity framework) so this section of the linq query
                    is like -from table Users- in sql, then the method -FirstOrDefault- takes lambda notation 
                    -u => u.Email == model.Email- to return null or the row in table Users that matches such condition,
                    this is like -select*- in sql
                */
                var emailCheck =db.Users.FirstOrDefault(u => u.Email == model.Email);// if emailCheck != null, then there is such entry in the db, default value is null

                if(emailCheck!=null){

                    var getPassword = db.Users.Where(u => u.Email == model.Email).Select(u => u.Password);// “SELECT Password from Users WHERE Email == model.Email“.
                    var materializePassword = getPassword.ToList();
                    var password = CustomDecrypt.Decrypt(materializePassword[0]);
                    //var decryptedPassword = CustomDecrypt.Decrypt(password);

                    if (model.Email != null && model.Password == password)
                    {
                        var getName = db.Users.Where(u => u.Email == model.Email).Select(u => u.Name);
                        var materializeName = getName.ToList();
                        var name = materializeName[0];


                        var getEmail = db.Users.Where(u => u.Email == model.Email).Select(u => u.Email);
                        var materializeEmail = getEmail.ToList();
                        var email = materializeEmail[0];

                        var getRole = db.Users.Where(u => u.Email == model.Email).Select(u => u.isAdmin);
                        var materializeRole = getRole.ToList();

                        var role="";

                        if (materializeRole[0])
                        {
                            role = "admin";
                        }
                        else {
                            role = "notAdmin";
                        }

                        //var role = materializeRole[0].ToString();//seems they are all strings


                        var identity = new ClaimsIdentity(new[] {
                                       new Claim(ClaimTypes.Name, name),
                                       new Claim(ClaimTypes.Email, email),
                                       new Claim(ClaimTypes.Role, role),
                                    }, "ApplicationCookie"); //the claims are stored in cookies, our chosen login method
                                             //there is a relationship between claims-cookies-session


                        var ctx = Request.GetOwinContext();//links the post request with middleware
                      
                        var authManager = ctx.Authentication;
                        authManager.SignIn(identity);//uses the info in var identity to sin in the request accordingly

                        return RedirectToAction("Index", "Project");//if signIn was done,  such redirect is possible
                    }


                }

               
            }

            ModelState.AddModelError("", "invalid email or password");
            return View(model); //Should always be declared on the end of an action method
    }

       

        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("Login", "Auth");
        }

/*
*
*Registration code is part of the sample app, never used in this implementation
*
*/

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
            
        }

        //registers a new use
        [HttpPost]
        public ActionResult Registration(User model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MainDbContext())
                {
                    var queryUser = db.Users.FirstOrDefault(u => u.Email == model.Email);//avoids user replication
                    if (queryUser == null)
                    {
                        var encryptedPassword = CustomEncrypt.Encrypt(model.Password);
                        var user = db.Users.Create();
                        user.Email = model.Email;
                        user.Password = encryptedPassword;
                        //user.Country = model.Country;
                        user.Name = model.Name;
                        db.Users.Add(user);
                        db.SaveChanges();

                    }
                    else
                    {

                        return RedirectToAction("Registration");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "One or more fields have been");
            }
            return View();
        }

        // GET: Auth
        [HttpGet]
        public ActionResult Index()
        {
            /*var db = new MainDbContext();
            return View(db.Lists.Where(x => x.Public == "YES").ToList());*/
            return View();
        }

    }
}