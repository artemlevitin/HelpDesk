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
    public class ClientsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Clients
        [Authorize(Roles = "Admin, Support")]
        public ActionResult Index()
        {
            var clients = db.Clients.Include(c => c.Companies);
            return View(clients.ToList());
        }

        // GET: Clients/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clients clients = db.Clients.Find(id);
            if (clients == null)
            {
                return HttpNotFound();
            }
            if (clients.UserID != null)
            {
                ViewBag.AppUser = db.Users.Find(clients.UserID).UserName;
            }
            return View(clients);
        }

        // GET: Clients/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.CompanyID = Utils.AddFirstItem(new SelectList(db.Companies.Where(cm => cm.Active == true), "CompanyID", "Name"));
            ViewBag.UserID = getSelectListUser();
            return View();
        }
        

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "ClientID,Name,Phone,Email,UserID,CompanyID,Image,Active")] Clients clients, HttpPostedFileBase upload)
        {
            
            if (ModelState.IsValid && !db.Clients.Any(c => c.Name == clients.Name.ToString()))
            {
              
                if (upload != null)
                {
                    // получаем имя файла
                    string shortPath = "/Files/PhotoUser/" + System.IO.Path.GetFileName(upload.FileName);
                    // сохраняем файл в папку Files/ в проекте
                    string savePath = Server.MapPath("~/Files/PhotoUser/" + System.IO.Path.GetFileName(upload.FileName));
                    upload.SaveAs(savePath);
                    clients.Image = shortPath;
                }
                
                db.Clients.Add(clients);
                try
                {
                    db.SaveChanges();
                }
                catch(Exception ex) { return View("error"); }
                return RedirectToAction("Index"); 
            }

            ViewBag.CompanyID = Utils.AddFirstItem(new SelectList(db.Companies.Where(cm=>cm.Active==true), "CompanyID", "Name", clients.CompanyID));
            if(clients.UserID==null)
                ViewBag.UserID = getSelectListUser();

            ViewBag.ErrorMsg = "Неверно заполнено поле";
           
            return View(clients);
        }
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
        // GET: Clients/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clients clients = db.Clients.Find(id);
            if (clients == null)
            {
                return HttpNotFound();
            }

            ViewBag.CompanyID = Utils.AddFirstItem(new SelectList(db.Companies.Where(cm => cm.Active == true), "CompanyID", "Name", clients.CompanyID));

            if (clients.UserID == null)
                ViewBag.UserID = getSelectListUser();

            return View(clients);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "ClientID,Name,Phone,Email,UserID,CompanyID,Image,Active")] Clients clients, HttpPostedFileBase upload)
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
                    clients.Image = shortPath;
                }
                db.Entry(clients).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyID = Utils.AddFirstItem(new SelectList(db.Companies.Where(cm => cm.Active == true), "CompanyID", "Name", clients.CompanyID));
            if (clients.UserID == null)
                ViewBag.UserID = getSelectListUser();
            return View(clients);
        }

        // GET: Clients/Delete/5
        //public ActionResult Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Clients clients = db.Clients.Find(id);
        //    if (clients == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(clients);
        //}

        //// POST: Clients/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(Guid id)
        //{
        //    Clients clients = db.Clients.Find(id);
        //    db.Clients.Remove(clients);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        public FileResult GetFile(string fileName)
        {
            // Путь к файлу
            string file_path = Server.MapPath("~/Files/PhotoUser/" + fileName);
            // Тип файла - content-type
            string file_type = "image";

            return File(file_path, file_type, fileName);
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
