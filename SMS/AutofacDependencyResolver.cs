using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Autofac;
using System.Linq;

namespace SMS
{
    internal class AutofacDependencyResolver : IDependencyResolver
    {
        private IContainer container;

        public AutofacDependencyResolver(IContainer container)
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