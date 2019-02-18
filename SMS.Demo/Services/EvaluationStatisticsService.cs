using SMS.Demo.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMS.Entity.Models;
using SMS.Entity.Result;
using SMS.Entity;
using SMS.Data.Interface;

namespace SMS.Demo.Services
{
    public class EvaluationStatisticsService : IEvaluationStatisticsContract
    {
        public IRepository<SMSEvaluation, string> EvaluationRepository { set; get; }

        public IRepository<SMSCourseStudentMap, int> CourseStudentMapRepository { set; get; }
        public IRepository<SMSTeacherCourseMap, int> TeacherCourseMapRepository { set; get; }
        public IRepository<SMSEvaluationTeaching, int> EvaluationTeachingRepository { set; get; }
        public IRepository<SMSUser, string> UserRepository { set; get; }

        public IRepository<SMSCourse, string> CourseRepository { set; get; }
        public IQueryable<SMSUser> User
        {
            get { return UserRepository.Entities; }
        }
        public IQueryable<SMSEvaluation> Evaluation
        {
            get { return EvaluationRepository.Entities; }
        }
        public IQueryable<SMSCourseStudentMap> CourseStudentMap
        {
            get { return CourseStudentMapRepository.Entities; }
        }
        public IQueryable<SMSCourse> Course
        {
            get { return CourseRepository.Entities; }
        }
        public IQueryable<SMSTeacherCourseMap> TeacherCourseMap
        {
            get { return TeacherCourseMapRepository.Entities; }
        }
        public IQueryable<SMSEvaluationTeaching> EvaluationTeaching
        {
            get { return EvaluationTeachingRepository.Entities; }
        }
        public DataResult InsertInformationToTable(SMSEvaluation entity)
        {
            try
            {
                var Id = Evaluation.Where(a => a.Id == entity.Id).FirstOrDefault();
                if(Id != null) { return DataProcess.Failure("The Id cannot be repeated"); }
                EvaluationRepository.Insert(entity);
                return DataProcess.Success();
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }

        public DataResult GetSendElective()
        {
            try
            {
                var list = Evaluation.Select(a => new
                            {
                                Id = a.Id,
                                Title = a.Title,
                                PublisherId = a.PublisherId,
                                Time = a.Time,
                                State = a.State
                            }).ToList();
                if (list.Count == 0) return DataProcess.Failure("No Data");
                return DataProcess.Success(list);
            }
            catch (Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }

        public DataResult ChangeElectiveState(int s,string id)
        {
            try
            {
                SMSEvaluation eva = Evaluation.Where(a => a.Id == id).FirstOrDefault();   
                       
                if (s == 0)
                {
                    if (eva.State == 0) return DataProcess.Failure("State had close");
                    eva.State = 0;
                }
                else
                {
                    if (eva.State == 1) return DataProcess.Failure("State had open");
                    eva.State = 1;
                }
                EvaluationRepository.Update(eva);
                return DataProcess.Success("Successful operation");
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }
        public DataResult GetStuElectiveInformation(string id)
        {
            try
            {
                var eva = Evaluation.Where(a => a.State == 1 && a.FunctionType == 0).FirstOrDefault();
                if (eva == null) return DataProcess.Failure("No course information is available");
                if (eva.TimeType == 0)
                {
                    var list =CourseStudentMap.Where(a => a.StudentId == id && a.MidEvaluation == 0).Select(b => new
                    {
                        CourseId = b.CourseId,
                        StudentId = b.StudentId,
                        MidEvaluation = b.MidEvaluation
                    }).Join(Course, c => c.CourseId, d => d.Id, (c, b) => new
                    {
                        CourseId = c.CourseId,
                        StudentId = c.StudentId,
                        MidEvaluation = c.MidEvaluation,
                        Name = b.Name
                    }).Join(TeacherCourseMap, e => e.CourseId, f => f.CourseId, (e, f) => new
                    {
                        CourseId = e.CourseId,
                        StudentId = e.StudentId,
                        MidEvaluation = e.MidEvaluation,
                        Name = e.Name,
                        TeacherId = f.TeacherId
                    }).Join(User, g => g.TeacherId, h => h.Id, (g, h) => new
                    {
                        CourseId = g.CourseId,
                        StudentId = g.StudentId,
                        MidEvaluation = g.MidEvaluation,
                        CourseName = g.Name,
                        TeacherId = g.TeacherId,
                        TeacherName = h.Name,
                        EvaluationId = eva.Id,
                        TimeType = eva.TimeType
                    }).ToList();
                    if (list.Count == 0) return DataProcess.Failure("No Couse  Information");
                    return DataProcess.Success(list);
                }
                else
                {
                    var list = CourseStudentMap.Where(a => a.StudentId == id && a.FinalEvaluation == 0 ).Select(b => new
                    {
                        CourseId = b.CourseId,
                        StudentId = b.StudentId,
                        MidEvaluation = b.MidEvaluation
                    }).Join(Course, c => c.CourseId, d => d.Id, (c, b) => new
                    {
                        CourseId = c.CourseId,
                        StudentId = c.StudentId,
                        MidEvaluation = c.MidEvaluation,
                        Name = b.Name
                    }).Join(TeacherCourseMap, e => e.CourseId, f => f.CourseId, (e, f) => new
                    {
                        CourseId = e.CourseId,
                        StudentId = e.StudentId,
                        MidEvaluation = e.MidEvaluation,
                        Name = e.Name,
                        TeacherId = f.TeacherId
                    }).Join(User, g => g.TeacherId, h => h.Id, (g, h) => new
                    {
                        CourseId = g.CourseId,
                        StudentId = g.StudentId,
                        MidEvaluation = g.MidEvaluation,
                        CourseName = g.Name,
                        TeacherId = g.TeacherId,
                        TeacherName = h.Name,
                        EvaluationId = eva.Id,
                        TimeType = eva.TimeType
                    }).ToList();
                    if (list.Count == 0) return DataProcess.Failure("No Data");
                    return DataProcess.Success(list);
                }
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }

        public DataResult InsertStuScoreToTable(SMSEvaluationTeaching entity)
        {
            try
            {
                var eva = Evaluation.Where(a => a.Id == entity.EvaluationId).FirstOrDefault();
                var course = CourseStudentMap.Where(a => a.CourseId == entity.CourseId && a.StudentId == entity.StudentId).FirstOrDefault();
                if (eva.TimeType == 0)
                {
                    course.MidEvaluation = 1;
                }
                if(eva.TimeType == 1)
                {
                    course.FinalEvaluation = 1;
                }
                CourseStudentMapRepository.Update(course,false);
                EvaluationTeachingRepository.Insert(entity);
                return DataProcess.Success();
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }

        public DataResult GetTeachEachMark(string id)
        {
            try
            {
                var eva = Evaluation.Where(a => a.State == 1 && a.FunctionType == 0).FirstOrDefault();
                var list = TeacherCourseMap.Where(a => a.TeacherId == id).Join(Course,f => f.CourseId, g => g.Id,(f,g) => new {
                                CourseId = f.CourseId,
                                CourseName = g.Name
                            }).Join(EvaluationTeaching,b => b.CourseId,c => c.CourseId,(b,c) => new {
                                EvaluationId = c.EvaluationId,
                                CourseId = b.CourseId,
                                Name = b.CourseName,
                                Toltal = c.AspectOne + c.AspectTwo + c.AspectThree + c.AspectFour
                            }).Where(h => h.EvaluationId == eva.Id).GroupBy(d => new {
                                d.CourseId,
                            }).Select(e => new {
                                name = e.FirstOrDefault().Name,
                                value = e.Sum(h => h.Toltal),
                                CouseId = e.FirstOrDefault().CourseId
                            }).ToList();
                return DataProcess.Success(list);
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
            throw new NotImplementedException();
        }
        public DataResult GetMarkByCourseId(string courseid)
        {
            try
            {
                var list = EvaluationTeaching.GroupBy(a => new {
                    a.CourseId
                }).Select(b => new
                {
                    CourseId = b.FirstOrDefault().CourseId,
                    Asp1 = b.Sum(c => c.AspectOne),
                    Asp2 = b.Sum(c => c.AspectTwo),
                    Asp3 = b.Sum(c => c.AspectThree),
                    Asp4 = b.Sum(c => c.AspectFour)
                }).Where(d => d.CourseId == courseid).FirstOrDefault();
                return DataProcess.Success(list);
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message); ;
            }
        }
        public DataResult GetMarkForAdminQuery(string EvaluationId)
        {
            try
            {
                var list = EvaluationTeaching.Where(a => a.EvaluationId == EvaluationId).Join(TeacherCourseMap, b => b.CourseId, c => c.CourseId, (b, c) => new
                {
                    EvaluationId = b.EvaluationId,
                    TeacherId = c.TeacherId,
                    Mark = b.AspectOne + b.AspectTwo + b.AspectThree + b.AspectFour
                }).Join(Evaluation,z => z.EvaluationId,y => y.Id,(z,y) => new {
                    Title = y.Title,
                    TeacherId = z.TeacherId,
                    Mark = z.Mark
                }).Join(User, d => d.TeacherId, e => e.Id, (d, e) => new
                {
                    Title = d.Title,
                    TeacherId = d.TeacherId,
                    Mark = d.Mark,
                    TeacherName = e.Name
                }).GroupBy(f => new
                {
                    f.TeacherId
                }).Select(g => new
                {
                    Title = g.FirstOrDefault().Title,
                    Name = g.FirstOrDefault().TeacherName,
                    Id = g.FirstOrDefault().TeacherId,
                    Mark = g.Sum(h => h.Mark)
                }).ToList();
                return DataProcess.Success(list);
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }
        public DataResult GetActivity()
        {
            try
            {
                var list = Evaluation.Select(a => new
                {
                    Id = a.Id,
                    Title = a.Title
                }).ToList();
                return DataProcess.Success(list);
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }
    }
}
