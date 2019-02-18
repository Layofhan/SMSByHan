using SMS.Demo.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMS.Entity.Result;
using SMS.Data.Interface;
using SMS.Entity.Models;
using SMS.Entity;

namespace SMS.Demo.Services
{
    public class PerformanceManagementService : IPerformanceManagementContract
    {
        public IRepository<SMSCourseStudentMap,int> CourseStudentMapRepostitory { get; set; }
        public IRepository<SMSStudentBase,string> StudentBaseRepository { get; set; }
        public IRepository<SMSUser,string> UserRepository { get; set; }
        public IQueryable<SMSCourseStudentMap> CourseStudentMap
        {
            get { return CourseStudentMapRepostitory.Entities; }
        }
        public IQueryable<SMSStudentBase> StudentBase
        {
            get { return StudentBaseRepository.Entities; }
        }
        public IQueryable<SMSUser> User
        {
            get { return UserRepository.Entities; }
        }
        public DataResult GetStudentGrade(string CourseId, string ClassId, int limit, int page)
        {
            try
            {
                //存储整理好后的数据
                TabularData<MakeupScore> tabulardata;
                //获取数量
                int count = CourseStudentMap.Where(a => a.CourseId == CourseId).Join(StudentBase, b => b.StudentId, c => c.Id, (b, c) => new
                {
                    StudentId = b.StudentId,
                    StudentClass = c.Class,
                    Achievement = b.Achievement,
                    Indexs = b.Indexs,
                    Credit = b.Credit,
                    MakeupExam = b.MakeupExam,
                    Remark = b.Remark
                }).Join(User, d => d.StudentId, e => e.Id, (d, e) => new
                {
                    StudentId = d.StudentId,
                    StudentClass = d.StudentClass,
                    StudentName = e.Name,
                    Achievement = d.Achievement,
                    Indexs = d.Indexs,
                    Credit = d.Credit,
                    MakeupExam = d.MakeupExam,
                    Remark = d.Remark
                }).Count();
                if (count > 0)
                {
                    //获取数据
                    var list = CourseStudentMap.Where(a => a.CourseId == CourseId).Join(StudentBase, b => b.StudentId, c => c.Id, (b, c) => new
                    {
                        StudentId = b.StudentId,
                        StudentClass = c.Class,
                        Achievement = b.Achievement,
                        Indexs = b.Indexs,
                        Credit = b.Credit,
                        MakeupExam = b.MakeupExam,
                        Remark = b.Remark
                    }).Join(User, d => d.StudentId, e => e.Id, (d, e) => new
                    {
                        StudentId = d.StudentId,
                        StudentClass = d.StudentClass,
                        StudentName = e.Name,
                        Achievement = d.Achievement,
                        Indexs = d.Indexs,
                        Credit = d.Credit,
                        MakeupExam = d.MakeupExam,
                        Remark = d.Remark
                    }).OrderBy(f => f.StudentId).Skip(limit * (page - 1)).Take(limit).ToList();

                    //将匿名类型转化为强类型
                    List<MakeupScore> makeupscore = new List<MakeupScore>();
                    MakeupScore ms;
                    foreach (var li in list)
                    {
                        ms = new MakeupScore();
                        ms.StudentId = li.StudentId;
                        ms.StudentClass = li.StudentClass;
                        ms.StudentName = li.StudentName;
                        ms.Achievement = li.Achievement;
                        ms.Indexs = li.Indexs;
                        ms.Credit = li.Credit;
                        ms.MakeupExam = li.MakeupExam;

                        makeupscore.Add(ms);
                    }
                    tabulardata = new TabularData<MakeupScore>
                    {
                        List = makeupscore,
                        AllCount = count
                    };
                }
                else
                {
                    tabulardata = new TabularData<MakeupScore>
                    {
                        List = null,
                        AllCount = 0
                    };
                }
                return DataProcess.Success(tabulardata);
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }

        public DataResult UpdateScore(int Grade, string Creditx, string Indexs, string CourseId, string StudentId)
        {
            try
            {
                var coursestudent = CourseStudentMap.Where(a => a.CourseId == CourseId && a.StudentId == StudentId).FirstOrDefault();
                coursestudent.Achievement = Grade;
                coursestudent.Credit = Creditx;
                coursestudent.Indexs = Indexs;
                CourseStudentMapRepostitory.Update(coursestudent);
                return DataProcess.Success();
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }

        public DataResult UpdateIndexs(string Indexs, string CourseId, string StudentId)
        {
            try
            {
                var coursestudent = CourseStudentMap.Where(a => a.CourseId == CourseId && a.StudentId == StudentId).FirstOrDefault();
                coursestudent.Indexs = Indexs;
                return DataProcess.Success();
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }

        public DataResult UpdateCredit(string Credit, string CourseId, string StudentId)
        {
            try
            {
                var coursestudent = CourseStudentMap.Where(a => a.CourseId == CourseId && a.StudentId == StudentId).FirstOrDefault();
                coursestudent.Credit = Credit;
                return DataProcess.Success();
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }

        public DataResult UpdateMakeupGrade(int MakeupGrade, string CourseId, string StudentId)
        {
            try
            {
                var coursestudent = CourseStudentMap.Where(a => a.CourseId == CourseId && a.StudentId == StudentId).FirstOrDefault();
                coursestudent.MakeupExam = MakeupGrade;
                return DataProcess.Success();
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }

        public DataResult UpdateRemark(string Remark, string CourseId, string StudentId)
        {
            try
            {
                var coursestudent = CourseStudentMap.Where(a => a.CourseId == CourseId && a.StudentId == StudentId).FirstOrDefault();
                coursestudent.Remark = Remark;
                return DataProcess.Success();
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }
    }
}
