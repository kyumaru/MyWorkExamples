using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.ViewModels;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ReportController : Controller
    {
        private MainDbContext db = new MainDbContext();

        // GET: Report
        public ActionResult Index()
        {           
            var allUsers = db.Users.ToList() ;
            var viewModel = new List<ReportData>();

            foreach (var item in allUsers) {

                viewModel.Add(new ReportData() {user=item, totalHours=getTotalHoursUser(item)});
            }

            return View(viewModel);
        }


        public ActionResult Details(int? userId)
        {
            if (userId != null)
            {
                var user = db.Users.SingleOrDefault(u => u.ID == userId);

                if (user==null) {
                    return RedirectToAction("Index");
                }

                var allTasks = db.ProjectTasks;
                var viewModel = new List<ProjectTask>();

                foreach (var task in allTasks) {
                    if (task.assignees.Contains(user)) {
                        viewModel.Add(task);
                    }
                }

                return View(viewModel);

            }
            return RedirectToAction("Index");
            //throw new NotImplementedException();
        }


        private int getTotalHoursUser(User user)
        {
            int hours = 0;

            var allProjectTask = db.ProjectTasks.ToList();

            foreach (var task in allProjectTask) {
                if (task.assignees.Contains(user)&&task.approved) {
                    hours += (int)task.hours;
                }       
            }

            return hours;
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