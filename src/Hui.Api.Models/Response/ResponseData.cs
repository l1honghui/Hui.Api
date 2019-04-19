using System;
using System.Runtime.CompilerServices;

namespace Hui.Api.Models.DbSchemas
{
    public class ResponseData<T>
    {
        public int Code
        {
            [CompilerGenerated]
            get;
            [CompilerGenerated]
            set;
        }

        public string Message
        {
            [CompilerGenerated]
            get;
            [CompilerGenerated]
            set;
        }

        public T Data
        {
            [CompilerGenerated]
            get;
            [CompilerGenerated]
            set;
        }

        public static ResponseData<T> Success(T value, string msg = "")
        {
            ResponseData<T> responseData = new ResponseData<T>
            {
                Data = value,
                Code = 0
            };
            if (string.IsNullOrEmpty(msg))
            {
                responseData.Message = "请求成功";
            }
            else
            {
                responseData.Message = msg;
            }
            return responseData;
        }

        public static ResponseData<T> Failure(int code, string msg, T value)
        {
            ResponseData<T> responseData = new ResponseData<T>
            {
                Data = value,
                Code = code
            };
            if (string.IsNullOrEmpty(msg))
            {
                responseData.Message = "系统错误";
            }
            else
            {
                responseData.Message = msg;
            }
            return responseData;
        }
    }
}
