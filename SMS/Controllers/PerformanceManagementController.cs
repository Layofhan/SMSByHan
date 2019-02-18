using SMS.Data.Services;
using SMS.Demo.Contracts;
using SMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Controllers
{
    public class PerformanceManagementController : Controller
    {
        public IPerformanceManagementContract PerformanceManagementContract { get; set; }
        // GET: PerformanceManagement
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取学生的课程信息--用于成绩教师成绩录入
        /// </summary>
        /// <param name="CourseId"></param>
        /// <param name="ClassId"></param>
        /// <returns></returns>
        public ActionResult GetStudentGrade(string CourseId,string ClassId,int limit,int page)
        {
            try
            {
                return PerformanceManagementContract.GetStudentGrade(CourseId,ClassId,limit,page).ToMvcJson();
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message).ToMvcJson();
            }
        }
        /// <summary>
        /// 更新学生成绩
        /// </summary>
        /// <param name="Grade">成绩</param>
        /// <returns></returns>
        public ActionResult UpdateScore(int Grade,string Creditx,string Indexs,string CourseId,string StudentId)
        {
            try
            {
                return PerformanceManagementContract.UpdateScore(Grade, Creditx, Indexs, CourseId, StudentId).ToMvcJson();
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message).ToMvcJson();
            }
        }
        /// <summary>
        /// 更新绩点
        /// </summary>
        /// <param name="Indexs">绩点</param>
        /// <param name="CourseId">课程ID</param>
        /// <param name="StudentId">学生ID</param>
        /// <returns></returns>
        public ActionResult UpdateIndexs(string Indexs, string CourseId, string StudentId)
        {
            try
            {
                return PerformanceManagementContract.UpdateIndexs(Indexs, CourseId, StudentId).ToMvcJson();
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message).ToMvcJson();
            }
        }
        /// <summary>
        /// 更新学分
        /// </summary>
        /// <param name="Credit">学分</param>
        /// <param name="CourseId">课程ID</param>
        /// <param name="StudentId">学生ID</param>
        /// <returns></returns>
        public ActionResult UpdateCredit(string Credit, string CourseId, string StudentId)
        {
            try
            {
                return PerformanceManagementContract.UpdateCredit(Credit, CourseId, StudentId).ToMvcJson();
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message).ToMvcJson();
            }
        }
        /// <summary>
        /// 更新补考成绩
        /// </summary>
        /// <param name="MakeupGrade">补考成绩</param>
        /// <param name="CourseId">课程ID</param>
        /// <param name="StudentId">学生ID</param>
        /// <returns></returns>
        public ActionResult UpdateMakeupGrade(int MakeupGrade,string CourseId,string StudentId)
        {
            try
            {
                return PerformanceManagementContract.UpdateMakeupGrade(MakeupGrade,CourseId,StudentId).ToMvcJson();
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message).ToMvcJson();
            }
        }
        /// <summary>
        /// 更新备注
        /// </summary>
        /// <param name="Remark">备注</param>
        /// <param name="CourseId">课程ID</param>
        /// <param name="StudentId">学生ID</param>
        /// <returns></returns>
        public ActionResult UpdateRemark(string Remark,string CourseId,string StudentId)
        {
            try
            {
                return PerformanceManagementContract.UpdateRemark(Remark,CourseId,StudentId).ToMvcJson();
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message).ToMvcJson();
            }
        }
    }
}