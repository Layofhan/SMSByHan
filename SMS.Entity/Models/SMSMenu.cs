using SMS.Data.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entity.Models
{
    [Table("Xu_Menu")]
    public class SMSMenu : EntityBase<string>
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 菜单路径
        /// </summary>
        public string Adress { get; set; }
        /// <summary>
        /// 图标路径
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }


    }
}
