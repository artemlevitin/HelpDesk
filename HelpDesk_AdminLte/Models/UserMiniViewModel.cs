using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpDesk_AdminLte.Models
{
    public class UserMiniViewModel
    {
        public string UserName { get; set; }

        public string Picture { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Departement { get; set; }

        public string Company { get; set; }

        public string Speciality { get; set; }

        public int CountMessage { get; set; }

        public string UserID { get; set; }


    }
}