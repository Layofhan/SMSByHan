using SMS.Data.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entity.Models
{
    [Table("Xu_NoticeParticular")]
    public class SMSNoticeParticular : EntityBase<int>
    {
        /// <summary>
        /// 通知消息内容
        /// </summary>
        public string Contents { get; set; }
        /// <summary>
        /// 通知消息ID
        /// </summary>
        public string NoticeId { get; set; }
    }
}
