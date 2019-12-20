using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebTest.Ioc
{
    /// <summary>
    /// 注册,泛类需要对应泛型接口
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RegisterAttribute : Attribute
    {
        public Type InterfaceType { get; set; }
        public RegisterAttribute(Type interfaceType)
        {
            InterfaceType = interfaceType;
        }

    }
}
