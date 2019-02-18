using SMS.Data.Interface;
using SMS.Entity;
using SMS.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Demo.Contracts
{
    public interface IMenuContract : IScopeDependency, IDependency
    {
        /// <summary>
        /// 首页菜单数据
        /// </summary>
        /// <returns></returns>
        DataResult LoadingMenu();
    }
}
