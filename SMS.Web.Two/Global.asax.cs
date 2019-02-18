
using Autofac;
using Autofac.Integration.Mvc;
using SMS.Data.Interface;
using SMS.Data.Services;
using SMS.Interceptor;
using SMS.Web.Two.Interceptor;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SMS.Web.Two
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
           // AutofacResolver();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        private void AutofacResolver()
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

            Assembly[] assembliess = AppDomain.CurrentDomain.GetAssemblies().ToArray();//获取已加载到此应用程序域的执行上下文中的程序集。

            Type[] dependencyTypes = assembliess
                .SelectMany(s => s.GetTypes())
                .Where(p => baseType.IsAssignableFrom(p) && p != baseType).ToArray();
            //Type[] sd = assembliess.SelectMany(s => s.GetTypes()).Where(p => typeof(AuthorizationFilter).IsAssignableFrom(p)).ToArray();
            //将继承于IDependency 接口的程序集批量注册到容器中--待解决问题：泛型集合的批量注册问题
            builder.RegisterAssemblyTypes(assemblies)
                   .Where(type => baseType.IsAssignableFrom(type) && !type.IsAbstract)
                   .PropertiesAutowired()
                   .InstancePerLifetimeScope()
                   .AsImplementedInterfaces();
            //builder.RegisterAssemblyTypes(assemblies)
            //    .Where(p => typeof(AuthorizationFilter).IsAssignableFrom(p))
            //    .PropertiesAutowired()
            //    .InstancePerLifetimeScope()
            //    .AsImplementedInterfaces();
            //将仓储注册到容器中
            builder.RegisterGeneric(typeof(EFRepositoryBase<,>)).As(typeof(IRepository<,>))
                .PropertiesAutowired().InstancePerLifetimeScope();

            //将拦截器注册到容器中
            //builder.RegisterType(typeof(AuthorizationFilter)).AsImplementedInterfaces().SingleInstance()
            //   .PropertiesAutowired();


            //注册MVC类型
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired(PropertyWiringOptions.None);
            //特性注入
            builder.RegisterFilterProvider();


            //生成容器并提供给MVC
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolvers(container));
            #endregion

        }
    }
}
