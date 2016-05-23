using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class CreateProjectData
    {
        public Project project  { get; set; }//an empty project, buffer for a project
        public IEnumerable<User> users { get; set; }//buffer for all users
    }
    
}