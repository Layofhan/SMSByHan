using SMS.Data.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entity.Models
{
    [Table("Xu_Branch")]
    public class SMSBranch : EntityBase<string>
    {
        /// <summary>
        /// 分院名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 学院名称
        /// </summary>
        public string CollegeName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
