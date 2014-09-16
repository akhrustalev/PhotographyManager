using System.Web;
using System.Web.Mvc;
using PhotographyManager.Web.Filters;

namespace PhotographyManager.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new ExceptionHandlerAttribute());

        }
    }
}