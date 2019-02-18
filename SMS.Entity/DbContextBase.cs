using SMS.Data.Interface;
using SMS.Data.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entity
{
    public class DbContextBase: DbContext
    {
        //设置连接字符串
        public DbContextBase() : base(GetConnectionString()) { }
       // public DbSet<SMSUsers> SMSUsers { get; set; }

        //获取配置文件中的连接字符串
        public static string GetConnectionString()
        {
            string dbConnectionname = "Default";
            ConnectionStringSettings conconnectionStringSettings = ConfigurationManager.ConnectionStrings[dbConnectionname];
            if (conconnectionStringSettings == null)
            {
                return "请配置数据库连接字符串，连接名称"+dbConnectionname;
            }
            return conconnectionStringSettings.ConnectionString;
        }
        //初始化上下文时自动执行
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //移除一对多级联删除
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //获取实体配置信息
            IEntityMapper[] entityMappers = InitialEntityMapper.Initial;
            //将实体配置信息 注册到上下文中
            foreach (IEntityMapper mapper in entityMappers)
            {
                mapper.RegistTo(modelBuilder.Configurations);
            }
        }
    }
}
