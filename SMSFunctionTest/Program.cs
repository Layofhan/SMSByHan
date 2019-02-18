using Autofac;
using SMS.Data.Interface;
using SMS.Data.Services;
using SMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMSFunctionTest
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //InitialEntityMapper.InitialMapperAssembly(
            //    //Assembly.GetExecutingAssembly()
            //    Assembly.LoadFile(@"D:\Layoomiety\SMS\SMS.Entity\bin\Debug\SMS.Entity.dll")
            //    );
            ContainerBuilder builder = new ContainerBuilder();
            //builder.RegisterGeneric(typeof(EFRepositoryBase<,>)).As(typeof(IRepository<,>));
            //Type baseType = typeof(IDependency);

            //// 获取所有相关类库的程序集
            ////Assembly[] assemblies = Assembly.GetExecutingAssembly();
            //var assemblies = Assembly.GetExecutingAssembly();

            //builder.RegisterAssemblyTypes(assemblies)
            //    .Where(type => baseType.IsAssignableFrom(type) && !type.IsAbstract)
            //    .AsImplementedInterfaces().InstancePerLifetimeScope();//InstancePerLifetimeScope 保证对象生命周期基于请求
            //IContainer container = builder.Build();
            //DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            Application.Run(new Form1());
        }
    }
}
