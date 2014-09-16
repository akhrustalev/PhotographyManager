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
using PhotographyManager.Web.Filters;

namespace PhotographyManager.Web.Controllers
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
            return View(_unitOfWork.Users.GetOne(user => user.Name.Equals(User.Identity.Name),user=>user.Album.Select(album=>album.Photo.Select(photo=>photo.PhotoImage)),user=>user.Roles,user=>user.Membership));
        }
    }
}
