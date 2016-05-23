using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class ProjectTaskController : Controller
    {
        private MainDbContext db = new MainDbContext();

        // GET: ProjectTask
        public ActionResult Index()
        {
            var projectItems = db.ProjectTasks.Include(p => p.Project);
            return View(projectItems.ToList());
        }

        // GET: ProjectTask/Details/id
        public ActionResult Details(int? id, bool? backToProyectEdit)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectTask projectTask = (ProjectTask)db.ProjectItems.Find(id);
            if (projectTask == null)
            {
                return HttpNotFound();
            }

            if (backToProyectEdit != null) {
                int myProjectID = db.ProjectTasks.Single(u => u.ID == id).ProjectID;

                ViewBag.myProjectID = myProjectID;//so the view knows it should redirect there
            }

            return View(projectTask);
        }

        // GET: ProjectTask/Create
        public ActionResult Create(int? projectId) // Project/edit was not sending in the argument because it has to have the same name 
        {
            TempData["projectId"] = null;

            if (projectId == null)
            {
                ViewBag.ProjectID = new SelectList(db.Projects, "ID", "Name");
                ViewBag.toProjectEdit = false;
                ViewBag.myProject = -1;
            }
            else {

                //ViewBag.ProjectID = new SelectList(db.Projects.Where(u => u.ID == projectId), "ID", "Name");
                ViewBag.ProjectID = new SelectList(db.Projects.Where(u => u.ID == projectId), "ID", "Name");
                //TempData["backToProject-Edit"] = true;//possible usage of tempData
                ViewBag.myProject = (int)projectId; //passing the projectId over viewbag, this should be done by creating new view Models instead
                TempData["projectId"] = projectId;

                //TODO assing users from project users
                var availableUsers = db.Projects.SingleOrDefault(u => u.ID == projectId).assignees;
                PopulateAssignedUserData(availableUsers);

            }

            return View();

        }



        private void PopulateAssignedUserData(IEnumerable<User> users)//a custom model is added into viewbag to diplay user rows with checkboxes
        {
            var allUsers = new HashSet<User>(users);
            var viewModel = new List<AssignedUserData>();

            foreach (var user in allUsers)
            {
                viewModel.Add(new AssignedUserData
                {
                    UserID = user.ID,
                    Name = user.Name,
                    Major = user.Major,
                    Ucard = user.Ucard,
                    Assigned = false
                });
            }
            ViewBag.Users = viewModel;
            //throw new NotImplementedException();
        }

        // POST: ProjectTask/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*
         projectId is received by query string on form submission, look at URL in browser,
         if name of the parameter is there, it gets passed to the controller,
         i.e http://localhost:52678/ProjectTask/Create?projectId=1
        */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Description,hours,approved,ProjectID")] ProjectTask projectTask, int? projectId, string[] selectedUsers)
        {
            if (ModelState.IsValid)
            {
                
                Project project = (Project)db.ProjectItems.Find(projectId);
                var check=checkOrUpdateProjectHours(project, projectTask);

                if (check)
                {

                    if (selectedUsers != null)
                    {
                        projectTask.assignees = new List<User>();

                        foreach (var userId in selectedUsers)
                        {
                            var x = int.Parse(userId); //Linq does not like to parse this in query
                                                       //AddOrUpdateProjectItemUser(db, projectId, Int32.Parse(userId));//happens in context, memory 
                            projectTask.assignees.Add(db.Users.Single(u => u.ID == x));
                        }

                    }


                    project.ProjectTasks.Add(projectTask);
                    db.SaveChanges();
                }
                else {
                    return Redirect(Request.UrlReferrer.ToString());// upon this redirect an error message should show
                }
             
                //return RedirectToAction("Index");
            }

            if (TempData["projectId"] != null)
            {
                //return RedirectToAction( "Edit", "Project", new { id = 1 });
                return RedirectToAction("Edit", "Project", new { id = projectId });
            }
            else {
                 return RedirectToAction("Index");
            }
            
        }

       

        // GET: ProjectTask/Edit/5
        public ActionResult Edit(int? id, bool? backToProyectEdit)
        {
           
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ProjectTask projectTask = (ProjectTask)db.ProjectItems.Find(id);
                if (projectTask == null)
                {
                    return HttpNotFound();
                }

                if (backToProyectEdit == true)
                {
                    TempData["projectId"] = projectTask.ProjectID;// to tell post_edit to redirect there

                }

                ViewBag.ProjectID = new SelectList(db.Projects, "ID", "Name", projectTask.ProjectID);
                return View(projectTask);
           
        }

        // POST: ProjectTask/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Name,Description,hours,approved,ProjectID")] ProjectTask projectTask, int? id)
        {

            if (ModelState.IsValid)
            {
                projectTask.ID = (int)id;
                // AsNoTracking() http://stackoverflow.com/questions/23201907/asp-net-mvc-attaching-an-entity-of-type-modelname-failed-because-another-ent
                Project project = db.Projects.SingleOrDefault(u=>u.ID==projectTask.ProjectID);
                var check = checkEditProjectHours(project, projectTask);//*****

                if (check)
                {
                    //db.Entry(projectTask).State = EntityState.Modified; //had to it manually due to error
                    
                    ProjectTask efProjectTask= db.ProjectTasks.SingleOrDefault(u => u.ID == id);
                    efProjectTask.Name = projectTask.Name;
                    efProjectTask.Description = projectTask.Description;
                    efProjectTask.hours = projectTask.hours ;
                    efProjectTask.approved = projectTask.approved;
                    efProjectTask.ProjectID = projectTask.ProjectID;

                    db.SaveChanges();
                }
                else {
                   return Redirect(Request.UrlReferrer.ToString());// upon this redirect an error message should show
                }
             

                if (TempData["projectId"] != null)
                {

                    var x = (int)TempData["projectId"];
                    TempData["projectId"]=null;//it was not reseting back to null
                    return RedirectToAction("Edit", "Project", new { id = x });
                    
                }

                return RedirectToAction("Index");
            }
            ViewBag.ProjectID = new SelectList(db.ProjectItems, "ID", "Name", projectTask.ProjectID);
            return View(projectTask);
        }

        private bool checkEditProjectHours(Project project, ProjectTask projectTask)
        {
            var projectHours = (int)project.hours;
            //int allCurrentTaskHours = calcAllTaskHours(project);
            var incomingTaskHours = (int)projectTask.hours;
            var targetTaskHours = (int)db.ProjectTasks.SingleOrDefault(u => u.ID == projectTask.ID).hours;
            int difference = incomingTaskHours - targetTaskHours;

            int newTotalTaskHours = projectHours+difference;

            if ((newTotalTaskHours) <= 30000)
            {//the new task hours are equal less than project permissive hours
                    project.hours = newTotalTaskHours;
            }
            else {

                return false;
            }

            return true;
        }

        public ActionResult removeUser(int? taskId, int? userId)
        {
            if (taskId != null && userId != null) {
                ProjectTask task = (ProjectTask)db.ProjectItems.SingleOrDefault(u => u.ID == taskId);
                User user = db.Users.SingleOrDefault(u => u.ID == userId);

                if (task != null && user != null) {
                    task.assignees.Remove(user);
                    db.SaveChanges();
                }
            }

            //return RedirectToAction("Edit", "ProjectTask", new { id = taskId });

            return Redirect(Request.UrlReferrer.ToString());//go back to previous page
        }

        // GET: ProjectTask/Delete/5
        public ActionResult Delete(int? id, bool? backToProyectEdit)
        {
            TempData["projectId"] = null;//it was not reseting back to null

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectTask projectTask = (ProjectTask)db.ProjectItems.Find(id);
            if (projectTask == null)
            {
                return HttpNotFound();
            }

            if (backToProyectEdit != null)
            {
                int myProjectID = db.ProjectTasks.Single(u => u.ID == id).ProjectID;
                TempData["projectId"]= myProjectID;

            }


            return View(projectTask);
        }

        // POST: ProjectTask/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectTask projectTask = (ProjectTask)db.ProjectItems.Find(id);
            db.ProjectItems.Remove(projectTask);
            db.SaveChanges();

            if (TempData["projectId"] != null)
            {
                
                return RedirectToAction("Edit", "Project", new { id = (int)TempData["projectId"] });

            }

            return RedirectToAction("Index");
        }

        private bool checkOrUpdateProjectHours(Project project, ProjectTask projectTask)
        {
             var projectHours = (int)project.hours;
                int allCurrentTaskHours = calcAllTaskHours(project);
                var newTaskHours = (int)projectTask.hours;

                int newTotalTaskHours = allCurrentTaskHours + newTaskHours;

                if ((newTotalTaskHours) <= 30000)
                {//the new task hours are equal less than project permissive hours

                    if (projectHours < newTotalTaskHours)
                        project.hours = newTotalTaskHours;
                }
                else {

                    return false;
                }

                return true;
            
        }

        private int calcAllTaskHours(Project project)
        {
            var sum = 0;
            foreach (var item in project.ProjectTasks)
            {
                sum += (int)item.hours;
            }

            return sum;
            //throw new NotImplementedException();
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
