using SMS.Data.Interface;
using SMS.Demo.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMS.Entity.Result;
using SMS.Entity.Models;
using SMS.Entity;

namespace SMS.Demo.Services
{
    public class InformationMaintainService : IInformationMaintainContract
    {
        public IRepository<SMSUser,string> UserRepository { get; set; }

        public IQueryable<SMSUser> User
        {
            get { return UserRepository.Entities; }
        }
        public DataResult ModificationPassword(string UserId, string NewPassword)
        {
            try
            {
                SMSUser user = UserRepository.GetByKey(UserId);
                user.Password = NewPassword;
                UserRepository.Update(user);
                return DataProcess.Success();
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }

        public DataResult SearchBaseInfor(string UserId)
        {
            try
            {
                var user = User.Where(a => a.Id == UserId).FirstOrDefault();
                if (user != null) return DataProcess.Success(user);
                else return DataProcess.Failure("对象为空!");
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }
    }
}
