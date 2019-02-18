using SMS.Data.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entity.Models
{
    [Table("Xu_TeacherCourseMap")]
    public class SMSTeacherCourseMap : EntityBase<int>
    {
        /// <summary>
        /// 课程ID
        /// </summary>
        public string CourseId { get; set; }
        /// <summary>
        /// 教师ID
        /// </summary>
        public string TeacherId { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 周学时
        /// </summary>
        public string LearnTime { get; set; }
        /// <summary>
        /// 起始结束时间
        /// </summary>
        public string StartEndTime { get; set; }
        /// <summary>
        /// 校区
        /// </summary>
        public string Campus { get; set; }
        /// <summary>
        /// 开课时间
        /// </summary>
        public DateTime OpenClassTime { get; set; }
        /// <summary>
        /// 地点
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 教材
        /// </summary>
        public string Books { get; set; }
        /// <summary>
        /// 考试时间
        /// </summary>
        public DateTime ExamTime { get; set; }
    }
}
