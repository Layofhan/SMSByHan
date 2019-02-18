using SMS.Data.Services;
using SMS.Demo.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Controllers
{
    public class InformationMaintainController : Controller
    {
        public IInformationMaintainContract InformationMaintainContract { get; set; }
        // GET: InformationMaintain
        /// <summary>
        /// 基本资料界面
        /// </summary>
        /// <returns></returns>
        public ActionResult BaseInformation()
        {
            return View();
        }
        /// <summary>
        /// 安全设置界面
        /// </summary>
        /// <returns></returns>
        public ActionResult SecuritySetting()
        {
            return View();
        }
        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="UserId">用户ID</param>
        /// <param name="NewPassword">新密码</param>
        /// <returns></returns>
        public ActionResult ModificationPassword(string UserId, string NewPassword)
        {
            return InformationMaintainContract.ModificationPassword(UserId, NewPassword).ToMvcJson();
        }
        /// <summary>
        /// 查询用户基础信息
        /// </summary>
        /// <param name="UserId">用户ID</param>
        /// <returns></returns>
        public ActionResult SearchBaseInfor(string UserId)
        {
            return InformationMaintainContract.SearchBaseInfor(UserId).ToMvcJson();
        }
    }
}