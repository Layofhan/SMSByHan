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
    public class BaseInformationManageService: IBaseInformationManageContract
    {
        public IRepository<SMSCurriculumNature, string> CurriculumNatureRepository { get; set; }
        public IRepository<SMSGrade, string> GradeRepository { get; set; }
        public IRepository<SMSBranch, string> BranchRepository { get; set; }
        public IRepository<SMSMajor, string> MajorRepository { get; set; }
        public IQueryable<SMSCurriculumNature> CurriculumNature
        {
            get { return CurriculumNatureRepository.Entities; }
        }
        public IQueryable<SMSGrade> Grade
        {
            get { return GradeRepository.Entities; }
        }
        public IQueryable<SMSBranch> Branch
        {
            get { return BranchRepository.Entities; }
        }
        public IQueryable<SMSMajor> Major
        {
            get { return MajorRepository.Entities; }
        }
        public DataResult NatureIdIsRepeat(string id)
        {
            try
            {
                var nature = CurriculumNature.Where(a => a.Id == id).FirstOrDefault();
                if (nature == null)
                {
                    return DataProcess.Success();
                }
                else
                    return DataProcess.Failure("Repeat ID! Please pay attention to maintenance.");
            }
            catch (Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }

        }
        public DataResult InsertNatureToTable(SMSCurriculumNature nature)
        {
            try
            {
                CurriculumNatureRepository.Insert(nature);
                return DataProcess.Success();
            }
            catch (Exception ex)
            {
                return DataProcess.Failure();
            }
        }

        public DataResult InsertGradeToTable(SMSGrade nature)
        {
            try
            {
                //判断ID是否重复
                if (Grade.Where(a => a.Id == nature.Id).FirstOrDefault() == null)
                {
                    GradeRepository.Insert(nature);
                    return DataProcess.Success();
                }
                else
                {
                    return DataProcess.Failure("Repeat ID! Please pay attention to maintenance.");
                }
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }
        public DataResult InsertBranchToTable(SMSBranch brach)
        {
            try
            {
                //判断ID是否重复
                if (Branch.Where(a => a.Id == brach.Id).FirstOrDefault() == null)
                {
                    BranchRepository.Insert(brach);
                    return DataProcess.Success();
                }
                else
                    return DataProcess.Failure("Repeat ID! Please pay attention to maintenance.");
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }

        public DataResult GetBranchInformation()
        {
            try
            {
                var list = Branch.Select(a => new {
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

        public DataResult InsertMajorToTable(SMSMajor major)
        {
            try
            {
                if(Major.Where(a => a.Id == major.Id).FirstOrDefault() == null)
                {
                    MajorRepository.Insert(major);
                    return DataProcess.Success();
                }
                else
                {
                    return DataProcess.Failure("Repeat ID!Please pay attention to maintenance.");
                }
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }

        public DataResult GetNatureInformation()
        {
            try
            {
                var list = CurriculumNature.Select(a => new {
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
    }
}
