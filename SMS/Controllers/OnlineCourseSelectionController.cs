using SMS.Data.Services;
using SMS.Demo.Common;
using SMS.Demo.Contracts;
using SMS.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Controllers
{
    public class OnlineCourseSelectionController : Controller
    {
        public IOnlineCourseSelectionContract OnlineCourseSelectionContract { get; set; }
        //返回选课通用视图模板
        // GET: OnlineCourseSelection
        public ActionResult Index(string Id)
        {
            ViewBag.Id = Id;
            return View();
        }
        //获取选课信息
        //Id为选课类型
        IdentityTicket ident;
        public ActionResult GetChoiceCourseInformation(int Id)
        {
            ident = IdentityManager.GetIdentFromAll();
            return OnlineCourseSelectionContract.GetChoiceCourseInformation(ident.UserId,Id).ToMvcJson("yyyy-MM-dd HH:mm:ss");
        }
        //将学生选课的信息插入到数据库中
        //Id为选课类型
        public ActionResult InsertChooseCourseToTable(int Id,string courseid,string gradeid)
        {
            if(ident == null)
            {
                ident = IdentityManager.GetIdentFromAll();
            }
            SMSCourseStudentMap csm = new SMSCourseStudentMap();
            csm.CourseId = courseid;
            csm.GradeId = gradeid;
            csm.StudentId = ident.UserId;
            csm.MidEvaluation = 0;
            csm.FinalEvaluation = 0;
            return OnlineCourseSelectionContract.InsertChooseCourseToTable(Id,csm).ToMvcJson();
        }
        //获取学生已选课的信息
        public ActionResult GetChoosedInformation(int Id)
        {
            if (ident == null)
            {
                ident = IdentityManager.GetIdentFromAll();
            }
            return OnlineCourseSelectionContract.GetChoosedInformation(Id,ident.UserId).ToMvcJson("yyyy-MM-dd HH:mm:ss");
        }
    }
}