/*
-Scaffolding makes this wrong, had to cast projectItems to project manually
-Note that even if the db is a single table for projects and projectItems, linq queries are
over a db context dbset( entity set, tables), so it can refference projects or projectItems idividually
*/

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
    public class ProjectController : Controller
    {
        private MainDbContext db = new MainDbContext();

        // GET: Project
        public ActionResult Index(int? id)
        {
            var allProyects = db.Projects;

            foreach (var project in allProyects) {
                checkOrUpdateProjectHours(project);
            }

            db.SaveChanges();
            return View(allProyects);
        }
        //this is protection against setting hours below the sum of project tasks
        private void checkOrUpdateProjectHours(Project project)
        {

            int taskHours = calcAllTaskHours(project);

            if ((int)project.hours < taskHours) {
                project.hours = taskHours;
       
            }

            //throw new NotImplementedException();
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

        // GET: Project/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = (Project)db.ProjectItems.Find(id);//had to add this kind of a cast manually
            if (project == null)
            {
                return HttpNotFound();
            }
         
            return View(project);
        }

        // GET: Project/Create
        public ActionResult Create()
        {
            var viewModel = new CreateProjectData();// the view must receive a model correctly instatiated
            viewModel.users = db.Users;
            /* this is eager loading
            .Include(i => i.Name)
            .Include(i => i.Major)
            .Include(i => i.Ucard)
            .OrderBy(i => i.Name) ;
            */

            PopulateAssignedUserData(viewModel.users);// for the checkboxes, their model gets passed by the viewbag

            return View(viewModel);
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
                    Major= user.Major,
                    Ucard= user.Ucard,
                    Assigned = false
                });
            }
            ViewBag.Users = viewModel;
            //throw new NotImplementedException();
        }

        // POST: Project/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Description,hours,approved")] Project project, string[] selectedUsers)
        {
            if (ModelState.IsValid)
            {
                project.assignees = new List<User>();//must be instantiated manually 
                project.ProjectTasks= new List<ProjectTask>();//must be instantiated manually
                 
                if (selectedUsers!=null) {
                    var projectId = project.ID;

                    foreach (var userId in selectedUsers)
                    {
                        var x = int.Parse(userId); //Linq does not like to parse this in query
                        //AddOrUpdateProjectItemUser(db, projectId, Int32.Parse(userId));//happens in context, memory 
                        project.assignees.Add(db.Users.Single(u=>u.ID== x));
                    }
                    
                }

                db.ProjectItems.Add(project);// add it to the context table--this happens in memory
                db.SaveChanges();//save the context state to db

                return RedirectToAction("Index");
            }

            return View(project);
        }


        void AddOrUpdateProjectItemUser(MainDbContext context, int projectId, int userId)
        {
            var crs = context.ProjectItems.SingleOrDefault(c => c.ID == projectId);//returns the entity(row) of the entity set
            if (crs != null)
                crs.assignees.Add(context.Users.Single(i => i.ID == userId));
        }

        // GET: Project/Edit/5
        public ActionResult Edit(int? id, int? userId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = (Project)db.ProjectItems.Find((int)id);
           
            if (project == null)
            {
                return HttpNotFound();
            }        

            if (userId != null)//it is removing an user from this project
            {
                removeUserbyId( project, (int)userId);
                removeUserFromProjectTasks(project, (int)userId);
                db.SaveChanges();

                return RedirectToAction("Edit", "Project", new { id = id });

            }

            return View(project);
        }

        private void removeUserFromProjectTasks(Project project, int userId)
        {
            var user = db.Users.Single(u => u.ID == userId);

            foreach (var task in project.ProjectTasks ) {
                if (task.assignees.Contains(user)) {
                    task.assignees.Remove(user);
                }
            }

            //throw new NotImplementedException();
        }

        // POST: Project/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,hours,approved")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();               

                return RedirectToAction("Index");
            }
            return View(project);
        }

        
     

        public void removeUserbyId( Project project, int userId) {  

            foreach (var item in project.assignees) {
                if (item.ID == userId) {
                    project.assignees.Remove(item);
                    break;
                }
       
            }
        }

        // GET: Project/Delete/5
        public ActionResult Delete(int? id)/*? here means the id parameter received over string query may be null */
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = (Project)db.ProjectItems.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = (Project)db.ProjectItems.Find(id);
            //wipe all projectTasks before purging project, was getting an error due to same table fk column project_id
            wipeProjectTasks(project);
            db.ProjectItems.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //it was necesary to remove all projectTasks manually, EF was not cascading the deletion on project
        protected void wipeProjectTasks(Project project) {

            while (project.ProjectTasks.Count()>0) {

                var item = project.ProjectTasks.First();
                //ProjectTask projectTask = (ProjectTask)db.ProjectItems.Find(item.ID);
                db.ProjectItems.Remove(item);
            }

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