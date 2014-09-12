using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using PhotographyManager.Filters;
using PhotographyManager.DataAccess.UnitOfWork;
using PhotographyManager.Model;
using PhotographyManager.Security;

namespace PhotographyManager.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {

        #region Constructor

        public AccountController(IUnitOfWork unitOfWork):base(unitOfWork)
        {
        }

        #endregion

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            //if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            //{
            //    return RedirectToAction("MyHomePage", "Home");
            //}

            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return RedirectToAction("Index","Home");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
          //  WebSecurity.Logout();

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
               var user = _unitOfWork.Users.GetOne(us => us.Name.Equals(model.UserName));
               if (user != null)
                  {
                      ModelState.AddModelError("", "User name already exists. Please enter another user name.");
                      return View("Register");
                  }
                   user = new FreeUser { Name = model.UserName };
                  _unitOfWork.Users.Add(user);
                  _unitOfWork.Commit();
                  //WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
                  //WebSecurity.Login(model.UserName, model.Password);
                  return RedirectToAction("MyHomePage", "Home", new { Id = user.ID });
            }
            return View("Register",model);
        }
    }
}

