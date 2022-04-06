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
using HelpDesk_AdminLte.Utilites;

namespace HelpDesk_AdminLte.Controllers
{
    [Authorize]
    public class MessagesController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
      
        

        // GET: Messages
        public async Task<ActionResult> Index()
        {
            return View(await _db.Messages.ToListAsync());
        }

       
         public async Task<ActionResult> PersonalChat(string oponentUserId)
        {

            string selfUserID = User.Identity.GetUserId();

            string oponentUserID = oponentUserId != null ? oponentUserId : selfUserID;

            var conversation = await _db.Conversation.Where(c => c.OneUserID == selfUserID || c.TwoUserID == selfUserID).
                                                Where(c => c.OneUserID == oponentUserID || c.TwoUserID == oponentUserID).FirstOrDefaultAsync();
            if (conversation == null)
            {
               conversation = new Conversation(){ OneUserID = selfUserID,TwoUserID = oponentUserID};
                _db.Conversation.Add(conversation);
               await _db.SaveChangesAsync();
            }
            
            var supports= _db.Support.Where(s => s.Active == true && s.UserID != selfUserID && s.UserID != null).ToList();
            var usersSupoortViewList = new List<UserMiniViewModel>();
            foreach (var s in supports)
            {
                usersSupoortViewList.Add(new CreateUserConversationViewModel().getById(s.UserID,selfUserID));

            }

            var clients = _db.Clients.Where(c => c.Active == true && c.UserID != selfUserID && c.UserID != null).ToList();
            var usersClientViewList = new List<UserMiniViewModel>();
            foreach (var c in clients)
            {
                usersClientViewList.Add(new CreateUserConversationViewModel().getById(c.UserID, selfUserID));

            }

            ViewBag.SupportChat = usersSupoortViewList;
            ViewBag.ClientChat = usersClientViewList;
            return View(conversation);

        }

       

       

        // POST: Messages/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(/*[Bind(Include = "MessageID, Conversation, DateCreate,DateDelivery,Text,FromUserID")]*/ Messages messages, string opUserId)
        {
            if (ModelState.IsValid)
            {
                var conv = await _db.Conversation.Where(c => c.ConversationID == messages.ConversationID).SingleOrDefaultAsync();

                foreach (var m in conv.Messages)
                {
                    m.DateRead =( m.DateRead == null & m.FromUserID != messages.FromUserID ) ? messages.DateCreate : m.DateRead;
                 }

                _db.Messages.Add(messages);
                await _db.SaveChangesAsync();
                
                var msg = _db.Messages.AsEnumerable().LastOrDefault();
                await sendMailMessaqge(msg);


                return RedirectToAction("PersonalChat","Messages", new { oponentUserId = opUserId });

            }

            return  RedirectToAction("PersonalChat", "Messages", new { oponentUserId = opUserId } );
        }

       
      
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int conversationID, string opUserId)
        {
            var messages = await _db.Messages.Where(m => m.ConversationID == conversationID).ToListAsync();
            _db.Messages.RemoveRange(messages);
            await _db.SaveChangesAsync();
            return RedirectToAction("PersonalChat", "Messages", new { oponentUserId = opUserId });
        }

        private async Task sendMailMessaqge(Messages messages)
        {

            Clients fromNameClient_Message=null;
            try
            {
                fromNameClient_Message = await _db.Clients.Where(с => с.UserID == messages.FromUserID).SingleAsync();
            }
            catch (Exception) { }

            Support fromNameSupport_Message=null;
            try
            {
                fromNameSupport_Message = await _db.Support.Where(s => s.UserID == messages.FromUserID).SingleAsync();
            }
            catch (Exception) { }

            var fromNameMessage = fromNameClient_Message != null ? fromNameClient_Message.Name : fromNameSupport_Message.Name;

            var linkToReq = HtmlHelper.GenerateLink(this.ControllerContext.RequestContext,
                   System.Web.Routing.RouteTable.Routes,
                   "Получено сообщение", null, "PersonalChat", "Messages", null, Request.Url.Host, null, null, null);

            var toMessage = await _db.Users.Where(u => u.Id == messages.ToUserID).SingleAsync();

            var body = "<h4>Вы получили сообщение от " + fromNameMessage + ".</h4> <p> Перейти " + linkToReq + "</p> Дата: " + messages.DateCreate;
 
            await new EmailService().SendAsync(new IdentityMessage() { Body = body, Destination = toMessage.Email, Subject = "Cообщение от " + fromNameMessage + " получено" });
          
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
