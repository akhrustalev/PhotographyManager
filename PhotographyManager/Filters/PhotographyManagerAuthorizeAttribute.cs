using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotographyManager.Web.Security;
using PhotographyManager.Model;
using PhotographyManager.DataAccess.UnitOfWork;

namespace PhotographyManager.Web.Filters
{
    public class PhotographyManagerAuthorizeAttribute : AuthorizeAttribute
    {
        private string _roles;
        private string[] _rolesSplit = new string[0];
        public string Roles
        {
            get
            {
                return _roles;
            }
            set
            {
                _roles = value;
                _rolesSplit = SplitString(value);
            }
        }
        public bool AuthorizeCore(AuthorizationContext filterContext)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                User user = unitOfWork.Users.GetOne(u => u.Name.Equals(HttpContext.Current.User.Identity.Name));
                if (_rolesSplit.Length == 0) return true;
                for (int i=0; i<_rolesSplit.Length;i++)
                   {
                       if (PhotographyManager.Web.Security.WebSecurity.IsUserInRole(user.Name,_rolesSplit[i])) return true;
                   }
                return false;
            }
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (AuthorizeCore(filterContext))
            {
                return;
            }
            else filterContext.Result =new HttpUnauthorizedResult();
        }
        internal static string[] SplitString(string original) 
        { 
             if (String.IsNullOrEmpty(original)) 
             { 
                 return new string[0]; 
             } 

 
             var split = from piece in original.Split(',') 
                         let trimmed = piece.Trim() 
                         where !String.IsNullOrEmpty(trimmed) 
                         select trimmed; 
            return split.ToArray(); 
         } 

    }
}