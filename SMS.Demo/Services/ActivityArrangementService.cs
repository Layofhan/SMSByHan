using SMS.Demo.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMS.Entity.Models;
using SMS.Entity.Result;
using SMS.Data.Interface;
using SMS.Entity;

namespace SMS.Demo.Services
{
    public class ActivityArrangementService : IActivityArrangementContract
    {
        public IRepository<SMSChoiceCourse, string> CurriculumNatureRepository { get; set; }
        public IQueryable<SMSChoiceCourse> ChoiceCourse
        {
            get { return CurriculumNatureRepository.Entities; }
        }
        public DataResult InsertChoiceCourseToTabl(SMSChoiceCourse course)
        {
            try
            {
                if(ChoiceCourse.Where(a => a.Id == course.Id).FirstOrDefault() == null)
                {
                    CurriculumNatureRepository.Insert(course);
                    return DataProcess.Success();
                }
                return DataProcess.Failure("Id duplicate, please pay attention to maintenance");
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
            throw new NotImplementedException();
        }
    }
}
