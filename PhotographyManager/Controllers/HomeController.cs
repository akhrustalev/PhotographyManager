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

        public HomeController(IUnitOfWork uoW)
        {
            
            _unitOfWork = uoW;
        }
        public ActionResult Index()
        {
            return View("Index", _unitOfWork);

        }

        public ActionResult MyHomePage()
        {
            return View(_unitOfWork.Users.GetById((int)Membership.GetUser().ProviderUserKey));
        }


    }
}
