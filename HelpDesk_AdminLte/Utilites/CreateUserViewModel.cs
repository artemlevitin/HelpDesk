using HelpDesk_AdminLte.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpDesk_AdminLte.Utilites
{
    public class CreateUserViewModel
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private Clients _client;
        private Support _support;
        private ApplicationUser _userIdent;
        private readonly ApplicationUserManager _userManagIdent = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        private IEnumerable<Requests> _requestsActive;
        private IEnumerable<Requests> _requestsExpired;
        private IEnumerable<Requests> _requestsCreated;
      


        public UserViewModel getById(string userID)
        {
            var userView = new UserViewModel();
           

            if (userID != null)
            {
                
                _userIdent = _userManagIdent.FindById(userID);

                try
                {
                    
                    userView.Count_MessageNew = db.Messages.Where(mes => mes.DateRead == null).
                                                    Where(mes => mes.Conversation.OneUserID == userID || mes.Conversation.TwoUserID == userID).
                                                    Where(mes => mes.FromUserID != userID).Count();

                    _client = db.Clients.SingleOrDefault(user => user.UserID == userID);
                    if (_client != null)
                    {
                       //userView.UserIsClient = true;
                        userView.Email = _client.Email;
                        userView.Phone = _client.Phone;
                        userView.UserName = _client.Name;
                        userView.Picture = _client.Image == null ? @"/Files/PhotoUser/User.png" : _client.Image;
                        userView.Company = _client.Companies.Name;
                        
                        userView.IdSupportOrClient = _client.ClientID.ToString();
                        _requestsActive = (IEnumerable<Requests>)db.Requests.Where(req => req.CreatedByUserId.ToString().Equals(_client.UserID.ToString()));
                        _requestsExpired = (IEnumerable<Requests>)_requestsActive.Where(req => req.DateRequest < DateTime.Today);
                        _requestsCreated = (IEnumerable<Requests>)db.Requests.Where(req => req.CreatedByUserId.ToString().Equals(userID));


                        userView.Count_RequestsActive = (_requestsActive != null) ? _requestsActive.Count() : 0;
                        userView.Count_RequestsExpired = (_requestsExpired != null) ? _requestsExpired.Count() : 0;
                        userView.Name_RequestsExpired = (_requestsExpired != null) ? _requestsExpired.Select(r => r.RequestID + ") " + r.NameRequest).ToList() : null;
                        userView.Count_RequestsCreated = (_requestsCreated != null) ? _requestsCreated.Count() : 0;
                        return userView;
                    }

                   _support = db.Support.SingleOrDefault(user => user.UserID == userID);
                    if (_support != null)
                    {
                        //userView.UserIsClient = false;
                        userView.Email = _support.Email;
                        userView.Phone = _support.Phone;
                        userView.UserName = _support.Name;
                        userView.Picture = _support.Image == null ? @"/Files/PhotoUser/User.png" : _support.Image;
                        userView.Speciality = _support.Speciality;
                        userView.IdSupportOrClient = _support.SupportID.ToString();
                        userView.Department = _support.Departments.Name;

                        _requestsActive = (IEnumerable<Requests>)db.Requests.Where(req => req.SupportID.ToString().Equals(_support.SupportID.ToString()));
                        _requestsExpired = (IEnumerable<Requests>)_requestsActive.Where(req => req.DateRequest < DateTime.Today);
                        _requestsCreated = (IEnumerable<Requests>)db.Requests.Where(req => req.CreatedByUserId.ToString().Equals(userID));


                        userView.Count_RequestsActive = (_requestsActive != null) ? _requestsActive.Count() : 0;
                        userView.Count_RequestsExpired = (_requestsExpired != null) ? _requestsExpired.Count() : 0;
                        userView.Name_RequestsExpired = (_requestsExpired != null) ? _requestsExpired.Select(r => r.RequestID + ") " + r.NameRequest).ToList() : null;
                        userView.Count_RequestsCreated = (_requestsCreated != null) ? _requestsCreated.Count() : 0;

                        return userView;
                    }
                }
                catch (Exception ex) { return null; }
            }
            userView.UserName = "Гость";
            userView.Picture = @"/Files/PhotoUser/User.png";

            return userView;

        }
    }
}