using SMS.Data.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entity.Models
{
    [Table("Xu_Course")]
    public class SMSCourse : EntityBase<string>
    {
        /// <summary>
        /// 课程名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 课程性质ID
        /// </summary>
        public string NaturesId { get; set; }
        /// <summary>
        /// 分院ID
        /// </summary>
        public string BranchId { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 学分
        /// </summary>
        public string Credit { get; set; }
        /// <summary>
        /// 绩点
        /// </summary>
        public string Indexs { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
