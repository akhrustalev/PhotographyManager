using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotographyManager.Web.Filters;
using PhotographyManager.Model;
using PhotographyManager.DataAccess.UnitOfWork;

namespace PhotographyManager.Web.Controllers
{
    [PhotographyManagerAuthorize(Roles="Admin")]
    public class AdminController :BaseController
    {
        public AdminController(IUnitOfWork unitOfWork):base(unitOfWork)
        {
        }
        public ActionResult ManageUsers()
        {
            User currentUser = _unitOfWork.Users.GetOne(user => user.Name.Equals(User.Identity.Name));
            List<User> users = new List<User>();
            users = _unitOfWork.Users.GetMany(user => user.ID != currentUser.ID).ToList();
            return View("ManageUsers", users);
        }

        public ActionResult AddChangedUsers()
        {
            foreach(User user in _unitOfWork.Users.GetMany(u=>u.ID>=0,u=>u.Membership))
            {
                if (Request.Form[user.Name+"Free"] == "On")
                {
                    if (!user.GetType().BaseType.Equals(typeof(FreeUser)))
                    {
                        User changedUser = new FreeUser { Name = user.Name, Photo = user.Photo, Album = user.Album, Roles = user.Roles, ID = user.ID };
                        UserMembership membership = new UserMembership { ID = changedUser.ID, Password = user.Membership.Password, PasswordSalt = user.Membership.PasswordSalt, User = changedUser };
                        changedUser.Membership = membership;
                        _unitOfWork.Photos.GetOne(p => p.UserID == user.ID).User = changedUser;
                        _unitOfWork.Albums.GetOne(a => a.UserID == user.ID).User = changedUser;
                        _unitOfWork.Users.Remove(_unitOfWork.Users.GetById(user.ID));
                        _unitOfWork.Users.GetById(user.ID).Membership = null;
                        _unitOfWork.UserMembership.GetOne(u => u.ID == user.ID).User = null;
                        _unitOfWork.UserMembership.Remove(_unitOfWork.UserMembership.GetOne(u => u.ID == user.ID));
                        _unitOfWork.UserMembership.Add(membership);
                        _unitOfWork.Users.Add(changedUser);
                        
                    }
                }
                else
                {
                    if (user.GetType().BaseType.Equals(typeof(FreeUser)))
                    {
                        User changedUser = new PaidUser { Name = user.Name, Photo = user.Photo, Album = user.Album, Roles = user.Roles, ID = user.ID };
                        UserMembership membership = new UserMembership { ID = changedUser.ID, Password = user.Membership.Password, PasswordSalt = user.Membership.PasswordSalt, User = changedUser };
                        changedUser.Membership = membership;
                        _unitOfWork.Photos.GetOne(p => p.UserID == user.ID).User = changedUser;
                        _unitOfWork.Albums.GetOne(a => a.UserID == user.ID).User = changedUser;
                        _unitOfWork.Users.Remove(_unitOfWork.Users.GetById(user.ID));
                        _unitOfWork.Users.GetById(user.ID).Membership = null;
                        _unitOfWork.UserMembership.GetOne(u => u.ID == user.ID).User = null;
                        _unitOfWork.UserMembership.Remove(_unitOfWork.UserMembership.GetOne(u => u.ID == user.ID));
                        _unitOfWork.UserMembership.Add(membership);
                        _unitOfWork.Users.Add(changedUser);
                    }
                }
            }
            _unitOfWork.Commit();
            return RedirectToAction("MyHomePage","Home");
        }
        

    }
}
