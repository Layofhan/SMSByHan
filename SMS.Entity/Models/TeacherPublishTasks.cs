using SMS.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entity.Models
{
    /// <summary>
    /// 教师发布作业类
    /// </summary>
    public class TeacherPublishTasks
    {
        /// <summary>
        /// 课程ID
        /// </summary>
        public string CourseId;
        /// <summary>
        /// 要求
        /// </summary>
        public string Require;
        /// <summary>
        /// 内容
        /// </summary>
        public string Content;
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime SendTime;
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime;
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime;
        /// <summary>
        /// 状态
        /// </summary>
        public int State;
        /// <summary>
        /// 课程名字
        /// </summary>
        public string CourseName;
    }
}
