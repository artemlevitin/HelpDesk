using HelpDesk_AdminLte.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static HelpDesk_AdminLte.Models.DashboardViewModel;

namespace HelpDesk_AdminLte.Controllers
{
    public class DashboardController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        
        // GET: Dashboard
        public ActionResult Index()
        {
           var dashboardViewModel = new DashboardViewModel() ;
           var allRequest = db.Requests.Count();
            // var compan = new SelectList(db.Companies, "CompanyID", "Name");
          
            dashboardViewModel.CountUser = db.Users.Count();

            dashboardViewModel.CountDepartment = db.Departments.Count();

            dashboardViewModel.CountCompanies = db.Companies.Count();

            dashboardViewModel.CountRequest = db.Requests.Count();

            dashboardViewModel.CountClient = db.Clients.Count();

            dashboardViewModel.CountMessage = db.Messages.Count();


           // dashboardViewModel.requestByCompany = null;  //from comp in db.Companies
            //                                       join req in db.Requests on comp.CompanyID equals req.CompanyID into cmpReq1
            //                                       from cmpReq2 in cmpReq1.DefaultIfEmpty()
            //                                       group cmpReq2 by comp.Name into cmpReqGroup
            //                                       select new RequestByInfo() { Name= cmpReqGroup.Key, CountReq= cmpReqGroup.Count() };

            //dashboardViewModel.requestByClient = from clnt in db.Clients
            //                                     join req in db.Requests on clnt.ClientID equals req. into cmpReq1
            //                                     from cmpReq2 in cmpReq1.DefaultIfEmpty()
            //                                     group cmpReq2 by comp.Name into cmpReqGroup
            //                                     select new RequestByInfo() { Name = cmpReqGroup.Key, CountReq = cmpReqGroup.Count() };

            dashboardViewModel.requestBySupport = from sup in db.Support
                                                  join req in db.Requests on sup.SupportID equals req.SupportID into Req1
                                                  from Req2 in Req1.DefaultIfEmpty()
                                                  group Req2 by sup.Name into supReqGroup
                                                  select new RequestByInfo() { Name = supReqGroup.Key, CountReq = supReqGroup.Count() };

            //dashboardViewModel.requestByDepartment = from dep in db.Departments
            //                                      join req in db.Requests on dep.DepartmentID equals req. into Req1
            //                                      from Req2 in Req1.DefaultIfEmpty()
            //                                      group Req2 by sup.Name into supReqGroup
            //                                      select new RequestByInfo() { Name = supReqGroup.Key, CountReq = supReqGroup.Count() };

            dashboardViewModel.requestByStatus = from status in db.Status
                                                  join req in db.Requests on status.StatusID equals req.StatusID into Req1
                                                  from Req2 in Req1.ToList()
                                                  group Req2 by status.Name into statusReqGroup
                                                  select new RequestByInfo() { Name = statusReqGroup.Key, CountReq = statusReqGroup.Count() };

            dashboardViewModel.requestByPriority = from priority in db.Priority
                                                 join req in db.Requests on priority.PriorityID equals req.PriorityID into Req1
                                                 from Req2 in Req1.DefaultIfEmpty()
                                                 group Req2 by priority.Name into priorityReqGroup
                                                 select new RequestByInfo() { Name = priorityReqGroup.Key, CountReq = priorityReqGroup.Count() };

            //var regByCompany = new SelectList( db.Requests.Where(r=>r.CompanyID==1).Include(r => r.Companies).Include(r => r.Priority).Include(r => r.Status), "Companies", "Priority", "Status");
            // var depBy = "CompanyID", "Name");.Where(r => r.Support.Active == true), "CompanyID", "Name");
            //foreach(var r in regByCompany)
            //{
            //   string text = r.Text;
            //    string value = r.Value;
            //}
            return View(dashboardViewModel);
        }

        public ActionResult Dashboardv1()
        {
            return View();
        }

        public ActionResult Dashboardv2()
        {
            return View();
        }
    }
}