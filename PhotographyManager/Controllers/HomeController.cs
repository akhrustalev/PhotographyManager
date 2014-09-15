using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotographyManager.DataAccess.UnitOfWork;
using PhotographyManager.Model;
using System.Data;
using System.IO;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.Security;
using PhotographyManager.Services;
using System.Data.SqlClient;
using System.Globalization;
using PhotographyManager.Filters;

namespace PhotographyManager.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IUnitOfWork unitOfWork):base(unitOfWork)
        {
        }
        public ActionResult Index()
        {
            return View("Index");
        }
        public ActionResult MyHomePage()
        {
            return View(_unitOfWork.Users.GetOne(user => user.Name.Equals(User.Identity.Name)));
        }
        [Authorize(Roles="Admin")]
        public ActionResult ManageUsers()
        {
            User currentUser = _unitOfWork.Users.GetOne(user => user.Name.Equals(User.Identity.Name));
            List<User> users = new List<User>();
            users = _unitOfWork.Users.GetMany(user => user.ID != currentUser.ID).ToList();
            return View("Admin",users);
        }
    }
}
