using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entity
{
    public class DataResult<TData>
    {

        public TData Data { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
