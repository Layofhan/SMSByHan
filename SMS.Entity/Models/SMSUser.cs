using SMS.Data.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entity.Models
{
    [Table("Xu_User")]
    public class SMSUser : EntityBase<string>
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public Int16 Sex { get; set; }
        public string Birthdate { get; set; }
        public string Mobilephone { get; set; }
        public string Email { get; set; }
        public string Qq { get; set; }
        public string WeXin { get; set; }
        /// <summary>
        /// 加入时间
        /// </summary>
        public DateTime JoinDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserId { get; set; }
        public string CreateUserName { get; set; }
    }
}
