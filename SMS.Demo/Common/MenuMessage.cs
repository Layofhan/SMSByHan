using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMS.Common
{
    public class MenuMessage
    {
        /// <summary>
        /// 一级菜单ID
        /// </summary>
        public string MenuId;
        /// <summary>
        /// 一级菜单名称
        /// </summary>
        public string Name;
        /// <summary>
        /// 一级菜单对应的二级菜单数据
        /// </summary>
        public ChildMenuMessage[] ChildNodes;
        /// <summary>
        /// 一级菜单路径
        /// </summary>
        public string Adress;
        /// <summary>
        /// 一级菜单图标路径
        /// </summary>
        public string Icon;
    }
}