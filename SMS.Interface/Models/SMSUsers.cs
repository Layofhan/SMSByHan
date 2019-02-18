using SMS.Data.Services;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.Interface
{
    [Table("Users")]
    public class SMSUsers: EntityBase<int>
    {
        public string Name {get;set;}
        public string PassWord { get; set; }

    }
}
