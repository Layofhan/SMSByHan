using SMS.Data.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entity.Models
{
    [Table("Xu_EvaluationTeaching")]
    public class SMSEvaluationTeaching : EntityBase<int>
    {
        /// <summary>
        /// 评价活动ID
        /// </summary>
        public string EvaluationId { get; set; }
        /// <summary>
        /// 学生ID
        /// </summary>
        public string StudentId { get; set; }
        /// <summary>
        /// 评价时间
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 课程ID
        /// </summary>
        public string CourseId { get; set; }
        /// <summary>
        /// 各种评价内容的分数 eg：作业批改情况
        /// </summary>
        public int AspectOne { get; set; }
        public int AspectTwo { get; set; }
        public int AspectThree { get; set; }
        public int AspectFour { get; set; }
        public int AspectFive { get; set; }
        public int AspectSix { get; set; }
    }
}
