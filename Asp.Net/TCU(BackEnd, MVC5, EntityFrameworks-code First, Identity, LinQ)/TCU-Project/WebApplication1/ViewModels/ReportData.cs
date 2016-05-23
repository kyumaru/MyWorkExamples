using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class ReportData
    {
        public int totalHours { get; set; }// buffer for total hours
        public User user { get; set; }//buffer for all users
    }
}