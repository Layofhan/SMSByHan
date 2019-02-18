using SMS.Data.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entity.Models
{
    [Table("Xu_Notice")]
    public class SMSNotice : EntityBase<int>
    {
        /// <summary>
        /// 通知消息ID
        /// </summary>
        public string NoticeId { get; set; }
        /// <summary>
        /// 通知消息标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 通知消息发送时间
        /// </summary>
        public DateTime PublishDate { get; set; }
    }
}
