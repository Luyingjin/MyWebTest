using Jotting.Utility.Factory.Entity;
using Jotting.Utility.InversionOfControl;
using Jotting.Utility.Reflection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyWebTest.Ioc
{
    public static class IoC
    {
        static IoC()
        {

            _resolver.BuidAppDomain();
            _di.SetJudge(r => !(r.CustomAttributeProvider.GetCustomAttributes(typeof(NoInjectAttribute), false)?.Count() > 0));
        }
        static DependencyInjection _di = new DependencyInjection();
        static AssemblyFileResolver _resolver = new AssemblyFileResolver();
        static Task LoadTask;
        static HashSet<string> HasRegister = new HashSet<string>();
        static object Lock = new object();
        /// <summary>
        /// 根据接口获取实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T CreateInstance<T>()
        {
            LoadTask?.Wait();
            try
            {
                FactoryScope.InitScope();
                return _di.Lookup<T>(typeof(T));
            }
            catch
            {
                return default(T);
            }
        }
        public static IoCScope CreateScope()
        {
            LoadTask?.Wait();
            return new IoCScope(new DIScope(_di));
        }

        public static void Register<interfaceT, T>(bool singleton = false) where T : class, interfaceT, new()
        {
            _di.Register<T>(typeof(interfaceT), singleton);
        }
        public static void Register<interfaceT>(Func<object> func, bool singleton = false)
        {
            _di.Register(typeof(interfaceT), func, singleton);

        }

        public static void Inject(object x)
        {
            FactoryScope.InitScope();
            LoadTask?.Wait();
            _di.Inject(x);

        }

        public static void DisposeScope()
        {
            FactoryScope.Current?.Free();
            //执行注入实例的dispose方法
        }


        /// <summary>
        /// 依赖注册
        /// </summary>
        /// <param name="directory"></param>
        public static void Register(string directory)
        {
            lock (Lock)
            {
                if (!HasRegister.Contains(directory))
                {
                    HasRegister.Add(directory);
                }
                else
                {
                    return;
                }
                if (LoadTask != null)
                {
                    LoadTask.Wait();
                }
                LoadTask = Task.Run(() =>
                {

                    LoadDirectory(directory);
                    var fsw = new FileSystemWatcher(directory);
                    FileSystemEventHandler gin = (o, e) =>
                    {
                        if (e.FullPath.EndsWith(".dll"))
                        {
                            LoadTask?.Wait();
                            LoadFile(e.FullPath);

                        }
                    };
                    fsw.Filter = "*.dll";
                    fsw.Changed += gin;
                    fsw.Created += gin;
                    fsw.EnableRaisingEvents = true;
                    fsw.NotifyFilter = NotifyFilters.Size | NotifyFilters.LastWrite | NotifyFilters.FileName;
                });


            }

        }

        private static void LoadDirectory(string directory)
        {
            try
            {
                AssemblyQuery aq = new AssemblyQuery();
                var kk = aq.Search(directory, null, _resolver).SelectMany(s => s.DefinedTypes);
                foreach (var tp in kk)
                {
                    var singleton = tp.GetCustomAttribute<SingletonAttribute>() != null;
                    var nc = tp.GetCustomAttributes<RegisterAttribute>();
                    if (nc?.Count() > 0)
                    {
                        foreach (RegisterAttribute n in nc)
                        {
                            if (n.InterfaceType.IsAssignableFrom(tp) || n.InterfaceType == tp || (n.InterfaceType.IsGenericTypeDefinition && tp.ImplementedInterfaces.Any(r => r.GetGenericTypeDefinition() == n.InterfaceType)))
                            {
                                _di.Register(n.InterfaceType, tp, singleton);
                            }
                            else
                            {
                                System.Diagnostics.Trace.TraceError($"{tp.FullName}未继承接口{n.InterfaceType.FullName}，无法注册");
                            }
                        }

                    }
                }
            }
            catch (Exception)
            {

            }



        }
        private static void LoadFile(string filepath)
        {
            try
            {
                AssemblyQuery aq = new AssemblyQuery();
                var kk = aq.LoadFile(filepath, _resolver)?.DefinedTypes;
                if (kk == null) return;
                foreach (var tp in kk)
                {
                    var singleton = tp.GetCustomAttribute<SingletonAttribute>() != null;
                    var nc = tp.GetCustomAttributes<RegisterAttribute>();
                    if (nc?.Count() > 0)
                    {
                        foreach (RegisterAttribute n in nc)
                        {
                            if (n.InterfaceType.IsAssignableFrom(tp) || n.InterfaceType == tp || (n.InterfaceType.IsGenericTypeDefinition && tp.ImplementedInterfaces.Any(r => r.GetGenericTypeDefinition() == n.InterfaceType)))
                            {
                                _di.Register(n.InterfaceType, tp, singleton);
                            }
                            else
                            {
                                System.Diagnostics.Trace.TraceError($"{tp.FullName}未继承接口{n.InterfaceType.FullName}，无法注册");
                            }
                        }

                    }
                }
            }
            catch
            {

            }



        }

    }
}
