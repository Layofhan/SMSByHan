using SMS.Data.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entity.Models
{
    [Table("Xu_ChildMenu")]
    public class SMSChildMenu : EntityBase<string>
    {
        /// <summary>
        /// 子菜单名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 子菜单路径
        /// </summary>
        public string Adress { get; set; }
        /// <summary>
        /// 子菜单图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 子菜单备注信息
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 父节点ID
        /// </summary>
        public string FatherId { get; set; }
    }
}
