using SMS.Data.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entity.Models
{
    [Table("Xu_CourseStudentMap")]
    public class SMSCourseStudentMap : EntityBase<int>
    {
        /// <summary>
        /// 课程ID
        /// </summary>
        public string CourseId { get; set; }
        /// <summary>
        /// 学生ID
        /// </summary>
        public string StudentId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 学分
        /// </summary>
        public string Credit { get; set; }
        /// <summary>
        /// 成绩
        /// </summary>
        public int Achievement { get; set; }
        /// <summary>
        /// 绩点
        /// </summary>
        public string Indexs { get; set; }
        /// <summary>
        /// 辅修标记(0为是 1为不是)
        /// </summary>
        public int MinorRepairMark { get; set; }
        /// <summary>
        /// 补考成绩
        /// </summary>
        public int MakeupExam { get; set; }
        /// <summary>
        /// 重修成绩
        /// </summary>
        public int ReworkExam { get; set; }
        /// <summary>
        /// 重修标记
        /// </summary>
        public int ReworkMark { get; set; }
        /// <summary>
        /// 学年ID
        /// </summary>
        public string GradeId { get; set; }
        /// <summary>
        /// 期中评价标志
        /// </summary>
        public int MidEvaluation{ get; set; }
        /// <summary>
        /// 期末评价标志
        /// </summary>
        public int FinalEvaluation { get; set; }
    }
}
