using HelpDesk_AdminLte.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpDesk_AdminLte.Utilites
{
    public class CreateUserConversationViewModel
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private Clients _client;
        private Support _support;
       // private ApplicationUser _userIdent;
        private readonly ApplicationUserManager _userManagIdent = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
       
      


        public UserMiniViewModel getById(string selfUserID, string oponentUserID=null)
        {
            var userView = new UserMiniViewModel();
           

            if (selfUserID != null)
            {
                userView.CountMessage = db.Messages.Where(mes => mes.DateRead == null).
                                                                            Where(mes => mes.FromUserID == selfUserID && mes.ToUserID == oponentUserID).Count();
                // _userIdent = _userManagIdent.FindById(selfUserID);

                try
                {

                    _client = db.Clients.SingleOrDefault(user => user.UserID == selfUserID);

                    
                    if (_client != null)
                    {
                        userView.UserID = _client.UserID;
                        userView.Phone = _client.Phone;
                        userView.UserName = _client.Name;
                        userView.Picture = _client.Image;
                        userView.Email = _client.Email;
                        userView.Company = _client.Companies.Name;
                        
                        return userView;
                    }

                }
                catch (Exception ex) { return null; }

                try
                {
                    _support = db.Support.SingleOrDefault(user => user.UserID == selfUserID);
                    if (_support != null)
                    {
                        userView.UserID = _support.UserID;
                        userView.Phone = _support.Phone;
                        userView.UserName = _support.Name;
                        userView.Picture = _support.Image==null ? @"/Files/PhotoUser/User.png": _support.Image;
                        userView.Email = _support.Email;
                        userView.Speciality = _support.Speciality;
                        userView.Departement = _support.Departments.Name;
                        

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