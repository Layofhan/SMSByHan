using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Web.Two.Interceptor
{
    public class AutofacDependencyResolvers: IDependencyResolver
    {
        private IContainer container;

        public AutofacDependencyResolvers(IContainer container)
        {
            this.container = container;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                if (!container.IsRegistered(serviceType))
                {
                    return null;
                }
                return container.Resolve(serviceType);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Enumerable.Empty<object>();
        }
    }
}