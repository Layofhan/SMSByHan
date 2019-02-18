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
    public class OnlineCourseSelectionService : IOnlineCourseSelectionContract
    {
        public IRepository<SMSChoiceCourse, string> ChoiceCourseRepository { get; set; }
        public IRepository<SMSStudentBase, string> StudentBaseRepository { get; set; }
        public IRepository<SMSCourse, string> CourseRepository { get; set; }
        public IRepository<SMSTeacherCourseMap, int> TeacherCourseMapRepository { get; set; }
        public IRepository<SMSGrade, string> GradeRepository { get; set; }
        public IRepository<SMSCourseStudentMap, int> CourseStudentMapRepository { get; set; }
        public IQueryable<SMSChoiceCourse> ChoiceCourse
        {
            get { return ChoiceCourseRepository.Entities; }
        }
        public IQueryable<SMSStudentBase> StudentBase
        {
            get { return StudentBaseRepository.Entities; }
        }
        public IQueryable<SMSCourse> Course
        {
            get { return CourseRepository.Entities; }
        }
        public IQueryable<SMSTeacherCourseMap> TeacherCourseMap
        {
            get { return TeacherCourseMapRepository.Entities; }
        }
        public IQueryable<SMSGrade> Grade
        {
            get { return GradeRepository.Entities; }
        }
        public IQueryable<SMSCourseStudentMap> CourseStudentMap
        {
            get { return CourseStudentMapRepository.Entities; }
        }
        public DataResult GetChoiceCourseInformation(string stuid, int typeid)
        {
            try
            {
                //根据学生ID查询学生专业ID和班级ID
                var stu= StudentBase.Where(a => a.Id == stuid).Select(b => new {
                            MajorId = b.MajorId,
                            GradeId = b.GradeId
                         }).FirstOrDefault();
                if (stu == null) return DataProcess.Failure("This student is not in the basic information table, please contact the administrator");
                //根据判断条件，查找选课表中的信息
                var list = ChoiceCourse.Where(a => a.Enable && a.MajorId == stu.MajorId && a.GradeId == stu.GradeId && a.TypeId == typeid).ToList();
                if (list.Count == 0) return DataProcess.Failure("No course information!Please confirm the time.");
                //查询需要显示的内容
                //.Join(Grade, e => e.GradeId, f => f.Id, (e, f) => new {
                //     CourseId = e.CourseId,
                //     Capacity = e.Capacity,
                //     Surplus = e.Surplus,
                //     GradeId = e.GradeId,
                //     Name = e.Name,
                //     Credit = e.Credit,
                //     Indexs = e.Indexs
                // })
                var data = list.Join(Course, a => a.CourseId, b => b.Id, (a, b) => new
                {
                    CourseId = a.CourseId,
                    Capacity = a.Capacity,
                    Surplus = a.Surplus,
                    GradeId = a.GradeId,
                    Name = b.Name,
                    Credit = b.Credit,
                    Indexs = b.Indexs
                }).Join(TeacherCourseMap, c => c.CourseId, d => d.CourseId, (c, d) => new
                {
                    CourseId = c.CourseId,
                    GradeId = c.GradeId,
                    Name = c.Name,
                    Capacity = c.Capacity,
                    Surplus = c.Surplus,
                    Credit = c.Credit,
                    Indexs = c.Indexs,
                    OpenClassTime = d.OpenClassTime,
                    LearnTime = d.LearnTime,
                    Location = d.Location,
                    Campus = d.Campus
                }).ToList();
                if (data.Count == 0) return DataProcess.Failure("There is no teacher for this course, please inform the administrator");
                return DataProcess.Success(data);
            }
            catch (Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }

        public DataResult InsertChooseCourseToTable(int Id, SMSCourseStudentMap csm)
        {
            try
            {
                if(Id == 0||Id == 1||Id == 2||Id == 3||Id == 4)//通过这边的等于条件，可以决定哪几种类型的选课只能选择一门
                {
                   var list = CourseStudentMap.Where(a => a.StudentId == csm.StudentId).Join(ChoiceCourse, b => b.CourseId, c => c.CourseId, (b, c) => new
                                {
                                    StudentId = b.StudentId,
                                    CourseId = b.CourseId,
                                    GradeId = b.GradeId,
                                    TypeId = c.TypeId
                                }).Where(d => d.TypeId == Id).ToList();
                    if(list.Count > 0)
                    {
                        return DataProcess.Failure("There is only one course you can choose for your current class");
                    }
                }
                CourseStudentMapRepository.Insert(csm);
                return DataProcess.Success();
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
            throw new NotImplementedException();
        }

        public DataResult GetChoosedInformation(int Id, string studentid)
        {
            try
            {
                var list = CourseStudentMap.Where(a => a.StudentId == studentid).Select(b => new {
                                StudentId = b.StudentId,
                                CourseId = b.CourseId
                            }).Join(ChoiceCourse,c => c.CourseId,d => d.CourseId,(c,d) => new {
                                StudentId = c.StudentId,
                                CourseId = c.CourseId,
                                TypeId = d.TypeId,
                                GradeId = d.GradeId
                            }).Where(e => e.TypeId == Id).Join(Course,f => f.CourseId,g => g.Id,(f,g) => new {
                                CourseId = f.CourseId,
                                GradeId = f.GradeId,
                                Name = g.Name,
                                Credit = g.Credit,
                                Indexs = g.Indexs
                            }).Join(TeacherCourseMap, h => h.CourseId, i => i.CourseId, (h, i) => new
                            {
                                CourseId = h.CourseId,
                                GradeId = h.GradeId,
                                Name = h.Name,
                                Credit = h.Credit,
                                Indexs = h.Indexs,
                                OpenClassTime = i.OpenClassTime,
                                LearnTime = i.LearnTime,
                                Location = i.Location,
                                Campus = i.Campus
                            }).ToList();
                if(list.Count == 0)
                {
                    return DataProcess.Failure("No Data");
                }
                return DataProcess.Success(list);
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }
    }
}
