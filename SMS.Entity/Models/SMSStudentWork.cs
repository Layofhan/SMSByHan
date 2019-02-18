using SMS.Data.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entity.Models
{
    [Table("Xu_StudentWork")]
    public class SMSStudentWork : EntityBase<int>
    {
        /// <summary>
        /// 学生ID
        /// </summary>
        public string StudentId { get; set; }
        /// <summary>
        /// 作业ID，作业的唯一标识，由16位数字组成，年月日时分秒+4位随机数
        /// </summary>
        public string WorkId { get; set; }
        /// <summary>
        /// 文件名（实际名字+文件唯一标识）
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 上交时间
        /// </summary>
        public DateTime UpTime { get; set; }
        /// <summary>
        /// 课程ID
        /// </summary>
        public string CourseId { get; set; }
        /// <summary>
        /// 任务ID，对应教师发布的任务唯一标识
        /// </summary>
        public string TaskId { get; set; }
        /// <summary>
        /// 状态 0-未提交，1-已提交，2-已批改，3-逾期未提交
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 教师ID
        /// </summary>
        public string TeacherId { get; set; }
        /// <summary>
        /// 成绩
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// 作业批改时间
        /// </summary>
        public DateTime CheckTime{get;set;}
    }
}
