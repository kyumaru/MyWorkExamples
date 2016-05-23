using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.ViewModels
{
    public class AssignedUserData
    {
        public int UserID { get; set; }

        public string Name { get; set; }
        public string Major { get; set; }
        public string Ucard { get; set; }//carnet

        public bool Assigned { get; set; }
    }
}