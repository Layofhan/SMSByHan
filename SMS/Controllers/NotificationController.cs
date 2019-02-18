using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SMS.Data.Services;
using SMS.Demo.Common;
using SMS.Demo.Contracts;
using SMS.Entity;
using SMS.Interceptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Controllers
{
    public class NotificationController : Controller
    {
        public IMessageNotificationContract MessageNotificationContract { get; set; }
        // GET: Notification
        //返回消息通知页面
        [AuthorizationFilter(ActionName = "Notice")]
        public ActionResult Index()
        {

            return View();
        }
        //返回消息详细界面
        [AuthorizationFilter(ActionName = "Notice")]
        public ActionResult NoticeDetailedInterface(string id)
        {
            ViewBag.Message = id;
            return View();
        }
        //返回历史发送消息界面
        [AuthorizationFilter(ActionName = "Notice")]
        public ActionResult HasSendNoticeInterface()
        {
            return View();
        }
        //返回历史发送消息详情界面
        [AuthorizationFilter(ActionName = "Notice")]
        public ActionResult HasSendNoticeDetailedInterface(string id)
        {
            ViewBag.Message = id;
            return View();
        }

        //查询当前用户历史发送消息
        [AuthorizationFilter(ActionName = "Notice")]
        public ActionResult HasSendNotices()
        {
            var userid = IdentityManager.GetIdentFromAll().UserId;
            if(userid != null)
            {
                return MessageNotificationContract.SearchHasSendNotices(userid).ToMvcJson();
            }
            return DataProcess.Failure("Login information was not found").ToMvcJson();
        }
        //根据页面和每页显示的条数来查询消息
        [AuthorizationFilter(ActionName = "Notice")]
        public ActionResult SearchNotice(int page = 0, int limit = 0)
        {   
            return MessageNotificationContract.SearchNotice(page, limit).ToMvcJson("yyyy - MM - dd HH: mm:ss");
        }
        //根据消息ID来查询消息内容
        [AuthorizationFilter(ActionName = "Notice")]
        public ActionResult SearchNoticeDetailed(string id)
        {
            return MessageNotificationContract.SearchNoticeDetailed(id).ToMvcJson("yyyy - MM - dd HH: mm:ss");
        }
        //根据消息ID来查询历史消息内容 -- 教师角色在查看历史消息中使用该功能
        [AuthorizationFilter(ActionName = "Notice")]
        public ActionResult SearchHistoryNoticeDetailed(string id)
        {
            return MessageNotificationContract.SearchHasSendNoticesDetailed(id).ToMvcJson("yyyy - MM - dd HH: mm:ss");
        }
        //是否存在未读消息 true 代表存在 ，false 代表不存在
        [AuthorizationFilter(ActionName = "Notice")]
        public bool IsExistUnread()
        { 
            return  MessageNotificationContract.IsExistUnread();
        }
        //将选择的消息设置为已读消息
        [AuthorizationFilter(ActionName = "Notice")]
        public ActionResult SetNoticeHasRead(List<string> list)
        {
            var a= MessageNotificationContract.SetNoticeHasRead(list);
            return a.ToMvcJson();
        }
        //将该用户所有的消息设置为已读消息
        [AuthorizationFilter(ActionName = "Notice")]
        public ActionResult SetAllNoticeHasRead()
        {
            var userid = IdentityManager.GetIdentFromAll().UserId;
            if(userid != null)
            {
                return MessageNotificationContract.SetAllNoticeHasRead(userid).ToMvcJson();
            }
            return DataProcess.Failure("Login information was not found").ToMvcJson();
        }
        //删除所选消息
        [AuthorizationFilter(ActionName = "Notice")]
        public ActionResult DeleteNotice(List<string> list)
        {
            return MessageNotificationContract.DeleteNotice(list).ToMvcJson();
        }


        //返回消息发送界面
        public ActionResult SendNoticeMessage()
        {
            return View();
        }
        //发送通知消息
        public ActionResult SendNotice(string title,string content,string courseid)
        {
            IdentityTicket iden = IdentityManager.GetIdentFromAll();
            return MessageNotificationContract.SendNoticeMessage(title,content,courseid,iden.UserId).ToMvcJson();
        }
        //获取教师对应的课程
        public ActionResult GetCourseOfTeacher()
        {
            IdentityTicket iden = IdentityManager.GetIdentFromAll();
            return MessageNotificationContract.GetCourseOfTeacher(iden.UserId).ToMvcJson();
        }
    }
}