using SMS.Data.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entity.Models
{
    [Table("Xu_CourseSurplus")]
    public class SMSCourseSurplus : EntityBase<string>
    {
        /// <summary>
        /// 课程ID
        /// </summary>
        public string CourseId { get; set; }
        /// <summary>
        /// 余量
        /// </summary>
        public int Surplus { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
