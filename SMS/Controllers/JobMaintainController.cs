using SMS.Data.Services;
using SMS.Demo.Common;
using SMS.Demo.Contracts;
using SMS.Entity;
using SMS.Entity.Result;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Controllers
{
    public class JobMaintainController : Controller
    {
        public IJobinfoAndManageContract JobinfoAndManageContract { get; set; }
        // GET: JobMaintain
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult FileSubmit(string taskId,string studentId,string teacherId, string courseId)
        {
            ViewBag.TaskId = taskId;
            ViewBag.StudentId = studentId;
            ViewBag.TeacherId = teacherId;
            ViewBag.CourseId = courseId;
            return View();
        }
        /// <summary>
        /// word在线浏览功能
        /// </summary>
        /// <param name="workId">作业信息唯一标识</param>
        /// <returns></returns>
        public ActionResult OnlineWordBrowsing(string workId)
        {
            try
            {
                //获取文件名
                DataResult dataresult = JobinfoAndManageContract.GetFileNameById(workId);
                
                if (dataresult.Success)
                {
                    string filenames = dataresult.Message;
                    var ServiceName = Server.MapPath("~/StacticPage/");
                    var FileName = Server.MapPath("~/WordFiles/") + filenames;
                    var url = OnlineWordBrowse.WordToHtml(FileName, ServiceName);
                    string host = Request.Url.Host;
                    int port = Request.Url.Port;
                    url = "http://" + host + ":" + port + "/StacticPage/" + url;
                    return DataProcess.Success(url).ToMvcJson();
                }
                else
                {
                    return dataresult.ToMvcJson();
                }
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message).ToMvcJson();
            }
        }
        /// <summary>
        /// 文件提交
        /// </summary>
        /// <param name="TaskId">任务信息唯一标识</param>
        /// <param name="StudentId">学生唯一标识</param>
        /// <param name="TeacherId">教师唯一标识</param>
        /// <param name="CourseId">课程唯一标识</param>
        /// <returns></returns>
        public ActionResult Upload(string TaskId,string StudentId,string TeacherId,string CourseId)
        {
            try
            {
                //获取是否已有提交记录
                string orderfilename = JobinfoAndManageContract.WorkInforHasExist(TaskId, StudentId);
                if ("faile".Equals(orderfilename))
                {
                    return DataProcess.Failure("数据查询出错").ToMvcJson();
                }
                var file = Request.Files[0];
                var SavePath = Server.MapPath("~/WordFiles/");
                //string filename = file.FileName + "_" + DateTime.Now.ToString("yyyyMMdd");
                string filename = file.FileName.Substring(0,file.FileName.LastIndexOf('.')) + "_" + DateTime.Now.ToString("yyyyMMdd") + file.FileName.Substring(file.FileName.LastIndexOf('.')+1, file.FileName.Length- file.FileName.LastIndexOf('.')-1);               
                file.SaveAs(Path.Combine(SavePath, filename));                
                //查询是否有提交记录
                if (orderfilename != null && !("faile".Equals(orderfilename)))
                {
                    //更新之前提交的内容
                    if(JobinfoAndManageContract.UpdateWorkInfor(TaskId, StudentId, filename).Success)
                    {
                        //删除原有的文件
                        System.IO.File.Delete(Path.Combine(SavePath, orderfilename));
                        return DataProcess.Success("内容更新成功").ToMvcJson();
                    }
                    else
                    {
                        //删除现有文件
                        System.IO.File.Delete(Path.Combine(SavePath, filename));
                        return DataProcess.Failure().ToMvcJson();
                    }                   
                }
                //将信息保存到数据库中
                if (JobinfoAndManageContract.SaveWorkInfor(TaskId, StudentId, TeacherId, CourseId, filename).Success)
                {
                    return DataProcess.Success("内容保存成功").ToMvcJson();
                }
                else
                {
                    System.IO.File.Delete(Path.Combine(SavePath, filename));
                    return DataProcess.Failure("保存失败，请重试").ToMvcJson();
                }            
            }
            catch (Exception ex)
            {
                return DataProcess.Failure(ex.Message).ToMvcJson();
            }
        }
        /// <summary>
        /// 学生获取作业
        /// </summary>
        /// <returns></returns>
        public ActionResult GetWorkTask()
        {
            try
            {
                IdentityTicket identity = IdentityManager.GetIdentFromAll();
                return JobinfoAndManageContract.GetWorkTask(identity.UserId).ToMvcJson("yyyy-MM-dd HH:mm:ss");
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message).ToMvcJson();
            }
        }
        /// <summary>
        /// 获取当前学生已提交的作业
        /// </summary>
        /// <param name="page">当前页面</param>
        /// <param name="limit">当前一个数量</param>
        /// <returns></returns>
        public ActionResult GetWorkInformation(int page, int limit)
        {
            try
            {
                IdentityTicket identity = IdentityManager.GetIdentFromAll();
                return JobinfoAndManageContract.GetWorkInformation(identity.UserId, page, limit).ToMvcJson("yyyy-MM-dd HH:mm:ss");
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message).ToMvcJson();
            }
        }
    }
}