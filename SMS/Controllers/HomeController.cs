using SMS.Data.Services;
using SMS.Demo.Common;
using SMS.Demo.Contracts;
using SMS.Entity;
using SMS.Interceptor;
using SMS.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Controllers
{
    public class HomeController : Controller
    {
        public IMenuContract MenuContract { get; set; }
        public IHomeContract HomeContract { get; set; }
        // GET: Home
        //[AuthorizationFilter(ActionName = "Index")]
        [AuthorizationFilter(ActionName ="Index")]
        public ActionResult Index()
        {
            return View();
        }
        [AuthorizationFilter(ActionName = "Index")]
        public ActionResult Two()
        {
            return View();
        }
        [AuthorizationFilter(ActionName = "Index")]
        public ActionResult LoadingMenu()
        {
            return MenuContract.LoadingMenu().ToMvcJson();
        }
        [AuthorizationFilter(ActionName = "Index")]
        public ActionResult GetUserMessage()
        {
            try
            {
                var userid = IdentityManager.GetIdentFromAll().UserId;
                if(userid != null)
                {
                    return HomeContract.GetUserMessage(userid).ToMvcJson();
                }
                return DataProcess.Failure("Login information was not found").ToMvcJson();
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message).ToMvcJson();
            }
        }
        [AuthorizationFilter(ActionName = "Index")]
        //返回选项卡中的首页界面
        public ActionResult Home()
        {
            return View();
        }
        /// <summary>
        /// 获取当前吐槽语言
        /// </summary>
        /// <returns></returns>
        [AuthorizationFilter(ActionName = "Index")]
        public ActionResult GetRoast()
        {
            return null;
        }
        /// <summary>
        /// 添加吐槽语言
        /// </summary>
        /// <returns></returns>
        [AuthorizationFilter(ActionName = "Index")]
        public ActionResult SetRoast(string Content)
        {
            return null;
        }
        /// <summary>
        /// 获取当前在线人数
        /// </summary>
        /// <returns></returns>
        [AuthorizationFilter(ActionName = "Index")]
        public int GetCurrentNums()
        {
            return WSChatController.Sockets.Count;
        }
    }
}