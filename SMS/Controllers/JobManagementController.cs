using SMS.Data.Services;
using SMS.Demo.Common;
using SMS.Demo.Contracts;
using SMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Controllers
{
    public class JobManagementController : Controller
    {
        public IJobinfoAndManageContract JobinfoAndManageContract { get; set; }
        // GET: JobManagement
        public ActionResult WorkPublish()
        {
            return View();
        }

        public ActionResult WorkAssignment()
        {
            return View();
        }
        /// <summary>
        /// 获取当前教师的课程和对应班级信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCourseInformation()
        {
            try
            {
                IdentityTicket ident = IdentityManager.GetIdentFromAll();
                return JobinfoAndManageContract.GetCourseInformation(ident.UserId).ToMvcJson();
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message).ToMvcJson();
            }
        }
        /// <summary>
        /// 获取当前课程对应的班级名称
        /// </summary>
        /// <param name="CourseId">课程ID</param>
        /// <returns></returns>
        public ActionResult GetClassInformation(string CourseId)
        {
            try
            {
                IdentityTicket ident = IdentityManager.GetIdentFromAll();
                return JobinfoAndManageContract.GetClassInformation(ident.UserId, CourseId).ToMvcJson();
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message).ToMvcJson();
            }
        }
        /// <summary>
        /// 获取教师作业批改信息
        /// </summary>
        /// <param name="CouserId">课程ID</param>
        /// <param name="ClassId">班级ID</param>
        /// <returns></returns>
        public ActionResult GetWorkForTeachCheck(string CouserId, string ClassId)
        {
            try
            {
                return JobinfoAndManageContract.GetWorkForTeachCheck(CouserId, ClassId).ToMvcJson("yyyy-MM-dd HH:mm:ss");
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message).ToMvcJson();
            }
        }
        /// <summary>
        /// 更新成绩
        /// </summary>
        /// <param name="WorkId">作业唯一标识</param>
        /// <param name="CourseId">课程ID</param>
        /// <param name="Score">成绩</param>
        /// <param name="StudentId">学生ID</param>
        /// <returns></returns>
        public ActionResult UpdateWorkScore(string WorkId,string CourseId,int Score,string StudentId)
        {
            try
            {
                return JobinfoAndManageContract.UpdateWorkScore(WorkId, CourseId, Score, StudentId).ToMvcJson();
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message).ToMvcJson();
            }
        }
        /// <summary>
        /// 发布作业任务
        /// </summary>
        /// <param name="StartTime">开始时间</param>
        /// <param name="EndTime">结束时间</param>
        /// <param name="CourseId">课程ID</param>
        /// <param name="Require">要求</param>
        /// <param name="Content">内容</param>
        /// <returns></returns>
        public ActionResult PublishWork(DateTime StartTime,DateTime EndTime,string CourseId,string Require,string Content)
        {
            try
            {
                IdentityTicket ident = IdentityManager.GetIdentFromAll();
                return JobinfoAndManageContract.PublishWork(StartTime,EndTime,ident.UserId,CourseId,Require,Content).ToMvcJson();
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message).ToMvcJson();
            }
        }
        /// <summary>
        /// 获取老师已经发布的任务
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPublishedTask(int limit=10,int page=1)
        {
            try
            {
                IdentityTicket ident = IdentityManager.GetIdentFromAll();
                return JobinfoAndManageContract.GetPublishedTask(ident.UserId,limit,page).ToMvcJson("yyyy-MM-dd HH:mm:ss");
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message).ToMvcJson();
            }
        }
    }
}