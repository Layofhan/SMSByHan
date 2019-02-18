using SMS.Demo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Web.Two.Controllers
{
    public class TestCheck : FilterAttribute, IAuthorizationFilter
    {
        public string ActionName { get; set; }
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                 &&
                 !filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                )

            {
                filterContext.HttpContext.Response.Write(ActionName + "--");
                IdentityTicket ticket = IdentityManager.Identiket;
            }
            else
            {
                return;
            }
        }
    }
}