using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Interface
{
    public class SMSUserConfiguration : EntityTypeConfiguration<SMSUsers>, IEntityMapper
    {
        public SMSUserConfiguration()
        {
            HasKey(a => a.Id);
        }
        public void RegistTo(ConfigurationRegistrar configurations)
        {
            configurations.Add(this);
            //throw new NotImplementedException();
        }
    }
}
