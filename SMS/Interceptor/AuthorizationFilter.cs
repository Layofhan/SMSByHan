using SMS.Data.Interface;
using SMS.Demo.Common;
using SMS.Demo.Contracts;
using SMS.Demo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Interceptor
{
    //[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class AuthorizationFilter : FilterAttribute, IAuthorizationFilter
    {
        public string ActionName { get; set; }

        public void OnAuthorization(AuthorizationContext filterContext)
        {

            IdentityTicket ticket = IdentityManager.GetIdentFromSession();
            if (ticket == null)
                ticket = IdentityManager.GetIdentFromCookie();
            if (ticket != null)
            {
                foreach(var au in ticket.Authoritys)
                {
                    if (ActionName.Equals(au))
                    {
                        //filterContext.Result = new RedirectResult("/Home/Index");
                        return;
                    }
                }
                
                //跳转操作--跳转到 无权限界面
               filterContext.Result=new  RedirectResult("/Error/NoAuthority");
            }
            else
            {
                //跳转到 登入界面
                filterContext.Result = new RedirectResult("/Login/Index");
            }

            //if (!filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
            //     &&
            //     !filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
            //    )

            //{

            //}
            //else
            //{
            //    return;
            //}

        }

    }
}