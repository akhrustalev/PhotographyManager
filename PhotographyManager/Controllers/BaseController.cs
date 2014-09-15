using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotographyManager.DataAccess.UnitOfWork;
using PhotographyManager.Filters;
using PhotographyManager.Model;

namespace PhotographyManager.Controllers
{
    [ExceptionHandler]
    public abstract class BaseController : Controller
    {
        protected IUnitOfWork _unitOfWork;
        protected User currentUser;

        public BaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
