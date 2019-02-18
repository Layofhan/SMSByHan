using SMS.Data.Interface;
using SMS.Demo.Contracts;
using SMS.Entity;
using SMS.Entity.Models;
using SMS.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Demo.Services
{
    public class ResourceManagementService : IResourceManagementContract
    {
        public IRepository<SMSCourse, string> CourseRepository { get; set; }
        public IRepository<SMSRoleUserMap, int> RoleUserMapRepository { get; set; }
        public IRepository<SMSUser, string> UserRepository { get; set; }
        public IRepository<SMSTeacherCourseMap,int> TeacherCourseMapRepository { get; set; }
        public IRepository<SMSMajor, string> MajorRepository { get; set; }
        public IRepository<SMSGrade, string> GradeRepository { get; set; }
        public IRepository<SMSStudentBase, string> StudentBaseRepository { get; set; }
        public IRepository<SMSCourseStudentMap, int> CourseStudentMapRepository { get; set; }
        public IQueryable<SMSCourse> Course
        {
            get { return CourseRepository.Entities; }
        }
        public IQueryable<SMSRoleUserMap> RoleUserMap
        {
            get { return RoleUserMapRepository.Entities; }
        }
        public IQueryable<SMSUser> User
        {
            get { return UserRepository.Entities; }
        }
        public IQueryable<SMSTeacherCourseMap> TeacherCourseMap
        {
            get { return TeacherCourseMapRepository.Entities; }
        }
        public IQueryable<SMSMajor> Major
        {
            get { return MajorRepository.Entities; }
        }
        public IQueryable<SMSStudentBase> StudentBase
        {
            get { return StudentBaseRepository.Entities; }
        }
        public IQueryable<SMSGrade> Grade
        {
            get { return GradeRepository.Entities; }
        }
        public DataResult InsertCourseToTable(SMSCourse course)
        {
            try
            {
                if (Course.Where(a => a.Id == course.Id).FirstOrDefault() == null)
                {
                    CourseRepository.Insert(course);
                    return DataProcess.Success();
                }
                else
                    return DataProcess.Failure("Repeat ID!Please pay attention to maintenance.");
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }

        public DataResult GetCourseInfrormation()
        {
            try
            {
                var list = Course.Select(a => new
                {
                    Id = a.Id,
                    Name = a.Name
                }).ToList();
                return DataProcess.Success(list);
            }
            catch (Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }

        public DataResult GetTeacherInformation()
        {
            try
            {
                var list = RoleUserMap.Where(a => a.RoleId == "teacher").Join(User, b => b.UserId, c => c.Id, (b, c) => new
                {
                    Id = b.UserId,
                    Name = c.Name
                }).ToList();
                if (list == null) return DataProcess.Failure("No Teacher Information");
                return DataProcess.Success(list);
            }
            catch (Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }

        public DataResult InsertCurriculumToTable(SMSTeacherCourseMap teachercourse)
        {
            try
            {
                TeacherCourseMapRepository.Insert(teachercourse);
                return DataProcess.Success();
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }
        public DataResult GetProgressInformation()
        {
            try
            {
                var list = Major.Select(a => new {
                    Id = a.Id,
                    Name = a.Name
                }).ToList();
                return DataProcess.Success(list);
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }
        
        public DataResult GetGradeInformation()
        {
            try
            {
                var list = Grade.Select(a => new {
                    Id = a.Id,
                    Name = a.Id
                }).ToList();
                return DataProcess.Success(list);
            }
            catch (Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }
        public DataResult InsertStudentCourseToTable(string progressid, string courseid, string gradeid,string remark)
        {
            try
            {
                var list = StudentBase.Where(a => a.MajorId == progressid && a.GradeId == gradeid).Select(b => new { studentid = b.Id }).ToList();
                if(list == null)
                {
                    return DataProcess.Failure("No Student");
                }
                SMSCourseStudentMap scm;
                foreach (var li in list)
                {
                    scm = new SMSCourseStudentMap();
                    scm.CourseId = courseid;
                    scm.StudentId = li.studentid;
                    scm.Remark = remark;
                    scm.MinorRepairMark = 0;
                    scm.ReworkMark = 0;
                    scm.GradeId = gradeid;
                    scm.MidEvaluation = 0;
                    scm.FinalEvaluation = 0;
                    CourseStudentMapRepository.Insert(scm);
                }
                return DataProcess.Success();
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }
    }
}
