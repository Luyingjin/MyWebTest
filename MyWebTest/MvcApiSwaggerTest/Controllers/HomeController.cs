using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApiSwaggerTest.Controllers
{
    /// <summary>
    /// Home控制器
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// About
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}