using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Demo.Common
{
    public class IdentityTicket
    {
        /// <summary>
        ///     是否在线
        /// </summary>
        public bool IsForceOffline { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        ///     用户名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户角色
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        ///     用户权限
        /// </summary>
        public string[] Authoritys { get; set; }
    }
}
