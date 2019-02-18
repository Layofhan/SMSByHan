using SMS.Data.Interface;
using SMS.Demo.Common;
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
    public class LoginService:ILoginContract
    {
        
        public IRepository<SMSUser, string> UserRepository { set; get; }
        public IRepository<SMSRole, string> RoleRepository { set; get; }
        public IRepository<SMSRoleUserMap, int> RoleUserMapRepository { set; get; }
        public IRepository<SMSRoleAuthorityMap, int> RoleAuthorityMapRepository { set; get; }
        public IQueryable<SMSUser> User
        {
            get { return UserRepository.Entities; }
        }
        public IQueryable<SMSRoleUserMap> RoleUserMap
        {
            get { return RoleUserMapRepository.Entities; }
        }
        public IQueryable<SMSRoleAuthorityMap> RoleAuthorityMap
        {
            get { return RoleAuthorityMapRepository.Entities; }
        }
        public IQueryable<SMSRole> Role
        {
            get { return RoleRepository.Entities; }
        }
        public DataResult Login(string Name,string Password,bool isChace)
        {
            var user = User.Where(s => s.Id == Name && s.Password == Password).FirstOrDefault();
            if(user!=null)
            {
                //查找用户对应的权限
                var authoritys = RoleUserMap.Where(c => c.UserId == user.Id).Join(RoleAuthorityMap, a => a.RoleId, b => b.RoleId, (a, b) => b.AuthorityId).ToArray();
                //查找用户对应的角色
                var role = RoleUserMap.Where(c => c.UserId == user.Id).FirstOrDefault();

                IdentityTicket ticket = new IdentityTicket();
                ticket.IsForceOffline = true;
                ticket.UserId = user.Id;
                ticket.UserName = user.Name;
                ticket.Role = role.RoleId;
                ticket.Authoritys = authoritys;
                //将身份信息存入Session和Cookie中
                IdentityManager.Init(ticket, isChace);

                return DataProcess.Success("登入成功");
            }
            else
            {
                return DataProcess.Failure("账号或密码错误，请检查后重新输入!");
            }
        }
    }
}
