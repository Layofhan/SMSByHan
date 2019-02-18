using SMS.Data.Services;
using SMS.Demo.Contracts;
using SMS.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Controllers
{
    /// <summary>
    /// 管理员功能中的课程管理
    /// </summary>
    public class ResourceManagementController : Controller
    {
        public IResourceManagementContract ResourceManagementContract { get; set; }
        // GET: ResourceManagement
        public ActionResult Index()
        {
            return View();
        }
        //返回创建课程视图
        public ActionResult CreateCourse()
        {
            return View();
        }
        //返回教学安排视图
        public ActionResult ArrangeCurriculum()
        {
            return View();
        }
        //返回课程安排视图
        public ActionResult ArrangeCourse()
        {
            return View();
        }
        //将课程信息插入数据库
        public ActionResult InsertCourseToTable(string id, string name, string natureid, string branchid, bool enable, string credit, string index, string remark)
        {
            SMSCourse course = new SMSCourse();
            course.Id = id;
            course.Name = name;
            course.NaturesId = natureid;
            course.BranchId = branchid;
            course.Enabled = enable;
            course.Credit = credit;
            course.Indexs = index;
            course.Remark = remark;
            return ResourceManagementContract.InsertCourseToTable(course).ToMvcJson();
        }
        //获取课程信息
        public ActionResult GetCourseInfrormation()
        {
            return ResourceManagementContract.GetCourseInfrormation().ToMvcJson();
        }
        //获取教师信息
        public ActionResult GetTeacherInformation()
        {
            return ResourceManagementContract.GetTeacherInformation().ToMvcJson();
        }
        //获取专业信息
        public ActionResult GetProgressInformation()
        {
            return ResourceManagementContract.GetProgressInformation().ToMvcJson();
        }
        //获取年级信息
        public ActionResult GetGradeInformation()
        {
            return ResourceManagementContract.GetGradeInformation().ToMvcJson();
        }
        //将课程安排信息插入数据库
        public ActionResult InsertCurriculumToTable(SMSTeacherCourseMap teachercourse)
        {
            return ResourceManagementContract.InsertCurriculumToTable(teachercourse).ToMvcJson();
        }
        //将安排的课程插入学生与课程关系对应表中
        public ActionResult InsertStudentCourseToTable(string progressid,string courseid,string gradeid,string remark)
        {
            return ResourceManagementContract.InsertStudentCourseToTable(progressid, courseid, gradeid, remark).ToMvcJson();
        }
    }
}