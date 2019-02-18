using SMS.Data.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entity.Models
{
    [Table("Xu_Major")]
    public class SMSMajor : EntityBase<string>
    {
        /// <summary>
        /// 专业名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 分院ID
        /// </summary>
        public string BranchId { get; set; }
        /// <summary>
        /// 专业负责人
        /// </summary>
        public string Head { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
