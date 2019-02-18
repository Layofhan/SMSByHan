using SMS.Data.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entity.Models
{
    [Table("SMSUserDetail")]
    public class SMSUserDetail : EntityBase<int>
    {
        public string Qiannmin { get; set; }
    }
}
