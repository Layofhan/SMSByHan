using SMS.Data.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entity.Models
{
    [Table("Xu_Evaluation")]
    public class SMSEvaluation : EntityBase<string>
    {
        /// <summary>
        /// 评价活动标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 评价发布时间
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 评价活动发布人
        /// </summary>
        public string PublisherId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 评价状态(0为关闭，1为开启)
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 学年ID
        /// </summary>
        public string GradeId { get; set; }
        /// <summary>
        /// 时间类型(0为期中，1为期末)
        /// </summary>
        public int TimeType { get; set; }
        /// <summary>
        /// 功能类型(0为教学评价，1为毕业论文评价)
        /// </summary>
        public int FunctionType { get; set; }
    }
}
