using SMS.Data.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entity.Models
{
    [Table("Xu_StudentBase")]
    public class SMSStudentBase : EntityBase<string>
    {
        /// <summary>
        /// 班级
        /// </summary>
        public string Class { get; set; }
        /// <summary>
        /// 专业ID
        /// </summary>
        public string MajorId { get; set; }
        /// <summary>
        /// 年级ID
        /// </summary>
        public string GradeId { get; set; }
        /// <summary>
        /// 专业方向ID
        /// </summary>
        public string MajorDirectionId { get; set; }
    }
}
