using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Threading.Tasks;

using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace HelpDesk_AdminLte.Models
{
    public class UserViewModel
    {
       
        public UserViewModel() { }
      

        public SelectList Role { get; set; }
        public string IdSupportOrClient { get; set; }

        public int? Count_RequestsActive { get; set; }
        public int? Count_RequestsExpired { get; set; }

        public int? Count_RequestsCreated { get; set; }

        public int? Count_MessageNew { get; set; }
        public List<string> Name_RequestsExpired { get; set; }

        public string UserName { get; set; }

        public string Picture { get; set; }



        public string CompanyID { get; set; }
        public string Company { get; set; }
        public string Speciality { get; set; }

        public string Department { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

       
    }
}