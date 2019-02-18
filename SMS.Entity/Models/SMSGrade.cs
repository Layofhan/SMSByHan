using SMS.Data.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entity.Models
{
    [Table("Xu_Grade")]
    public class SMSGrade : EntityBase<string>
    {
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
