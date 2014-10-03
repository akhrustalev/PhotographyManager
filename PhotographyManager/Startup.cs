using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(PhotographyManager.Startup))]

namespace PhotographyManager
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           // app.MapSignalR();

        }
    }
}
