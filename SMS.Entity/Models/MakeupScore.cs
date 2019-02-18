using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entity.Models
{
    /// <summary>
    /// 成绩录入类
    /// </summary>
    public class MakeupScore
    {
        /// <summary>
        /// 学生ID
        /// </summary>
        public string StudentId;
        /// <summary>
        /// 学生班级
        /// </summary>
        public string StudentClass;
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName;
        /// <summary>
        /// 成绩
        /// </summary>
        public int Achievement;
        /// <summary>
        /// 绩点
        /// </summary>
        public string Indexs;
        /// <summary>
        /// 学分
        /// </summary>
        public string Credit;
        /// <summary>
        /// 补考成绩
        /// </summary>
        public int MakeupExam;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark;
    }
}
