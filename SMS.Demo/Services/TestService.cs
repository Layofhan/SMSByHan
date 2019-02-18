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
    public class TestService:ITestContract
    {
        public IRepository<SMSUsers, int> WmsAbutentsRepository { set; get; }
        public IRepository<SMSUserDetail, int> TestJoinRepository { set; get; }
        public IQueryable<SMSUserDetail> TestJoin
        {
            get { return TestJoinRepository.Entities; }
        }
        public IQueryable<SMSUsers> WmsAbutents
        {
            get { return WmsAbutentsRepository.Entities; }
        }
        public DataResult InsertEntity(SMSUsers entity,bool isSave = true)
        {
            return WmsAbutentsRepository.Insert(entity, isSave);
        }

        public DataResult DelectEntity(SMSUsers entity, bool isSave = true)
        {
            return WmsAbutentsRepository.Delete(entity, isSave);
        }

        public SMSUsers SearchEntityById(object key)
        {
            return WmsAbutentsRepository.GetByKey(key);
        }
        public DataResult SearchAllEntity(int curr,int nums)
        {
            //return WmsAbutents.Select(a => a).ToList();
            try
            {
                List<SMSUsers> list = WmsAbutents.Select(a => a).ToList();
                if (curr != 0 && nums != 0)
                {
                    list = list.Skip(nums * (curr - 1)).Take(nums).ToList();
                }
                 //var s= WmsAbutents.Skip()
                 string aa = "1";
                if (list != null)
                {
                    foreach (SMSUsers l in list)
                    {
                        l.Name = "<a href='/DataTest/testnotice?id=" + aa + "'>" + l.Name + "</a>";
                    }
                    return DataProcess.Success(list);
                }
                else
                    return DataProcess.Failure("没有数据");
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }
        public DataResult UpdataEntity(SMSUsers entity, bool isSave = true)
        {
            return WmsAbutentsRepository.Update(entity, isSave);
        }

        public DataResult Login(SMSUsers entity)
        {

            var user = WmsAbutents.Where(s => s.Name == entity.Name && s.PassWord == entity.PassWord).FirstOrDefault();
            //var user = WmsAbutents.Join(TestJoin, a => a.Id, b => b.Id,(a,b) =>new { Name = a.Name,Qianmin=b.Qiannmin }).FirstOrDefault();
            if (user != null)
            {
                IdentityTicket ticket = new IdentityTicket();
                ticket.IsForceOffline = true;
                ticket.UserName = user.Name;
                //IdentityManager.Init(ticket);

                return DataProcess.Success("登入成功");
            }
            return DataProcess.Failure("账号密码错误");
        }

        public DataResult MenuLoading(string Auth)
        {
            if (Auth == null)
                return DataProcess.Failure("传入权限为空，网站内部错误，请联系管理员");
            var menu = TestJoin.Where(s => s.Qiannmin == Auth).ToList();
            //var menu = TestJoin.ToList();
            foreach(var a in menu)
            {
                var B = a.Id;
            }
            if (menu != null)
                return DataProcess.Success(menu);
            else
                return DataProcess.Failure("该权限使用功能");
        }
    }
}
