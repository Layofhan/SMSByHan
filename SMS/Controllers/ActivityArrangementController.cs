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
    public class ActivityArrangementController : Controller
    {
        public IActivityArrangementContract ActivityArrangementContract { get; set; }
        // GET: ActivityArrangement
        public ActionResult Index()
        {
            return View();
        }
        //返回选课报名界面
        public ActionResult ElectiveRegistration()
        {
            return View();
        }
        public ActionResult InsertChoiceCourseToTabl(SMSChoiceCourse course) 
        {
            return ActivityArrangementContract.InsertChoiceCourseToTabl(course).ToMvcJson();
        }
    }
}