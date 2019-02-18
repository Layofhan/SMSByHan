using SMS.Data.Services;
using SMS.Demo.Common;
using SMS.Demo.Contracts;
using SMS.Entity;
using SMS.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Controllers
{
    public class EvaluationStatisticsController : Controller
    {
        public IEvaluationStatisticsContract EvaluationStatisticsContract { get; set; }
        // GET: EvaluationStatistics
        public ActionResult Index()
        {
            return View();
        }
        //返回评价活动发布视图
        public ActionResult ReleaseEvaluation()
        {
            return View();
        }
        //返回学生评教界面
        public ActionResult StudentEvaluation()
        {
            return View();
        }
        //返回学生评教详情界面
        public ActionResult AssessmentDetails()
        {
            return View();
        }
        //返回教师评价查看界面
        public ActionResult TeachingEvaluationQuery()
        {
            return View();
        }
        //返回管理员教师评分界面
        public ActionResult LectureEvaluation()
        {
            return View();
        }
        //将活动信息插入到数据库
        public ActionResult InsertInformationToTable(SMSEvaluation entity)
        {
            try
            {
                IdentityTicket ident = IdentityManager.GetIdentFromAll();
                entity.Time = DateTime.Now;
                entity.PublisherId = ident.UserId;
                return EvaluationStatisticsContract.InsertInformationToTable(entity).ToMvcJson();
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message).ToMvcJson();
            }

        }
        //返回已发布评教信息
        public ActionResult GetSendElective()
        {
            return EvaluationStatisticsContract.GetSendElective().ToMvcJson("yyyy - MM - dd HH: mm:ss");
        }
        //修改评价活动状态
        public ActionResult ChangeElectiveState(int s,string id)
        {
            return EvaluationStatisticsContract.ChangeElectiveState(s,id).ToMvcJson();
        }
        //获取学生评教所需要的信息(课程名称，课程教师名称，评教状态)
        public ActionResult GetStuElectiveInformation()
        {
            try
            {
                IdentityTicket ident = IdentityManager.GetIdentFromAll();
                return EvaluationStatisticsContract.GetStuElectiveInformation(ident.UserId).ToMvcJson();
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message).ToMvcJson();
            }
        }
        //将学生的评价情况插入到数据库中---考虑到评分方面和数据库的不对应，则不采用实体参数
        public ActionResult InsertStuScoreToTable(string EvaluationId,string StudentId,string CourseId,int acp1,int acp2,int acp3,int acp4)
        {
            SMSEvaluationTeaching entity = new SMSEvaluationTeaching();
            entity.EvaluationId = EvaluationId;
            entity.StudentId = StudentId;
            entity.Time = DateTime.Now;
            entity.CourseId = CourseId;
            entity.AspectOne = acp1;
            entity.AspectTwo = acp2;
            entity.AspectThree = acp3;
            entity.AspectFour = acp4;
            return EvaluationStatisticsContract.InsertStuScoreToTable(entity).ToMvcJson();
        }
        //返回对应教师每个课程的总评分数据
        public ActionResult GetTeachEachMark()
        {
            IdentityTicket iden = IdentityManager.GetIdentFromAll();
            return EvaluationStatisticsContract.GetTeachEachMark(iden.UserId).ToMvcJson();
        }
        //返回课程ID对应的各方面评分
        public ActionResult GetMarkByCourseId(string courseid)
        {
            return EvaluationStatisticsContract.GetMarkByCourseId(courseid).ToMvcJson();
        }
        //返回全部教师的对应评价总分
        public ActionResult GetMarkForAdminQuery(string EvaluationId = "0001")
        {
            return EvaluationStatisticsContract.GetMarkForAdminQuery(EvaluationId).ToMvcJson();
        }
        //返回评价活动的名称与对应的ID
        public ActionResult GetActivity()
        {
            return EvaluationStatisticsContract.GetActivity().ToMvcJson();
        }
    }
}