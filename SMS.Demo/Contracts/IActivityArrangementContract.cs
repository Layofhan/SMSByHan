using SMS.Data.Interface;
using SMS.Entity.Models;
using SMS.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Demo.Contracts
{
    public interface IActivityArrangementContract : IScopeDependency, IDependency
    {
        //将选课信息插入选课表中
        DataResult InsertChoiceCourseToTabl(SMSChoiceCourse course);
    }
}
