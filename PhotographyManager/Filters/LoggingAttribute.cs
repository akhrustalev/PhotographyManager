using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotographyManager.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class LoggingAttribute :FilterAttribute, IExceptionFilter
    {
        //private log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private log4net.ILog log = log4net.LogManager.GetLogger("MyLogger");
        public void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            log.Error(filterContext.Exception.Message);
            filterContext.Result = new ViewResult { ViewName = "ErrorView" };  
        }

    }
}