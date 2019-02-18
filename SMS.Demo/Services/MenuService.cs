using SMS.Demo.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMS.Entity.Result;
using SMS.Entity.Models;
using SMS.Data.Interface;
using SMS.Demo.Common;
using SMS.Entity;
using SMS.Common;

namespace SMS.Demo.Services
{
    public class MenuService : IMenuContract
    {
        public IRepository<SMSRoleMenuMap, int> RoleMenuMapRepository { set; get; }
        public IRepository<SMSMenu,string> MenuRepository { get; set; }

        public IRepository<SMSChildMenu, string> ChildMenuRepository { set; get; }
        public IQueryable<SMSRoleMenuMap> RoleMenuMap
        {
            get { return RoleMenuMapRepository.Entities; }
        }
        public IQueryable<SMSMenu> Menu
        {
            get { return MenuRepository.Entities; }
        }
        public IQueryable<SMSChildMenu> ChildMenu
        {
            get { return ChildMenuRepository.Entities; }
        }
        public DataResult LoadingMenu()
        {
            //获取当前登入用户的角色ID
            try
            {
                var roleid = IdentityManager.GetIdentFromAll().Role;
                if(roleid == null)
                {
                    return DataProcess.Failure("Session过期，请重新登入");
                }
                //获取角色对应一级菜单信息
                //var menu = RoleMenuMap.Where(a => a.RoleId == roleid).ToList();
                var menu = RoleMenuMap.Where(c => c.RoleId == roleid).Join(Menu, a => a.MenuId, b => b.Id, (a, b) => new {
                    MenuId = a.MenuId,
                    Name = b.Name,
                    Adress  = b.Adress,
                    Icon = b.Icon,
                    Enabled = b.Enabled
                }).Where(d => d.Enabled).ToList();
                //创建集合存放菜单信息
                List<MenuMessage> list = new List<MenuMessage>();
                MenuMessage menumessage;
                foreach(var m in menu)
                {
                    menumessage = new MenuMessage();
                    //获取一级菜单对应的二级菜单信息
                    var childmenu = ChildMenu.Where(a => a.FatherId == m.MenuId && a.Enabled).Select(b => new ChildMenuMessage{
                        Name = b.Name,
                        Adress = b. Adress,
                        Icon = b.Icon,
                        MenuId = b.Id
                    }).ToArray();

                    menumessage.MenuId = m.MenuId;
                    menumessage.Name = m.Name;
                    menumessage.Adress = m.Adress;
                    menumessage.Icon = m.Icon;
                    menumessage.ChildNodes = childmenu;

                    list.Add(menumessage);
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
