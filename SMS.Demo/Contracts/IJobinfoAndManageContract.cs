using SMS.Data.Interface;
using SMS.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Demo.Contracts
{
    public interface IJobinfoAndManageContract : IScopeDependency, IDependency
    {
        /// <summary>
        /// 获取对应学生的作业任务
        /// </summary>
        /// <param name="Id">学生ID</param>
        /// <returns></returns>
        DataResult GetWorkTask(string Id);
        /// <summary>
        /// 保存学生提交的作业信息
        /// </summary>
        /// <param name="TaskId">任务ID</param>
        /// <param name="StudentId">学生ID</param>
        /// <returns></returns>
        DataResult SaveWorkInfor(string TaskId, string StudentId, string TeacherId, string CourseId, string FileName);
        /// <summary>
        /// 更新学生作业信息
        /// </summary>
        /// <param name="TaskId">任务ID</param>
        /// <param name="StudentId">学生ID</param>
        /// <param name="FileName">文件名</param>
        /// <returns></returns>
        DataResult UpdateWorkInfor(string TaskId, string StudentId, string FileName);
        /// <summary>
        /// 判断当前学生是否已经提交该任务的作业信息
        /// </summary>
        /// <param name="TaskId">任务唯一标识</param>
        /// <param name="StudentId">学生唯一标识</param>
        /// <returns>如果信息存在则返回 对应文件名,不存在则返回 Null,查询出错则返回 faile</returns>
        string WorkInforHasExist(string TaskId, string StudentId);
        /// <summary>
        /// 根据作业信息唯一标识获取对应文件名
        /// </summary>
        /// <param name="FileId">作业信息唯一标识</param>
        /// <returns>文件名</returns>
        DataResult GetFileNameById(string WorkId);
        /// <summary>
        /// 获取该学生已经提交的作业信息
        /// </summary>
        /// <param name="StudentId">学生ID</param>
        /// <returns></returns>
        DataResult GetWorkInformation(string StudentId,int page, int limit);
        /// <summary>
        /// 获取该教师的当前任职课程
        /// </summary>
        /// <param name="TeacherId">教师唯一标识</param>
        /// <returns></returns>
        DataResult GetCourseInformation(string TeacherId);
        /// <summary>
        /// 获取该教师下当前课程对应的班级
        /// </summary>
        /// <param name="TeacherId"></param>
        /// <returns></returns>
        DataResult GetClassInformation(string TeacherId,string CourseId);
        /// <summary>
        /// 获取课程对应的作业信息--用于教师作业批改
        /// </summary>
        /// <param name="CouserId">课程ID</param>
        /// <param name="ClassId">班级ID</param>
        /// <returns></returns>
        DataResult GetWorkForTeachCheck(string CouserId, string ClassId);
        /// <summary>
        /// 更新学生作业成绩
        /// </summary>
        /// <param name="WorkId">作业唯一标识</param>
        /// <param name="CourseId">课程ID</param>
        /// <param name="Score">成绩</param>
        /// <returns></returns>
        DataResult UpdateWorkScore(string WorkId, string CourseId, int Score, string StudentId);
        /// <summary>
        /// 插入作业任务信息
        /// </summary>
        /// <param name="StartTime">开始时间</param>
        /// <param name="EndTime">结束时间</param>
        /// <param name="CourseId">课程ID</param>
        /// <param name="Require">要求</param>
        /// <param name="Content">内容</param>
        /// <returns></returns>
        DataResult PublishWork(DateTime StartTime, DateTime EndTime,string TeacherId,string CourseId, string Require, string Content);
        /// <summary>
        /// 获取教室已经发布的作业任务
        /// </summary>
        /// <param name="TeacherId">教室ID</param>
        /// <returns></returns>
        DataResult GetPublishedTask(string TeacherId,int limit,int page);
    }
}
