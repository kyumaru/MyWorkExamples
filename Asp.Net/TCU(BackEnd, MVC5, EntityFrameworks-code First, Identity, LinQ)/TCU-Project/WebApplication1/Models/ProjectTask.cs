using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class ProjectTask : ProjectItem
    {
        [StringLength(30, ErrorMessage = "máximo 30 caracteres")]
        [Display(Name = "Tarea")]
        public override string Name { get; set; }

        //public override int ID { get; set; }// to have another name displayed on gui for this inheritance

        //a projectTask is linked to a single project
        [Display(Name = "Proyecto")]
        public int ProjectID { get; set; } //this is supposed to be a fk to Project

        //virtual to have it lazy loading this info(query this info on demand not when projectTasks load up)
        public virtual Project Project { get; set; } //this is the navigation(relationship) of the fk above


    }
}