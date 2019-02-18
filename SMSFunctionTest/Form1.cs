using SMS.Data.Interface;
using SMS.Data.Services;
using SMS.Entity;
using SMS.Entity.Models;
using SMS.Entity.Result;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMSFunctionTest
{
    public partial class Form1 : Form
    {
       public IRepository<SMSUsers, int> WmsAbutentsRepository { set; get; }

        public Form1()
        {
            InitializeComponent();
            
            SMSUsers user = new SMSUsers();
            user.Name = "df";
            user.PassWord = "df";
            DbContextBase context = new DbContextBase();
            context.Set<SMSUsers>().Add(user);
            context.SaveChanges();

            //var test = new SMSUserDetail();
            //test.Qiannmin = "111";
            //context.Set<SMSUserDetail>().Add(test);
            //try
            //{
            //    DataResult re = DataProcess.Success("chenggon");
            //    WmsAbutentsRepository.Insert(user);
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            MessageBox.Show("dd");
        }
    }
}
