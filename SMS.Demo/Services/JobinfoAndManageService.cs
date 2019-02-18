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
using SMS.Demo.Common;

namespace SMS.Demo.Services
{
    public class JobinfoAndManageService : IJobinfoAndManageContract
    {
        public IRepository<SMSCourseStudentMap,int> CourseStudentMapRepository { get; set; }

        public IRepository<SMSStudentTask,int> StudentTaskRepository { get; set; }

        public IRepository<SMSUser, string> UserRepository { get; set; }

        public IRepository<SMSCourse, string> CourseRepository { get; set; }

        public IRepository<SMSStudentWork, int> StudentWorkRepository { get; set; }

        public IRepository<SMSTeacherCourseMap,int> TeacherCourseMapRepository { get; set; }

        public IRepository<SMSStudentBase,string> StudentBaseRepository { get; set; }

        public IQueryable<SMSCourseStudentMap> CourseStudentMap
        {
            get { return CourseStudentMapRepository.Entities; }
        }
        public IQueryable<SMSStudentTask> StudentTask
        {
            get { return StudentTaskRepository.Entities; }
        }
        public IQueryable<SMSUser> User
        {
            get { return UserRepository.Entities; }
        }
        public IQueryable<SMSCourse> Course
        {
            get { return CourseRepository.Entities; }
        }
        public IQueryable<SMSStudentWork> StudentWork
        {
            get { return StudentWorkRepository.Entities; }
        }
        public IQueryable<SMSTeacherCourseMap> TeacherCourseMap
        {
            get { return TeacherCourseMapRepository.Entities; }
        }
        public IQueryable<SMSStudentBase> StudentBase
        {
            get { return StudentBaseRepository.Entities; }
        }
        public DataResult GetWorkTask(string Id)
        {
            try
            {
                var list = CourseStudentMap.Where(a => a.StudentId == Id).Join(StudentTask, b => b.CourseId, c => c.CourseId, (b, c) => new
                {
                    StudentId = b.StudentId,
                    TeacherId = c.TeacherId,
                    StartTime = c.StartTime,
                    EndTime = c.EndTime,
                    TaskId = c.TaskId,
                    Content = c.Content,
                    CourseId = c.CourseId
                }).Where(m => m.StartTime<=DateTime.Now && m.EndTime>=DateTime.Now).Join(Course,f => f.CourseId,h => h.Id,(f,h) => new {
                    StudentId = f.StudentId,
                    TeacherId = f.TeacherId,
                    StartTime = f.StartTime,
                    EndTime = f.EndTime,
                    TaskId = f.TaskId,
                    Content = f.Content,
                    CourseId = f.CourseId,
                    CourseName = h.Name
                }).Join(User, d => d.TeacherId, e => e.Id, (d, e) => new
                {
                    StudentId = d.StudentId,
                    TeacherId = d.TeacherId,
                    SendTime = d.StartTime,
                    EndTime = d.EndTime,
                    TaskId = d.TaskId,
                    SendContext = d.Content,
                    CourseId = d.CourseId,
                    CourseName = d.CourseName,
                    TeacherName = e.Name
                }).ToList();
                return DataProcess.Success(list);
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }

        public DataResult SaveWorkInfor(string TaskId, string StudentId, string TeacherId, string CourseId,string FileName)
        {
            try
            {
                SMSStudentWork studentwork = new SMSStudentWork();
                studentwork.FileName = FileName;
                studentwork.UpTime = DateTime.Now;
                studentwork.CourseId = CourseId;
                studentwork.TaskId = TaskId;
                studentwork.State = 1;
                studentwork.TeacherId = TeacherId;
                studentwork.StudentId = StudentId;
                studentwork.WorkId = RandomNumber.GenerateNum();
                studentwork.Score = 0;
                StudentWorkRepository.Insert(studentwork);
                return DataProcess.Success();
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }

        public DataResult UpdateWorkInfor(string TaskId,string StudentId,string FileName)
        {
            try
            {
                var workinfor = StudentWork.Where(a => a.StudentId == StudentId && a.TaskId == TaskId).FirstOrDefault();
                workinfor.FileName = FileName;
                workinfor.UpTime = DateTime.Now;
                workinfor.State = 1;
                StudentWorkRepository.Update(workinfor);
                return DataProcess.Success("作业信息更新成功");
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }

        public string WorkInforHasExist(string TaskId,string StudentId)
        {
            try
            {
                var workinfor = StudentWork.Where(a => a.TaskId == TaskId && a.StudentId == StudentId).FirstOrDefault();
                if (workinfor != null)
                {
                    return workinfor.FileName;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                return "faile";
            }
        }

        public DataResult GetFileNameById(string WorkId)
        {
            try
            {
                var workinfo = StudentWork.Where(a => a.WorkId == WorkId).FirstOrDefault();
                if (workinfo != null)
                {
                    return DataProcess.Success(workinfo.FileName);
                }
                else
                    return DataProcess.Failure("不存在该作业信息唯一标识,请检查其正确性");
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }

        public DataResult GetWorkInformation(string StudentId,int page,int limit)
        {
            try
            {
                if (page == 0 && limit == 0)
                {
                    var count = StudentWork.Where(m => m.StudentId == StudentId).Count();
                    return DataProcess.Success(count);
                }
                var list = StudentWork.Where(a => a.StudentId == StudentId).Join(Course,c => c.CourseId,d =>d.Id,(c,d) => new {
                    StudentId = c.StudentId,
                    CourseName = d.Name,
                    CourseId = d.Id,
                    FileName = c.FileName,
                    SubmitTime = c.UpTime,
                    State = c.State,
                    TeacherId = c.TeacherId
                }).Join(User,e => e.TeacherId,f => f.Id,(e,f) => new {
                    StudentId = e.StudentId,
                    CourseName = e.CourseName,
                    CourseId = e.CourseId,
                    FileName = e.FileName,
                    SubmitTime = e.SubmitTime,
                    State = e.State,
                    TeacherId = e.TeacherId,
                    TeacherName = f.Name
                }).OrderBy(g => g.StudentId).Skip(limit * (page - 1)).Take(limit).ToList();

                if (list != null)
                {
                    return DataProcess.Success(list);
                }
                else
                {
                    return DataProcess.Failure();
                }
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }

        public DataResult GetCourseInformation(string TeacherId)
        {
            try
            {
                var list = TeacherCourseMap.Where(a => a.TeacherId == TeacherId && a.Enabled == true).Join(CourseStudentMap,b => b.CourseId,c =>c.CourseId,(b,c) => new {
                    CourseId  = b.CourseId
                }).Join(Course,d => d.CourseId,e => e.Id,(d,e) => new {
                    CourseId = d.CourseId,
                    CourseName = e.Name
                }).Distinct().ToList();
                return DataProcess.Success(list);
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }

        public DataResult GetClassInformation(string TeacherId, string CourseId)
        {
            try
            {
                var list = TeacherCourseMap.Where(a => a.TeacherId == TeacherId && a.Enabled == true && a.CourseId == CourseId).Join(CourseStudentMap, b => b.CourseId, c => c.CourseId, (b, c) => new {
                    StudentId = c.StudentId
                }).Join(StudentBase, f => f.StudentId, g => g.Id, (f, g) => new {
                    Class = g.Class
                }).Distinct().ToList();
                return DataProcess.Success(list);
            }
            catch (Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }

        public DataResult GetWorkForTeachCheck(string CouserId,string ClassId)
        {
            try
            {
                var list = StudentBase.Where(a => a.Class == ClassId).Select(b => new
                {
                    StudentId = b.Id
                }).Join(User,h => h.StudentId,j => j.Id,(h,j) => new {
                    StudentId = h.StudentId,
                    StudentName = j.Name
                }).Join(StudentWork, c => c.StudentId, d => d.StudentId, (c, d) => new
                {
                    StudentId = c.StudentId,
                    StudentName = c.StudentName,
                    CourseId = d.CourseId,
                    FileName = d.FileName,
                    UpTime = d.UpTime,
                    Score = d.Score,
                    CheckTime = d.CheckTime,
                    State = d.State,
                    WorkId = d.WorkId
                }).Where(e => e.CourseId == CouserId).ToList();
                return
                     DataProcess.Success(list);
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }

        public DataResult UpdateWorkScore(string WorkId, string CourseId, int Score, string StudentId)
        {
            try
            {
                SMSStudentWork studentwork = new SMSStudentWork();
                studentwork = StudentWork.Where(a => a.WorkId == WorkId && a.CourseId == CourseId&& a.StudentId == StudentId).FirstOrDefault();
                studentwork.Score = Score;
                studentwork.State = 2;
                StudentWorkRepository.Update(studentwork);
                return DataProcess.Success();
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }

        public DataResult PublishWork(DateTime StartTime, DateTime EndTime, string TeacherId,string CourseId, string Require, string Content)
        {
            try
            {
                SMSStudentTask studenttask = new SMSStudentTask();
                studenttask.TeacherId = TeacherId;
                studenttask.SendTime = DateTime.Now;
                studenttask.StartTime = StartTime;
                studenttask.EndTime = EndTime;
                studenttask.TaskId = RandomNumber.GenerateNum();
                studenttask.CourseId = CourseId;
                studenttask.State = 0;
                studenttask.Require = Require;
                studenttask.Content = Content;

                StudentTaskRepository.Insert(studenttask);
                return DataProcess.Success();
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }

        public DataResult GetPublishedTask(string TeacherId,int limit,int page)
        {
            try
            {
                 var list = StudentTask.Where(a => a.TeacherId == TeacherId).OrderByDescending(e => e.SendTime).Skip(limit * (page - 1)).Take(limit).Select(b => new {
                    CourseId = b.CourseId,
                    Require = b.Require,
                    Content = b.Content,
                    SendTime = b.SendTime,
                    StartTime = b.StartTime,
                    EndTime = b.EndTime,
                    State = b.State
                }).Join(Course,c => c.CourseId,d => d.Id,(c,d) => new
                {
                    CourseId = c.CourseId,
                    Require = c.Require,
                    Content = c.Content,
                    SendTime = c.SendTime,
                    StartTime = c.StartTime,
                    EndTime = c.EndTime,
                    State = c.State,
                    CourseName = d.Name
                }).ToList();

                //将匿名类型转化为强类型
                List<TeacherPublishTasks> teacherpublishtasks = new List<TeacherPublishTasks>();
                TeacherPublishTasks tpt;
                foreach (var li in list)
                {
                    tpt = new TeacherPublishTasks();
                    tpt.CourseId = li.CourseId;
                    tpt.Require = li.Require;
                    tpt.Content = li.Content;
                    tpt.SendTime = li.SendTime;
                    tpt.StartTime = li.StartTime;
                    tpt.EndTime = li.EndTime;
                    tpt.State = li.State;
                    tpt.CourseName = li.CourseName;

                    teacherpublishtasks.Add(tpt);
                }

                //获取全部数量
                var count = StudentTask.Where(a => a.TeacherId == TeacherId).Count();
                TabularData<TeacherPublishTasks> tabulardata = new TabularData<TeacherPublishTasks>
                {
                    List = teacherpublishtasks,
                    AllCount = count
                };
                return DataProcess.Success(tabulardata);
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }


    }
}
