using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotographyManager.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class LoggingAttribute: FilterAttribute,IExceptionFilter
    {
        private log4net.ILog log;
        public  void OnException(ExceptionContext filterContext)
        {
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Error(filterContext);
          
        }
    }
}