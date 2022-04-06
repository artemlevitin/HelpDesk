using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelpDesk_AdminLte.Models
{
    public class DashboardViewModel
    {
        [Display(Name = "Компании")]
        public IEnumerable< RequestByInfo> requestByCompany { get; set; }

        [Display(Name = "Клиенты")]
        public IEnumerable<RequestByInfo> requestByClient { get; set; }

        [Display(Name = "Департаменты")]
        public IEnumerable<RequestByInfo> requestByDepartment { get; set; }

        [Display(Name = "Приоритеты")]
        public IEnumerable<RequestByInfo> requestByPriority { get; set; }

        [Display(Name = "Статус")]
        public IEnumerable<RequestByInfo> requestByStatus { get; set; }

        [Display(Name = "Сотрудники")]
        public IEnumerable<RequestByInfo> requestBySupport { get; set; }

        

        [Display(Name = "Пользователи")]
        public int CountUser { get; set; }

        [Display(Name = "Департаменты")]
        public int CountDepartment { get; set; }

        [Display(Name = "Компании")]
        public int CountCompanies { get; set; }

        [Display(Name = "Заявки")]
        public int CountRequest { get; set; }

        [Display(Name = "Сообщения")]
        public int CountMessage { get; set; }

        
        [Display(Name = "Клиенты")]

        public int CountClient { get; set; }
        // public SelectList ClientList { get; set; }



        public class RequestByInfo
        {
            public string Name { get; set; }
            public int CountReq { get; set; }

        }

      
    }

    
}