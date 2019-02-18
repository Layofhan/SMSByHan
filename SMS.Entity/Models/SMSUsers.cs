using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using SMS.Data.Services;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace SMS.Entity
{
    [Table("Users")]
    public class SMSUsers: EntityBase<int>
    {

        public string Name {get;set;}
        public string PassWord { get; set; }
       
    }
}
