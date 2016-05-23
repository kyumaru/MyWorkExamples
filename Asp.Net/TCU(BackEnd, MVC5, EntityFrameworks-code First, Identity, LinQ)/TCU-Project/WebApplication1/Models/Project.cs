using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Project : ProjectItem
    {
        [StringLength(30, ErrorMessage = "máximo 30 caracteres")]
        [Display(Name = "Proyecto")]
        public override string Name { get; set; }

        //public virtual ICollection<ProjectTask> ProjectTasks { get; set; } //holds the 1 to n relatioship between this class and ProjectTasks
        [Display(Name = "Tareas asociadas")]
        public virtual ICollection<ProjectTask> ProjectTasks { get; set; } //holds the 1 to n relatioship between this class and ProjectTasks

    }
}