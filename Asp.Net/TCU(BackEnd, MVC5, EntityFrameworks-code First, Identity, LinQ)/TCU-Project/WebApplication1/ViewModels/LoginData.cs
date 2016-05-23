using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.ViewModels
{
    public class LoginData
    {
        [Required] //the form will not submit, such annotations are constrains over the linked view
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email format.")]
        [NotMapped]
        public string Email { get; set; }
      

        //http://stackoverflow.com/questions/22936021/ef-code-first-encrypt-decrypt-password-property
        [Required]
        [NotMapped]
        //[StringLength(20, ErrorMessage = "password cannot be longer than 20 characters.")]
        [DataType(DataType.Password)]
        public string Password
        {
            get;
            set;
        }
    }
}