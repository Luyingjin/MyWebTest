using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using IoCx = MyWebTest.Ioc.IoC;
namespace MyWebTest
{
    /// <summary>
    /// controller 自动注入
    /// </summary>
    public class IocInjectFilter : ActionFilterAttribute, System.Web.Mvc.IActionFilter
    {

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            IoCx.DisposeScope();
            //释放
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            IoCx.Inject(filterContext.Controller);
        }
        

       
    }
}
