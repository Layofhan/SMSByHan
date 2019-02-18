using SMS.Data.Services;
using SMS.Demo.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Web.Two.Controllers
{
    public class TestController : Controller
    {
        public ITestContract TestContract { get; set; }
        [AllowAnonymous]
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }
        [TestCheck(ActionName = "te_test")]
        public ActionResult te()
        {
            return null;
           // return TestContract.SearchAllEntity().ToMvcJson();
        }
    }
}