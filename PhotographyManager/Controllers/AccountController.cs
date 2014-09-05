using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;
using PhotographyManager.Filters;
using PhotographyManager.DataAccess.UnitOfWork;
using PhotographyManager.Model;

namespace PhotographyManager.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class AccountController : BaseController
    {

        #region Constructor

        public AccountController(IUnitOfWork uoW)
        {
            _unitOfWork = uoW;
        }

        #endregion

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            {
                return RedirectToAction("MyHomePage", "Home", new { Id = _unitOfWork.Users.GetByName(user =>user.Name.Equals(model.UserName)).ID });
            }

            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();

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
        public ActionResult RegisterFreeUser(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
               var user = _unitOfWork.Users.GetByName(us=>us.Name.Equals(model.UserName));
               if (user != null)
                  {
                      ModelState.AddModelError("", "User name already exists. Please enter a different user name.");
                      return View("Register");
                  }
                   user = new FreeUser { Name = model.UserName };
                  _unitOfWork.Users.Add(user);
                  _unitOfWork.Commit();
                  WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
                  WebSecurity.Login(model.UserName, model.Password);
                  return RedirectToAction("MyHomePage", "Home", new { Id = user.ID });
            }
            return View("Register",model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterPaidUser(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _unitOfWork.Users.GetByName(us => us.Name.Equals(model.UserName));
                if (user != null)
                {
                    ModelState.AddModelError("", "User name already exists. Please enter a different user name.");
                    return View(model);
                }
                user = new PaidUser { Name = model.UserName };
                _unitOfWork.Users.Add(user);
                _unitOfWork.Commit();
                WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
                WebSecurity.Login(model.UserName, model.Password);
                return RedirectToAction("MyHomePage", "Home", new { Id = user.ID });
            }
            return View("Register",model);
        }


    }
}

