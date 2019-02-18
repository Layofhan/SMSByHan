using SMS.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration.Configuration;


namespace SMS.Data.Services
{
    public abstract class EntityBase<TKey>:IEntity<TKey>
    {
        public virtual TKey Id { get; set; }

    }
}
