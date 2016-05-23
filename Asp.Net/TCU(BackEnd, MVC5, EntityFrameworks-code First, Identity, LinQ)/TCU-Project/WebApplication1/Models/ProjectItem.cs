/*
    There can be inheritance at model level and another at db level, the db level inheritance implements 
    model level inheritance into db tables, after some work this solution implements both,
    in this case merging project and projectTask into ProjectItem table, this is TPH implementation approach and
    reduces the amount of joins, improving query performance. 
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public abstract class ProjectItem //base, parent class
    {
        [Key]
        public int ID { get; set; } //by default set to generate automaticaly by db

        public virtual string Name { get; set; }

        /*
       [Key]
       [DatabaseGenerated(DatabaseGeneratedOption.None)]// avoid auto db creation
       [Required]
       [Display(Name = "Proyecto")]
       public virtual string ID { get; set; }
       */

        //[Required]
        [StringLength(255, ErrorMessage = "máximo 255 caracteres")]
        [Display(Name = "Descripción")]
        public string Description { get; set; }

        [Required]
        [Range(0,30000)]//min 0 hours max 30000
        [Display(Name = "Horas")]
        [DisplayFormat(NullDisplayText = "0")]
        public int? hours { get; set; }

        [Display(Name = "Aprobado")]
        public bool approved { get; set; }//if the hours were approved by an admin

        [Display(Name = "Responsables")]
        public virtual ICollection<User> assignees { get; set; } //holds the 1 to many relatioship between ProjectItems and Users 

    }
}