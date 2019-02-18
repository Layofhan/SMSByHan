using SMS.Data.Interface;
using SMS.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Demo.Contracts
{
    public interface IHomeContract : IScopeDependency, IDependency
    {
        DataResult GetUserMessage(string id);
        /// <summary>
        /// 从吐槽变量中获取内容
        /// </summary>
        /// <returns></returns>
        DataResult GetRoast();
        /// <summary>
        /// 向变量中添加内容
        /// </summary>
        /// <param name="Content">内容</param>
        /// <returns></returns>
        DataResult SetRoast(string Content);
    }
}
