using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Data.Interface
{
    public interface IScopeDependency:IDependency
    {
        //用于业务层数据调用继承的接口，与数据层区分
    }
}
