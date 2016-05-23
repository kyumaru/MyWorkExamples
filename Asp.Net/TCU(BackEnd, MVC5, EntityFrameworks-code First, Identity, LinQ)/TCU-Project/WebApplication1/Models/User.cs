using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.CustomLibraries;

// there is a db, then you crate a model to represent it through a class, then a dbcontext to glue both together
namespace WebApplication1.Models
{
    public class User
    {

        //[Key] // in the database
        public int ID { get; set; }

        [Required] //the form will not submit, such annotations are constrains over the linked view
        [StringLength(30, ErrorMessage = "máximo 30 caracteres")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email format.")]
        public string Email { get; set; }
        /*
        [Required]
        [StringLength(20, ErrorMessage = "password cannot be longer than 20 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        */

       
        //http://stackoverflow.com/questions/22936021/ef-code-first-encrypt-decrypt-password-property
        [Required]
        [Display(Name = "Contraseña")]
        //[StringLength(20, ErrorMessage = "password cannot be longer than 20 characters.")]
        //[DataType(DataType.Password)]
        public virtual string Password
        { 
            get ;  
            set; 
        }
        /*This should be implemented some way, otherwise(like it is now above) the password has no data limit, 
            the encryption string grows with raw password input data*/
        /*
        [NotMapped]//not saved to database
        [Required]
        [StringLength(20, ErrorMessage = "password cannot be longer than 20 characters.")]
        public string Password
        {
            get { return CustomDecrypt.Decrypt(PasswordRestricted); }
            set { PasswordRestricted= CustomEncrypt.Encrypt( Password); }
        }*/


        [Display(Name = "EsAdmin")]
        public bool isAdmin { get; set; }
        /*
                [HiddenInput(DisplayValue = false)]//prevents such value from showing at associated view
                public string ReturnUrl { get; set; }
        */
        [StringLength(30, ErrorMessage = "máximo 30 caracteres")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [StringLength(20, ErrorMessage = "máximo 20 caracteres")]
        [Display(Name = "Carrera")]
        public string Major { get; set; }

        [StringLength(10, ErrorMessage = "máximo 10 caracteres")]
        [Display(Name = "Carné")]
        public string Ucard { get; set; }//carnet

        public virtual ICollection<ProjectItem> projectItems { get; set; } //holds the m to n relatioship between Users and Projects

        //public virtual ICollection<Project> projects { get; set; } //holds the m to n relatioship between Users and Projects

        //public virtual ICollection<ProjectTask> projectTasks { get; set; } //holds the m to n relatioship between Users and Projecttasks
          
    }
}