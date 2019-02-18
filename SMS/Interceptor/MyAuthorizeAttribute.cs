using SMS.Demo.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Interceptor
{
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        public string ActionName { get; set; }
        //public new string Roles { get; set; }
        public ITestContract TestContract { get; set; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var a = Roles;
            //return base.AuthorizeCore(httpContext);
            return DateTime.Now.Minute % 2 == 0;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string a = Roles;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.HttpContext.Response.Redirect("/Customer/Login");

            //base.HandleUnauthorizedRequest(filterContext);
        }
    }
}