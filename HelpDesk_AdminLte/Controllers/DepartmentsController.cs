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
    public class DepartmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.Departments.ToList());
        }

        // GET: Departments/Details/5
         [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departments departments = db.Departments.Find(id);
            if (departments == null)
            {
                return HttpNotFound();
            }
            return View(departments);
        }

        [Authorize(Roles = "Admin")]
        // GET: Departments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "DepartmentID,Name,Location,Phone,Image,Active,Description")] Departments departments, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null)
                {
                    // получаем имя файла
                    string shortPath = "/Files/PhotoUser/" + System.IO.Path.GetFileName(upload.FileName);
                    // сохраняем файл в папку Files/ в проекте
                    string savePath = Server.MapPath("~/Files/PhotoUser/" + System.IO.Path.GetFileName(upload.FileName));
                    upload.SaveAs(savePath);
                    departments.Image = shortPath;
                }
                db.Departments.Add(departments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(departments);
        }

        // GET: Departments/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departments departments = db.Departments.Find(id);
            if (departments == null)
            {
                return HttpNotFound();
            }
            return View(departments);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "DepartmentID,Name,Location,Phone,Image,Active,Description")] Departments departments, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null)
                {
                    // получаем имя файла
                    string shortPath = "/Files/PhotoUser/" + System.IO.Path.GetFileName(upload.FileName);
                    // сохраняем файл в папку Files/ в проекте
                    string savePath = Server.MapPath("~/Files/PhotoUser/" + System.IO.Path.GetFileName(upload.FileName));
                    upload.SaveAs(savePath);
                    departments.Image = shortPath;
                }
                db.Entry(departments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(departments);
        }

        // GET: Departments/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departments departments = db.Departments.Find(id);
            if (departments == null)
            {
                return HttpNotFound();
            }
            return View(departments);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int ?id)
        {
            Departments departments = db.Departments.Find(id);
            db.Departments.Remove(departments);
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
