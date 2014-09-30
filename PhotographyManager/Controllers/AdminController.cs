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
            List<User> users = _unitOfWork.Users.GetMany(u => !u.Name.Equals(User.Identity.Name), u => u.Membership).ToList();
            foreach(User user in users)
            {
                if (Request.Form[user.Name]!=null&&Request.Form[user.Name]=="Free")
                {
                    _unitOfWork.Users.ChangeUsersTypeToFree(user.ID);
                    _unitOfWork.Commit();
                }
                else
                {
                    if (user.GetType().BaseType.Equals(typeof(FreeUser)))
                    {
                        _unitOfWork.Users.ChangeUsersTypeToPaid(user.ID);
                        _unitOfWork.Commit();
                    }
                }
            }
            return RedirectToAction("MyHomePage","Home");
        }       
    }
}
