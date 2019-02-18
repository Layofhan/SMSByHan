using SMS.Data.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entity.Models
{
    [Table("Xu_RoleAuthorityMap")]
    public class SMSRoleAuthorityMap : EntityBase<int>
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 权限ID
        /// </summary>
        public string AuthorityId { get; set; }
    }
}
