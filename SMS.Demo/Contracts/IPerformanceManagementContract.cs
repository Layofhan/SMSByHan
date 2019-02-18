using SMS.Data.Interface;
using SMS.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Demo.Contracts
{
    public interface IPerformanceManagementContract : IScopeDependency, IDependency
    {
        /// <summary>
        /// 获取学生的课程信息--用于成绩教师成绩录入
        /// </summary>
        /// <param name="CourseId"></param>
        /// <param name="ClassId"></param>
        /// <returns></returns>
        DataResult GetStudentGrade(string CourseId, string ClassId, int limit, int page);
        /// <summary>
        /// 更新成绩
        /// </summary>
        /// <param name="Grade">成绩</param>
        /// <returns></returns>
        DataResult UpdateScore(int Grade, string Creditx, string Indexs, string CourseId, string StudentId);
        /// <summary>
        /// 更新绩点
        /// </summary>
        /// <param name="Indexs">绩点</param>
        /// <param name="CourseId">课程ID</param>
        /// <param name="StudentId">学生ID</param>
        /// <returns></returns>
        DataResult UpdateIndexs(string Indexs, string CourseId, string StudentId);
        /// <summary>
        /// 更新学分
        /// </summary>
        /// <param name="Credit">学分</param>
        /// <param name="CourseId">课程ID</param>
        /// <param name="StudentId">学生ID</param>
        /// <returns></returns>
        DataResult UpdateCredit(string Credit, string CourseId, string StudentId);
        /// <summary>
        /// 更新补考成绩
        /// </summary>
        /// <param name="MakeupGrade">补考成绩</param>
        /// <param name="CourseId">课程ID</param>
        /// <param name="StudentId">学生ID</param>
        /// <returns></returns>
        DataResult UpdateMakeupGrade(int MakeupGrade, string CourseId, string StudentId);
        /// <summary>
        /// 更新备注
        /// </summary>
        /// <param name="Remark">备注</param>
        /// <param name="CourseId">课程ID</param>
        /// <param name="StudentId">学生ID</param>
        /// <returns></returns>
        DataResult UpdateRemark(string Remark, string CourseId, string StudentId);
    }
}
