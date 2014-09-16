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
            return RedirectToAction("MyHomePage","Home");
        }
        

    }
}
