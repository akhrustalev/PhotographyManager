using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Web.Mvc;
using WebMatrix.WebData;
using PhotographyManager.Models;
using PhotographyManager.Model;

namespace PhotographyManager.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Ensure ASP.NET Simple Membership is initialized only once per app start
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }

        private class SimpleMembershipInitializer
        {
            public SimpleMembershipInitializer()
            {
                //Database.SetInitializer<UsersContext>(null);
                Database.SetInitializer<PhotographyManagerContext>(null);

                try
                {
                    //using (var context = new UsersContext())
                    using (var context = new PhotographyManagerContext())
                    {
                        if (!context.Database.Exists())
                        {
                            // Create the SimpleMembership database without Entity Framework migration schema
                            ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                        }
                    }

                    //WebSecurity.InitializeDatabaseConnection("PhotographyManagerContext", "UserProfile", "UserId", "UserName", autoCreateTables: true);
                    WebSecurity.InitializeDatabaseConnection("PhotographyManagerContext", "User", "ID", "Name", autoCreateTables: true);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized.", ex);
                }
            }
        }
    }
}
