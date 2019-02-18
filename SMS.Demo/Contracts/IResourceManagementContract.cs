using SMS.Data.Interface;
using SMS.Entity.Models;
using SMS.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Demo.Contracts
{
    public interface IResourceManagementContract : IScopeDependency, IDependency
    {
        //将课程信息插入课程信息表中
        DataResult InsertCourseToTable(SMSCourse course);
        //获取课程信息
        DataResult GetCourseInfrormation();
        //获取教师信息
        DataResult GetTeacherInformation();
        //获取专业信息
        DataResult GetProgressInformation();
        //获取年级信息
        DataResult GetGradeInformation();
        //将课程安排信息插入到数据库
        DataResult InsertCurriculumToTable(SMSTeacherCourseMap teachercourse);
        //将安排的课程插入学生与课程关系对应表中
        DataResult InsertStudentCourseToTable(string progressid, string courseid, string gradeid, string remark);
    }
}
