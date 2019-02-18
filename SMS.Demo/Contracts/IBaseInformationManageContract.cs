using SMS.Data.Interface;
using SMS.Entity.Models;
using SMS.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Demo.Contracts
{
    public interface IBaseInformationManageContract : IScopeDependency, IDependency
    {
        /// <summary>
        /// 查询ID是否重复
        /// </summary>
        /// <param name="id">课程性质ID</param>
        /// <returns>True则重复，反而反之</returns>
        DataResult NatureIdIsRepeat(string id);
        /// <summary>
        /// 将信息插入课程性质表中
        /// </summary>
        /// <returns></returns>
        DataResult InsertNatureToTable(SMSCurriculumNature nature);
        /// <summary>
        /// 将信息插入年级表中
        /// </summary>
        /// <param name="nature"></param>
        /// <returns></returns>
        DataResult InsertGradeToTable(SMSGrade nature);
        /// <summary>
        /// 将信息插入分院信息表
        /// </summary>
        /// <param name="brach"></param>
        /// <returns></returns>
        DataResult InsertBranchToTable(SMSBranch brach);
        /// <summary>
        /// 获取分院的信息
        /// </summary>
        /// <returns></returns>
        DataResult GetBranchInformation();
        /// <summary>
        /// 获取课程性质信息
        /// </summary>
        /// <returns></returns>
        DataResult GetNatureInformation();
        /// <summary>
        /// 将信息插入专业信息表中
        /// </summary>
        /// <param name="major"></param>
        /// <returns></returns>
        DataResult InsertMajorToTable(SMSMajor major);
    }
}
