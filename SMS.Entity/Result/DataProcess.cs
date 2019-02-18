using SMS.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entity
{
    public class DataProcess
    {

        public static DataResult Failure()
        {
            return new DataResult()
            {
                Data = null,
                Message = "操作失败!",
                Success = false
            };
        }
        public static DataResult Failure(string message)
        {
            return new DataResult()
            {
                Data = null,
                Message = message,
                Success = false
            };
        }
        public static DataResult Failure(object data)
        {
            return new DataResult()
            {
                Data = data,
                Message = "操作失败!",
                Success = false
            };
        }
        public static DataResult Failure(string message, object data)
        {
            return new DataResult()
            {
                Data = data,
                Message = message,
                Success = false
            };
        }
        public static DataResult SetDataResult(bool success, string message, object data)
        {
            return new DataResult()
            {
                Data = data,
                Message = message,
                Success = success
            };
        }
        public static DataResult Success()
        {
            return new DataResult()
            {
                Data = null,
                Message = "操作成功!",
                Success = true
            };
        }
        public static DataResult Success(string message)
        {
            return new DataResult()
            {
                Data = null,
                Message = message,
                Success = true
            };
        }
        public static DataResult Success(object data)
        {
            return new DataResult()
            {
                Data = data,
                Message = "操作成功!",
                Success = true
            };
        }
        public static DataResult Success(string message, object data)
        {
            return new DataResult()
            {
                Data = data,
                Message = message,
                Success = true
            };
        }
    }
}
