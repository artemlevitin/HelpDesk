using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HelpDesk_AdminLte.Models;
using HelpDesk_AdminLte.Utilites;

namespace HelpDesk_AdminLte.Controllers
{
    [Authorize]
    public class SupportsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Supports
        public ActionResult Index()
        {
            var support = db.Support.Include(s => s.Departments);
            return View(support.ToList());
        }

        // GET: Supports/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Support support = db.Support.Find(id);
            if (support == null)
            {
                return HttpNotFound();
            }
            //ViewBag.DepartamentID = new SelectList(db.Departments, "DepartmentID", "Name");
            if (support.UserID != null)
            {
                ViewBag.AppUser = db.Users.Find(support.UserID).UserName;
            }
            

            return View(support);
        }

        // GET: Supports/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.DepartamentID = new SelectList(db.Departments, "DepartmentID", "Name");
            ViewBag.UserID = getSelectListUser();
            return View();
        }

        // POST: Supports/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "SupportID,Name,Speciality,Phone,Email,DepartamentID,UserID,Image,Active")] Support support, HttpPostedFileBase upload)
        {
            if(ModelState.IsValid && !db.Support.Any(s => s.Name == support.Name.ToString()))
            {
                if (upload != null)
                {
                    // получаем имя файла
                    string shortPath = "/Files/PhotoUser/" + System.IO.Path.GetFileName(upload.FileName);
                    // сохраняем файл в папку Files/ в проекте
                    string savePath = Server.MapPath("~/Files/PhotoUser/" + System.IO.Path.GetFileName(upload.FileName));
                    upload.SaveAs(savePath);
                    support.Image = shortPath;
                }
                support.SupportID = Guid.NewGuid();
                db.Support.Add(support);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartamentID = new SelectList(db.Departments, "DepartmentID", "Name", support.DepartamentID);
            ViewBag.UserID = getSelectListUser();
            ViewBag.ErrorMsg = "Неверно заполнено поле";
            return View(support);
        }

        // GET: Supports/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Support support = db.Support.Find(id);
            if (support == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartamentID = new SelectList(db.Departments, "DepartmentID", "Name", support.DepartamentID);
            if (support.UserID == null)
                          ViewBag.UserID=getSelectListUser();

            return View(support);
        }

        // POST: Supports/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "SupportID,Name,Speciality,Phone,Email,DepartamentID,UserID,Image,Active")] Support support, HttpPostedFileBase upload)
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
                    support.Image = shortPath;
                }
                

                db.Entry(support).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartamentID = new SelectList(db.Departments, "DepartmentID", "Name", support.DepartamentID);
            if (support.UserID == null)
                ViewBag.UserID = getSelectListUser();

            return View(support);
        }

        // GET: Supports/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Support support = db.Support.Find(id);
            if (support == null)
            {
                return HttpNotFound();
            }
            return View(support);
        }

        // POST: Supports/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        //public ActionResult DeleteConfirmed(Guid id)
        //{
        //    Support support = db.Support.Find(id);
        //    db.Support.Remove(support);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        private SelectList getSelectListUser()
        {
            
                var notSupport = from u in db.Users
                                 join s in db.Support
                                 on u.Id equals s.UserID into f
                                 where f.Count() == 0
                                 select new IdNameUser
                                 {
                                     Id = u.Id,
                                     Name = u.UserName
                                 };
            
                var last =
                                new SelectList(from u in notSupport
                                               join c in db.Clients
                                   on u.Id equals c.UserID into f
                                               where f.Count() == 0
                                               select new
                                               {
                                                   Id = u.Id,
                                                   UserName = u.Name
                                               }
                                                       , "Id", "UserName");
                return Utils.AddFirstItem(last);
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
