using SMS.Data.Services;
using SMS.Demo.Contracts;
using SMS.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Controllers
{
    
    /// <summary>
    /// 管理员功能的的基础信息维护
    /// </summary>
    public class BaseInformationManageController : Controller
    {
        public IBaseInformationManageContract BaseInformationManageContract { get; set; }
        // GET: BaseInformationManage
        public ActionResult Index()
        {
            return View();
        }
        //返回创建年级视图
        public ActionResult CreateGrade()
        {
            return View();
        }
        //返回创建分院信息视图
        public ActionResult CreateBranch()
        {
            return View();
        }
        //返回创建分院专业信息视图
        public ActionResult CreateMajor()
        {
            return View();
        }
        //返回创建课程性质视图
        public ActionResult CreateNature()
        {
            return View();
        }
        //查询性质ID是否重复
        public ActionResult NatureIdIsRepeat(string id)
        {
            return BaseInformationManageContract.NatureIdIsRepeat(id).ToMvcJson();
        }
        //将课程性质信息插入到课程性质表中
        public ActionResult InsertNatureToTable(string id, string name, string remarket)
        {
            SMSCurriculumNature nature = new SMSCurriculumNature();
            nature.Id = id;
            nature.Name = name;
            nature.Remark = remarket;
            return BaseInformationManageContract.InsertNatureToTable(nature).ToMvcJson();
        }
        //将年级信息插入年级表中
        public ActionResult InsertGradeToTable(string id,string remarket)
        {
            SMSGrade grade = new SMSGrade();
            grade.Id = id;
            grade.Remark = remarket;
            return BaseInformationManageContract.InsertGradeToTable(grade).ToMvcJson();
        }
        //将分院信息插入分院信息表中
        public ActionResult InsertBranchToTable(string id,string branchname,string collegename,string remark)
        {
            SMSBranch branch = new SMSBranch();
            branch.Id = id;
            branch.Name = branchname;
            branch.CollegeName = collegename;
            branch.Remark = remark;
            return BaseInformationManageContract.InsertBranchToTable(branch).ToMvcJson();
        }
        //获取分院信息
        public ActionResult GetBranchInformation()
        {
            return BaseInformationManageContract.GetBranchInformation().ToMvcJson();
        }
        //将专业信息插入到专业信息表中
        public ActionResult InsertMajorToTable(string id,string majorname,string branchid,string majorpeople,string remark)
        {
            SMSMajor major = new SMSMajor();
            major.Id = id;
            major.Name = majorname;
            major.BranchId = branchid;
            major.Remark = remark;
            return BaseInformationManageContract.InsertMajorToTable(major).ToMvcJson();
        }
        //获取课程性质信息
        public ActionResult GetNatureInformation()
        {
            return BaseInformationManageContract.GetNatureInformation().ToMvcJson();
        }
    }
}