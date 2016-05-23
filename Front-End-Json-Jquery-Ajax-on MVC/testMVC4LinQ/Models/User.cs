using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace testMVC4LinQ.Models
{
    public class User
    {
        [Key]
        public string Usuario { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        [Required]
        public bool Habilitado { get; set; }
    }
}