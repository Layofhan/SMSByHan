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
    public class HomeService:IHomeContract
    {
        public IRepository<SMSUser, string> UserRepository { get;set;  }
        public IQueryable<SMSUser> User
        {
            get { return UserRepository.Entities; }
        }
        public DataResult GetUserMessage(string id)
        {
            try
            {
                var name = User.Where(a => a.Id == id).FirstOrDefault();
                return DataProcess.Success(name);
            }
            catch(Exception ex)
            {
                return DataProcess.Failure("Have Some Problem");
            }

        }

        public DataResult GetRoast()
        {
            return null;
        }

        public DataResult SetRoast(string Content)
        {
            return null;
        }
    }
}
