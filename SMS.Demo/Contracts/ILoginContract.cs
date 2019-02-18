using SMS.Data.Interface;
using SMS.Entity;
using SMS.Entity.Models;
using SMS.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Demo.Contracts
{
    public interface ILoginContract : IScopeDependency, IDependency
    {
        ///<summary>
        ///登入验证
        ///<para>传入值为用户实体</para>
        ///<returns></return>
        /// </summary>
        DataResult Login(string Name,string Password,bool isChace);
    }
}
