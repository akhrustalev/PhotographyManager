using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotographyManager.Web.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ExceptionHandlerAttribute :FilterAttribute, IExceptionFilter
    {
        private log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            log.Error(filterContext.Exception.Message,filterContext.Exception);
            ViewResult result = new ViewResult { ViewName = "ErrorView" };
            result.ViewBag.Error = filterContext.Exception.Message;
            filterContext.Result = result;  
        }
    }
}