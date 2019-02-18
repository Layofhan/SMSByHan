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
    public interface IOnlineCourseSelectionContract : IScopeDependency, IDependency
    {
        //根据选课类型获取对应的选课信息
        DataResult GetChoiceCourseInformation(string stuid,int typeid);
        //将学生的选课信息插入学生与课程关联表中，Id为选课类型，用于不同类型下得选课判断
        DataResult InsertChooseCourseToTable(int Id,SMSCourseStudentMap csm);
        //根据选课性质，获取对应的学生已选课程的信息
        DataResult GetChoosedInformation(int Id,string studentid);
    }
}
