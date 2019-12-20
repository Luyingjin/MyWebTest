using Jotting.Utility.InversionOfControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebTest.Ioc
{
    public class IoCScope : IDisposable
    {
        DIScope _dIScope;
        public IoCScope(DIScope dIScope)
        {
            _dIScope = dIScope;
        }
        public T CreateInstance<T>()
        {
            return _dIScope.CreateInstance<T>(typeof(T));
        }

        public void Dispose()
        {
            _dIScope?.Dispose();
        }
        //public IoCScope(DependencyInjection dependencyInjection) : base(dependencyInjection)
        //{

        //}
    }
}
