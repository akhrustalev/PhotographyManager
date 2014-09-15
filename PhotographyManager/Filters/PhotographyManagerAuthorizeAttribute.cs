using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotographyManager.Security;

namespace PhotographyManager.Filters
{
    public class PhotographyManagerAuthorizeAttribute: AuthorizeAttribute
    {
        public bool AuthorizeCore(AuthorizationContext filterContext)
        {
            if (PhotographyManagerRoleProvider.IsUserInRole()) return false;
            else return true;
        }
    }
}