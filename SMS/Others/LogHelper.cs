using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Web.Others
{
    public class LogHelper
    {
        /// <summary>
        /// 一般信息
        /// </summary>
        /// <param name="controllertype"></param>
        /// <param name="message"></param>
        public static void info(Type controllertype, string message)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(controllertype);
            log.Info(message);
        }
        /// <summary>
        /// 一般信息
        /// </summary>
        /// <param name="controllertype"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public static void info(Type controllertype,string message,string ex)
        {
             log4net.ILog log = log4net.LogManager.GetLogger(controllertype);
             log.Info(message,new Exception(ex));
        } 
        /// <summary>
        /// 一般错误
        /// </summary>
        /// <param name="controllertype"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public static void error(Type controllertype,string message,string ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(controllertype);
            log.Error(message,new Exception(ex));
        }
        /// <summary>
        /// 致命错误
        /// </summary>
        /// <param name="controllertype"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public static void fatal(Type controllertype, string message, string ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(controllertype);
            log.Fatal(message, new Exception(ex));
        }
        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="controllertype"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public static void debug(Type controllertype, string message, string ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(controllertype);
            log.Debug(message, new Exception(ex));
        }
        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="controllertype"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public static void warn(Type controllertype, string message, string ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(controllertype);
            log.Warn(message, new Exception(ex));
        }
    }
}
