using Autofac;
using Autofac.Integration.Mvc;
using SMS.Data.Interface;
using SMS.Data.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace SMS.App_Start
{
    public class AutofacConfig
    {
        public static void AutofacResolver()
        {
            #region 单利运行成功
            // var builder = new ContainerBuilder();
            // builder.RegisterType<EFRepositoryBase<SMSUsers, int>>().As<IRepository<SMSUsers, int>>();
            // var a = Assembly.GetExecutingAssembly();
            // //
            // builder.RegisterControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            // //builder.RegisterControllers(Assembly.GetExecutingAssembly());
            // var container = builder.Build();
            // DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            #endregion

            #region 自动注入
            //创建autofac管理注册类的容器实例
            var builder = new ContainerBuilder();

            //注册所有实现了 IDependency 接口的类型
            Type baseType = typeof(IDependency);
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            //Assembly[] assembliess = AppDomain.CurrentDomain.GetAssemblies().ToArray();//获取已加载到此应用程序域的执行上下文中的程序集。
            Assembly[] assembliess = FindAll();
            Type[] dependencyTypes = assembliess
                .SelectMany(s => s.GetTypes())
                .Where(p => baseType.IsAssignableFrom(p) && p != baseType).ToArray();
            //将继承于IDependency 接口的程序集批量注册到容器中--待解决问题：泛型集合的批量注册问题
            builder.RegisterAssemblyTypes(assembliess)
                   .Where(type => baseType.IsAssignableFrom(type) && !type.IsAbstract)
                   .PropertiesAutowired()
                   .InstancePerLifetimeScope()
                   .AsImplementedInterfaces();

            //将仓储注册到容器中
            builder.RegisterGeneric(typeof(EFRepositoryBase<,>)).As(typeof(IRepository<,>))
                .PropertiesAutowired().InstancePerLifetimeScope();

            //注册MVC类型
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();
            //特性注入
            //builder.RegisterFilterProvider();
            //生成容器并提供给MVC
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            #endregion

        }
        public static string GetBinPath()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (baseDirectory != (Environment.CurrentDirectory + @"\"))
            {
                return Path.Combine(baseDirectory, "bin");
            }
            return baseDirectory;
        }
        public static Assembly[] FindAll()
        {
            Assembly[] assemblyArray = Directory.GetFiles(GetBinPath(), "*.dll", SearchOption.TopDirectoryOnly).Concat<string>(Directory.GetFiles(GetBinPath(), "*.exe", SearchOption.TopDirectoryOnly)).ToArray<string>().Select<string, Assembly>(new Func<string, Assembly>(Assembly.LoadFrom)).Distinct<Assembly>().ToArray<Assembly>();
            return assemblyArray;
        }
    }
}