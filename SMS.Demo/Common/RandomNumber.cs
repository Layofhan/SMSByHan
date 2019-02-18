using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Demo.Common
{
    public static class RandomNumber
    {
        public static int noticeid;
        /// <summary>
        /// 用于生成消息ID，理论上可实现1秒内100条消息ID的生成需求
        /// </summary>
        /// <returns></returns>
        public static string Init()
        {
            string time = DateTime.Now.ToString();
            string[] time1 = time.Split('/');
            string[] time2 = time1[3].Split(' ');
            time2[1] = time2[1].Replace(":","");
            time = time1[0] + time1[1] + time1[2]+time2[1];
            noticeid += 1;
            if (noticeid == 1 || noticeid == 100) noticeid = 11;
            return time + noticeid;
        } 
        /// <summary>
        /// 随机生成18位数 年月日时分秒+四位随机数
        /// </summary>
        /// <returns></returns>
        public static string GenerateNum()
        {
            string time = DateTime.Now.ToString("yyyyMMddHHmmss");
            Random random = new Random();
            string rand = random.Next(1000,9999).ToString();
            return time + rand;
        }
    }
}
