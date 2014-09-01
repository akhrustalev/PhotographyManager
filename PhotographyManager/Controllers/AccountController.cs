using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using WebMatrix.WebData;
using PhotographyManager.Filters;
using PhotographyManager.Models;
using PhotographyManager.DataAccess.UnitOfWork;
using PhotographyManager.Model;

namespace PhotographyManager.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Logging]
    public class AccountController : Controller
    {

        #region Constructor

        private IUnitOfWork _unitOfWork;

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
                return RedirectToAction("MyHomePage", "Home", new { Id = _unitOfWork.GetUsers().GetByName(user =>user.Name.Equals(model.UserName)).ID });
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
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _unitOfWork.GetUsers().GetByName(us=>us.Name.Equals(model.UserName));
                    if (user != null)
                        throw new MembershipCreateUserException("User name already exists. Please enter a different user name.");
                    if (Request.Form["submitBtn"] == "Register As Free User")
                        user = new FreeUser { Name = model.UserName };
                    else user = new PaidUser { Name=model.UserName};
                    _unitOfWork.GetUsers().Add(user);
                    _unitOfWork.Commit();
                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
                    WebSecurity.Login(model.UserName, model.Password);
                    return RedirectToAction("MyHomePage", "Home", new { Id = user.ID });
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", "User name already exists. Please enter a different user name.");
                }
            }
            return View(model);
        }
    }
}
