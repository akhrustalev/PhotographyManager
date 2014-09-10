using System.Web;
using System.Web.Mvc;
using PhotographyManager.Filters;

namespace PhotographyManager
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