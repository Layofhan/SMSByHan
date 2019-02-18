using SMS.Data.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entity.Models
{
    [Table("Xu_StudentTask")]
    public class SMSStudentTask : EntityBase<int>
    {
        /// <summary>
        /// 教师ID
        /// </summary>
        public string TeacherId { get; set; }
        /// <summary>
        /// 任务发布时间
        /// </summary>
        public DateTime SendTime { get; set; }
        /// <summary>
        /// 任务开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 任务结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 任务唯一标识，由16位数字组成，年月日时分秒+4位随机数
        /// </summary>
        public string TaskId { get; set; }
        /// <summary>
        /// 课程ID
        /// </summary>
        public string CourseId { get; set; }
        /// <summary>
        /// 状态--保留字段
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 要求
        /// </summary>
        public string Require { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
    }
}
