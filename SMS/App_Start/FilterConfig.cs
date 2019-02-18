using SMS.Interceptor;
using System.Web;
using System.Web.Mvc;

namespace SMS
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new MyAuthorizeAttribute());
            //filters.Add(new AuthorizationFilter());
            //filters.Add(DependencyResolver.Current.GetService<AuthorizationFilter>());
        }
    }
}
