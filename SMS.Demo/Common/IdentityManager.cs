using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SMS.Demo.Common
{
    public  class IdentityManager
    {

        public static IdentityTicket Identiket;
        //将登入信息保存到Cookie中
        //public static  void Init(IdentityTicket ticket)
        //{
        //    HttpCookie aCookie = new HttpCookie("UserName");
        //    aCookie.Value = ticket.UserName;
        //    aCookie.Expires = DateTime.Now.AddDays(1);
        //    //将用户信息存入cookie中--没有使用，还没找到需要使用的理由
        //    //HttpContext.Current.Response.Cookies.Add(aCookie);
        //    Identiket = new IdentityTicket();
        //    Identiket.IsForceOffline = true;
        //    Identiket.UserName = ticket.UserName;
        //    Identiket.Role = ticket.Role;
        //    Identiket.Authoritys = ticket.Authoritys;
        //}
        //将登入用户信息存入session和cookie
        public static void Init(IdentityTicket ticket,bool isChace)
        {
            if (isChace)
            {
                CreatCookie(ticket);
            }
            CreatSession(ticket);
        }
        /// <summary>
        /// 将用户信息存入session
        /// </summary>
        /// <param name="ticket">用户信息</param>
        public static void CreatSession(IdentityTicket ticket)
        {
            HttpContext.Current.Session["IdentSession"] = ticket;
        }
        /// <summary>
        /// 将用户信息存入cookie
        /// </summary>
        /// <param name="ticket">用户信息</param>
        public static void CreatCookie(IdentityTicket ticket)
        {
            HttpCookie aCookie = new HttpCookie("IdentCookie");
            aCookie.Value = Newtonsoft.Json.JsonConvert.SerializeObject(ticket);
            HttpContext.Current.Response.Cookies.Add(aCookie);
        }
        /// <summary>
        /// 从session中湖区用户信息
        /// </summary>
        /// <returns></returns>
        public static IdentityTicket GetIdentFromSession()
        {
            return (IdentityTicket)HttpContext.Current.Session["IdentSession"];
        }
        /// <summary>
        /// 从session中获取用户信息
        /// </summary>
        /// <returns></returns>
        public static IdentityTicket GetIdentFromCookie()
        {
            try
            {
                //return Newtonsoft.Json.JsonConvert.DeserializeObject<IdentityTicket>(HttpContext.Current.Request.Cookies["IdentCookie"].ToString());
                string a = HttpContext.Current.Request.Cookies["IdentCookie"].Value.ToString();
                return  Newtonsoft.Json.JsonConvert.DeserializeObject<IdentityTicket>(a);
            }
            catch(Exception ex)
            {
                return null;
            }
            
        }
        /// <summary>
        /// 先从session中获取用户信息，为空则从cookie中获取
        /// </summary>
        /// <returns></returns>
        public static IdentityTicket GetIdentFromAll()
        {
            IdentityTicket identity = new IdentityTicket();
            try
            {
                identity = (IdentityTicket)HttpContext.Current.Session["IdentSession"];
                if (identity == null)
                {
                    identity = Newtonsoft.Json.JsonConvert.DeserializeObject<IdentityTicket>(HttpContext.Current.Request.Cookies["IdentCookie"].Value.ToString());
                    if (identity == null) return null;
                    else return identity;
                }
                else
                    return identity;
            }
            catch(Exception es)
            {
                return null;
            }
        }
    }
}
