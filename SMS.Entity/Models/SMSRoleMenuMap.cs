using SMS.Data.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entity.Models
{
    [Table("Xu_RoleMenuMap")]
    public class SMSRoleMenuMap: EntityBase<int>
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 菜单ID
        /// </summary>
        public string MenuId { get; set; }
    }
}
