using SMS.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMS.Tests.EfTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            SMSUsers user = new SMSUsers();
            user.Name = "df";
            user.PassWord = "df";
            DbContextBase context = new DbContextBase();

            context.Set<SMSUsers>().Add(user);
            context.SaveChanges();
            MessageBox.Show("dd");
        }
    }
}
