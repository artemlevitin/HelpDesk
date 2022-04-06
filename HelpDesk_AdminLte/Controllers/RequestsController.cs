using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HelpDesk_AdminLte.Models;
using Microsoft.AspNet.Identity;
using System.Web.Routing;
using HelpDesk_AdminLte.Utilites;

namespace HelpDesk_AdminLte.Controllers
{
    [Authorize]
    public class RequestsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Requests

        [Authorize(Roles = "Admin, Support")]
        public async Task<ActionResult> Index()
        {
           
            return View(await db.Requests.Include(r => r.Clients).
                                                        Include(r => r.Priority).Include(r => r.Status).Include(r => r.Support).Include(r => r.User).ToListAsync());
        }

        public async Task<ActionResult> CreatorI(string idUser)
        {
            return View("Index",await db.Requests.Where(r=>r.CreatedByUserId==idUser).
                                                        Include(r => r.Clients).Include(r => r.Priority).Include(r => r.Status).Include(r => r.Support).Include(r => r.User).ToListAsync());
        }


        public async Task<ActionResult> CompanyBy(string idClient)
        {
           
                var cln = await db.Clients.Where(c => c.ClientID.ToString() == idClient).SingleAsync();
                var idCompany = cln.CompanyID;

                var reqs = await db.Requests.Include(r => r.Clients).Include(r => r.Priority).Include(r => r.Status).Include(r => r.Support).Include(r => r.User).
                                                    ToListAsync();
           
                return View("Index", reqs.Where(r => r.Clients.CompanyID == cln.CompanyID).ToList());
       
          
        }

        [Authorize(Roles = "Admin, Support")]
        public async Task<ActionResult> SupportI(string idSupport)
        {
            var _requestsActive = (IEnumerable<Requests>)db.Requests.Where(req => req.SupportID.ToString().Equals(idSupport));
            return View("Index", await db.Requests.Where(req => req.SupportID.ToString().Equals(idSupport)).
                                                        Include(r => r.Clients).Include(r => r.Priority).Include(r => r.Status).Include(r => r.Support).Include(r => r.User).ToListAsync());
        }
        // GET: Requests/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requests requests = await db.Requests.FindAsync(id);
            if (requests == null)
            {
                return HttpNotFound();
            }
            return View(requests);
        }

        // GET: Requests/Create
        public ActionResult Create()
        {
           
            ViewBag.ClientID = Utils.AddFirstItem(new SelectList(db.Clients.Where(c => c.Active == true), "ClientID", "Name"));
            ViewBag.PriorityID = new SelectList(db.Priority, "PriorityID", "Name");
            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "Name");
            ViewBag.SupportID = Utils.AddFirstItem(new SelectList(db.Support.Where(s => s.Active == true), "SupportID", "Name"));
            return View();
        }

        // POST: Requests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "RequestID,CreatedByUserId,DateRequest,NameRequest,Description,ClientID,SupportID,StatusID,CommentResolve,PriorityID,File")] Requests requests, HttpPostedFileBase upload)
        {
             if (ModelState.IsValid)
            {
                if (!(User.IsInRole("Admin")|| User.IsInRole("Support")) )
                {
                    if(requests.CommentResolve != null)
                    return View(requests);
                }

                requests.DateCreateRequest = DateTime.Now;
                
                if (upload != null)
                {
                    // получаем имя файла
                    requests.File = System.IO.Path.GetFileName(upload.FileName);
                    requests.FilePath = System.IO.Path.GetRandomFileName();
                    // сохраняем файл в папку Files/Requests в проекте
                    string path = Server.MapPath("~/Files/Requests" +"/" + requests.FilePath);
                    upload.SaveAs(path);             
                }
                db.Requests.Add(requests);
                 await db.SaveChangesAsync();

                var request = db.Requests.Include(r => r.Priority).Include(r => r.Status).Include(r => r.Support).Include(r => r.User).Include(r => r.Clients).AsEnumerable().LastOrDefault();
                await sendMailRequest(request);

                return RedirectToAction("CreatorI", "Requests", new { idUser = User.Identity.GetUserId() });
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                                       .Where(y => y.Count > 0)
                                       .ToList();

                ViewBag.ClientID = Utils.AddFirstItem(new SelectList(db.Clients.Where(c => c.Active == true), "ClientID", "Name", requests.Clients));
                ViewBag.PriorityID = new SelectList(db.Priority, "PriorityID", "Name");
                ViewBag.StatusID = new SelectList(db.Status, "StatusID", "Name");
                ViewBag.SupportID = Utils.AddFirstItem(new SelectList(db.Support.Where(s => s.Active == true), "SupportID", "Name", requests.Support));
                return View(requests);
            }
        }

        // GET: Requests/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requests requests = await db.Requests.FindAsync(id);
            if (requests == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientID = Utils.AddFirstItem(new SelectList(db.Clients.Where(c => c.Active == true), "ClientID", "Name",requests.ClientID));
            ViewBag.PriorityID = new SelectList(db.Priority, "PriorityID", "Name",requests.PriorityID);
            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "Name",requests.StatusID);
            ViewBag.SupportID = Utils.AddFirstItem((new SelectList(db.Support.Where(s => s.Active == true), "SupportID", "Name",requests.SupportID)));
            return View(requests);
        }

        // POST: Requests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "RequestID,CreatedByUserId,DateRequest,NameRequest,Description,ClientID,SupportID,StatusID,DateCreateRequest,CommentResolve,PriorityID,File, FilePath")] Requests requests, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {


                if (upload != null)
                {
                    // получаем имя файла
                    requests.File = System.IO.Path.GetFileName(upload.FileName);
                    
                    requests.FilePath = requests.FilePath!=null? requests.FilePath : System.IO.Path.GetRandomFileName();
                    // сохраняем файл в папку Files/Requests в проекте
                    string path = Server.MapPath("~/Files/Requests" + "/" + requests.FilePath);
                    upload.SaveAs(path);
                }

                db.Entry(requests).State = EntityState.Modified;
                await db.SaveChangesAsync();

                var request = await db.Requests.Include(r => r.Priority).Include(r => r.Status).Include(r => r.Support).Include(r => r.User).Include(r => r.Clients).SingleAsync(r => r.RequestID == requests.RequestID); 
                await sendMailRequest(request);

                return RedirectToAction("Details","Requests",new  { id= requests.RequestID });
            }
            ViewBag.ClientID = Utils.AddFirstItem(new SelectList(db.Clients.Where(c => c.Active == true), "ClientID", "Name", requests.ClientID));
            ViewBag.PriorityID = new SelectList(db.Priority, "PriorityID", "Name", requests.PriorityID);
            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "Name", requests.StatusID);
            ViewBag.SupportID = Utils.AddFirstItem(new SelectList(db.Support.Where(s => s.Active == true), "SupportID", "Name", requests.SupportID));
            return View(requests);
        }

        // GET: Requests/Delete/5
        [Authorize(Roles = "Admin, Support")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requests requests = await db.Requests.FindAsync(id);
            if (requests == null)
            {
                return HttpNotFound();
            }
            return View(requests);
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Requests requests = await db.Requests.FindAsync(id);
            db.Requests.Remove(requests);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public FileResult GetFile(string fileName, string filePath)
        {
            // Путь к файлу
            string file_path = Server.MapPath("~/Files/Requests/" + filePath);
            // Тип файла - content-type
            string file_type = "application/txt";

            return File(file_path, file_type, fileName);
        }

        private async Task sendMailRequest(Requests request)
        {
            
            var linkToReq = HtmlHelper.GenerateLink(this.ControllerContext.RequestContext,
                    System.Web.Routing.RouteTable.Routes,
                    request.NameRequest, null, "Details", "Requests", null, Request.Url.Host, null,
                    new RouteValueDictionary(new {id = request.RequestID }), null);

            var body = "<h4>Заявка " + request.RequestID + " изменена.</h4> <p>Cтатус:<strong> " + request.Status.Name + ".</strong></p> <p> Перейти " + linkToReq + "</p> Дата: " + DateTime.Now;

            var destanationSet = new HashSet<string>();

            destanationSet.Add(request.User.Email);

            if (request.Support!=null) {
                destanationSet.Add(request.Support.Email);
            }
           
            if (request.Clients != null)
            {
                destanationSet.Add(request.Clients.Email);

                if (request.Clients.Companies != null)
                {
                    destanationSet.Add(request.Clients.Companies.Email);
                }
            }
           
            foreach (var destanation in destanationSet){
                await new EmailService().SendAsync(new IdentityMessage() { Body = body, Destination = destanation, Subject = "Заявка " + request.RequestID + " изменена" });
            }
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
