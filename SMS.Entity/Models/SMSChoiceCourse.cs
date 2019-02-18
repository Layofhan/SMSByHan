using SMS.Data.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entity.Models
{
    [Table("Xu_ChoiceCourse")]
    public class SMSChoiceCourse : EntityBase<string>
    {
        /// <summary>
        /// 课程ID
        /// </summary>
        public string CourseId { get; set; }
        /// <summary>
        /// 容量
        /// </summary>
        public int Capacity { get; set; }
        /// <summary>
        /// 专业ID
        /// </summary>
        public string MajorId { get; set; }
        /// <summary>
        /// 年级ID
        /// </summary>
        public string GradeId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 选课类型
        /// 0:全校性选课;1:体育选课;2:重修或补休选报;3:专业选修课;4:专业方向选报
        /// </summary>
        public int TypeId { get; set; }
        /// <summary>
        /// 余量
        /// </summary>
        public int Surplus { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enable { get; set; }
    }
}
