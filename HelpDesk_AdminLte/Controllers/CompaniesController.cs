using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HelpDesk_AdminLte.Models;

namespace HelpDesk_AdminLte.Controllers
{
    [Authorize]
   
    public class CompaniesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Companies
     
        [Authorize(Roles = "Admin, Support")]
        public ActionResult Index(string clientId = null)
        {
            
                return View(db.Companies.ToList());
         
        }

        //public ActionResult Index(string clientId = null)
        //{
        //    if (clientId == null)
        //        return View(db.Companies.ToList());
        //    else
        //        return View(from cmp in db.Companies
        //                    join cln in db.Clients
        //                    on cmp.CompanyID equals cln.CompanyID into res
        //                    where cln.ClientID.ToString() != clientId
        //                    select new { CompanyID = cln.CompanyID }
        //                   );
        //}




        // GET: Companies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Companies companies = db.Companies.Find(id);
            if (companies == null)
            {
                return HttpNotFound();
            }
            return View(companies);
        }

        
        // GET: Companies/Create

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "CompanyID,Name,Active,Location,Email,Phone")] Companies companies)
        {
            if (ModelState.IsValid)
            {
                db.Companies.Add(companies);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(companies);
        }

        // GET: Companies/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Companies companies = db.Companies.Find(id);
            if (companies == null)
            {
                return HttpNotFound();
            }
            return View(companies);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "CompanyID,Name,Active,Location,Email,Phone")] Companies companies)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companies).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(companies);
        }

        // GET: Companies/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Companies companies = db.Companies.Find(id);
            if (companies == null)
            {
                return HttpNotFound();
            }
            return View(companies);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Companies companies = db.Companies.Find(id);
            db.Companies.Remove(companies);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
