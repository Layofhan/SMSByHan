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
    public interface ITestContract :IScopeDependency,IDependency
    {
        DataResult InsertEntity(SMSUsers entity,bool isSave = true);
        DataResult DelectEntity(SMSUsers entity, bool isSave = true);
        SMSUsers SearchEntityById(object key);
        DataResult UpdataEntity(SMSUsers entity, bool isSave = true);
        DataResult SearchAllEntity(int curr,int nums);
        ///<summary>
        ///登入
        ///</summary>
        DataResult Login(SMSUsers entity);
        ///<summary>
        ///首页根据权限进行菜单加载
        ///Auth为权限
        ///</summary>
        DataResult MenuLoading(string Auth);
    }
}
