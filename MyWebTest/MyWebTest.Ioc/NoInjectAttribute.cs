using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebTest.Ioc
{
    /// <summary>
    /// 不自动注入
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NoInjectAttribute : Attribute
    {
    }
}
