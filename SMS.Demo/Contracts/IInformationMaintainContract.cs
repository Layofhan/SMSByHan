using SMS.Data.Interface;
using SMS.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Demo.Contracts
{
    public interface IInformationMaintainContract : IScopeDependency, IDependency
    {
        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="UserId">用户ID</param>
        /// <param name="NewPassword">新密码</param>
        /// <returns></returns>
        DataResult ModificationPassword(string UserId,string NewPassword);
        /// <summary>
        /// 返回用户基础信息
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        DataResult SearchBaseInfor(string UserId);
    }
}
