using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1;
using WebApplication1.CustomLibraries;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        private MainDbContext db = new MainDbContext();


        // GET: User
        public ActionResult Index(int? projectId, int? projectTaskId)
        {

            if (projectTaskId != null)
            {//if projectTaskId got sent over stringquery

                int myProjectID = db.ProjectTasks.Single(u => u.ID == projectTaskId).ProjectID;

                var usersInMyProject = db.ProjectItems.Single(u => u.ID == myProjectID).assignees;
                var usersInProjectTask = db.ProjectItems.Single(u => u.ID == projectTaskId).assignees;
                var viewModel = new List<User>(); //all users not in projectItem.assignees

                foreach (var item in usersInMyProject)
                {
                    if (!usersInProjectTask.Contains(item))
                    {
                        viewModel.Add(item);

                    }
                }
                
                //ViewBag.projectItemId = projectTaskId;
                TempData["goBackTo"] = Request.UrlReferrer.ToString();//possible usage of tempData, here to pass an url to redirect to 
                TempData["projectItemId"] = projectTaskId;
                ViewBag.backToprojectItem = true;

                decryptAllPasswords( viewModel);
                return View(viewModel);
            }

            if (projectId != null) { //it should redirect to project-edit view
                                     //create a list with all the users not in project.assignees
                var usersInProjectItem = db.ProjectItems.Single(u=>u.ID==projectId).assignees;
                var allUsers = db.Users;
                var viewModel= new List<User>(); //all users not in projectItem.assignees

                foreach (var item in allUsers) {
                    if (!usersInProjectItem.Contains(item)) {
                        viewModel.Add(item);
                    }
                }

                TempData["goBackTo"] = Request.UrlReferrer.ToString();//possible usage of tempData, here to pass an url to redirect to 
                TempData["projectItemId"] = projectId;
                ViewBag.backToprojectItem = true;

                decryptAllPasswords(viewModel);
                return View(viewModel);
            }
          

            ViewBag.backToProjectItem = false;


            var sameViewModel = db.Users.ToList();
            decryptAllPasswords(sameViewModel);

            return View(sameViewModel);
        }

        private void decryptAllPasswords(List<User> viewModel)
        {
            foreach (var item in viewModel) {
                item.Password = CustomDecrypt.Decrypt(item.Password);
            }
            //throw new NotImplementedException();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string[] selectedUsers)
        {
            if (TempData["projectItemId"] != null)
            {
                var projectItemId = (int)TempData["projectItemId"];

                ProjectItem projectItem = db.ProjectItems.Single(u => u.ID == projectItemId);

                if (selectedUsers != null) {

                    foreach (var userId in selectedUsers)
                    {
                        var x = int.Parse(userId); //Linq does not like to parse this in query
                        projectItem.assignees.Add(db.Users.Single(u => u.ID == x));
                    }

                    db.SaveChanges();
                }

                return Redirect((string)TempData["goBackTo"]);
                //return RedirectToAction("Edit", "Project", new { id= projectItemId });

            }
            //return View();
            //db.SaveChanges();

            return RedirectToAction("Index", "Project");

        }


        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Email,Password,isAdmin,Name,Major,Ucard")] User user)
        {
            if (ModelState.IsValid)
            {
                user.Password= CustomEncrypt.Encrypt(user.Password);
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            user.Password = CustomDecrypt.Decrypt(user.Password);
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Email,Password,isAdmin,Name,Major,Ucard")] User user)
        {
            if (ModelState.IsValid)
            {
                user.Password = CustomEncrypt.Encrypt(user.Password);

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            user.Password = CustomDecrypt.Decrypt(user.Password);
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
