using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entity.Models
{
    public class TabularData<TKey>
    {
        /// <summary>
        /// 当前查询的数据
        /// </summary>
        public List<TKey> List = new List<TKey>();
        /// <summary>
        /// 总数量
        /// </summary>
        public int AllCount;
    }
}
