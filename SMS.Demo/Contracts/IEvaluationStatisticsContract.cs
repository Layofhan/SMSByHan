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
    public interface IEvaluationStatisticsContract : IScopeDependency, IDependency
    {
        //将评教信息插入评教活动表中
        DataResult InsertInformationToTable(SMSEvaluation entity);
        //返回已经保存的评教活动
        DataResult GetSendElective();
        //修改活动状态
        DataResult ChangeElectiveState(int s,string id);
        //获取学生评教所需要的信息(课程名称，课程教师名称，评教状态)
        DataResult GetStuElectiveInformation(string id);
        //将学生的评价情况插入到数据库中
        DataResult InsertStuScoreToTable(SMSEvaluationTeaching entity);
        //返回对应教师每个课程的总评分数据
        DataResult GetTeachEachMark(string id);
        //返回课程ID对应的各方面评分
        DataResult GetMarkByCourseId(string courseid);
        //返回活动ID对应的各个老师的总评分
        DataResult GetMarkForAdminQuery(string EvaluationId);
        //返回评价活动的名称与对应的ID
        DataResult GetActivity();
    }
}
