using SMS.Data.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entity.Models
{
    [Table("Xu_NotifyContact")]
    public class SMSNotifyContact : EntityBase<int>
    {
        /// <summary>
        /// 消息发布者ID
        /// </summary>
        public string PublisherId { get; set; }
        /// <summary>
        /// 消息接收者ID
        /// </summary>
        public string ReceiverId { get; set; }
        /// <summary>
        /// 消息状态判断标识 0未读 1已读
        /// </summary>
        public int Marker { get; set; }
        /// <summary>
        /// 消息ID
        /// </summary>
        public string NoticeId { get; set; }
    }
}
