using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Configuration;
using Ninject;
using Ninject.Parameters;
using Ninject.Syntax;
using PhotographyManager.DataAccess.UnitOfWork;

namespace PhotographyManager.DataAccess.Infrastructure
{
    public class NinjectDependencyResolver: IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        public void AddBindings()
         {
             kernel.Bind<IUnitOfWork>().To<MyUnitOfWork>();

         }
    }

}
