using SMS.Data.Services;
using SMS.Demo.Common;
using SMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Controllers
{
    public class InformationController : Controller
    {
        /// <summary>
        /// 课表的背景颜色
        /// </summary>
        public static string[] BgColor = new string[] { "#FFCCCC", "#FFCC99", "#CCFFFF", "#CCFF99", "#FFFFCC", "#FFFF99" , "#CCCCFF", "#FFFF66" };
        // GET: Information
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult StudentTimetable()
        {
            return View();
        }
        public ActionResult GetTimetable()
        {
            //IdentityTicket ident = IdentityManager.GetIdentFromAll();
            DayCourse[] daycourses = new DayCourse[7];
            DayCourse daycourse = new DayCourse();
            DayCourse daycourse2 = new DayCourse();
            Random radom = new Random();


            Course course = new Course();
            course.Nums = 2;
            course.Name = "数据结构<br/>5号楼461<br/>" + System.DateTime.Now;
            course.Time = System.DateTime.Now;
            course.Location = "5号楼461";
            course.StartTime = 1;
            course.EndTime = course.Nums + course.StartTime - 1;
            course.State = false;
            course.Color = BgColor[radom.Next(0,8)];
            course.CourseId = "1";

            Course course1 = new Course();
            course1.Nums = 2;
            course1.Name = "大学英语<br/>5号楼461<br/>" + System.DateTime.Now;
            course1.Time = System.DateTime.Now;
            course1.Location = "5号楼461";
            course1.StartTime = 7;
            course1.EndTime = course1.Nums + course1.StartTime - 1;
            course1.State = false;
            course1.Color = BgColor[radom.Next(0, 8)];
            course1.CourseId = "2";

            Course course2 = new Course();
            course2.Nums = 1;
            course2.Name = "大学英语2<br/>5号楼461<br/>" + System.DateTime.Now;
            course2.Time = System.DateTime.Now;
            course2.Location = "5号楼461";
            course2.StartTime = 10;
            course2.EndTime = course2.Nums + course2.StartTime - 1;
            course2.State = false;
            course2.Color = BgColor[radom.Next(0, 8)];
            course2.CourseId = "3";

            Course course3 = new Course();
            course3.Nums = 2;
            course3.Name = "测试课程<br/>5号楼461<br/>" + System.DateTime.Now;
            course3.Time = System.DateTime.Now;
            course3.Location = "5号楼461";
            course3.StartTime = 3;
            course3.EndTime = course3.Nums + course3.StartTime - 1;
            course3.State = false;
            course3.Color = BgColor[radom.Next(0, 8)];
            course3.CourseId = "4";

            Course course4 = new Course();
            course4.Nums = 2;
            course4.Name = "测试课程<br/>5号楼461<br/>" + System.DateTime.Now;
            course4.Time = System.DateTime.Now;
            course4.Location = "5号楼461";
            course4.StartTime = 5;
            course4.EndTime = course4.Nums + course4.StartTime - 1;
            course4.State = false;
            course4.Color = BgColor[radom.Next(0, 8)];
            course4.CourseId = "5";

            daycourse.Courses.Add(course);
            daycourse.Courses.Add(course1);
            daycourse.Courses.Add(course2);

            daycourse2.Courses.Add(course3);
            daycourse2.Courses.Add(course4);

            daycourses[0] = daycourse;
            daycourses[1] = daycourse2;
            daycourses[2] = daycourse;
            daycourses[3] = daycourse2;
            daycourses[4] = daycourse;
            daycourses[5] = daycourse2;
            daycourses[6] = daycourse;

            return DataProcess.Success(daycourses).ToMvcJson("yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// 学生成绩查询视图
        /// </summary>
        /// <returns></returns>
        public ActionResult ResultInquiry()
        {
            return View();
        }
        /// <summary>
        /// 学生考试查询视图
        /// </summary>
        /// <returns></returns>
        public ActionResult ExaminationInformation()
        {
            return View();
        }
        /// <summary>
        /// 专业课程培养计划视图
        /// </summary>
        /// <returns></returns>
        public ActionResult ProfessionalCurriculumTrainingPlan()
        {
            return View();
        }
        /// <summary>
        /// 转专业查询视图
        /// </summary>
        /// <returns></returns>
        public ActionResult ProfessionalEnquiry()
        {
            return View();
        }
        /// <summary>
        /// 教学进度查询视图
        /// </summary>
        /// <returns></returns>
        public ActionResult TeachingProgressQuery()
        {
            return View();
        }
    }
    #region 学生课表模块中使用的额外类
    /// <summary>
    /// 一天包含的课程
    /// </summary>
    public class DayCourse
    {
        public List<Course> Courses = new List<Course>();
    }
    /// <summary>
    /// 课程内容
    /// </summary>
    public class Course
    {
        /// <summary>
        /// 课程节数
        /// </summary>
        public int Nums { get; set; }
        /// <summary>
        /// 课程名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 上课时间
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 上课地点
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 课程开始节数
        /// </summary>
        public int StartTime { get; set; }
        /// <summary>
        /// 课程结束节数
        /// </summary>
        public int EndTime { get; set; }
        /// <summary>
        /// 遍历状态-供前端使用
        /// </summary>
        public bool State { get; set; }
        /// <summary>
        /// 课程显示颜色
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// 课程ID
        /// </summary>
        public string CourseId { get; set; }
    }
    #endregion
}