namespace Hui.Api.Models.Response
{
    public class ResponseData<T>
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }

        public static ResponseData<T> Success(T value, string msg = "")
        {
            ResponseData<T> responseData = new ResponseData<T>
            {
                Data = value,
                Code = 0
            };
            
            responseData.Message = string.IsNullOrEmpty(msg) ? "请求成功" : msg;
            
            return responseData;
        }

        public static ResponseData<T> Failure(int code, string msg, T value)
        {
            ResponseData<T> responseData = new ResponseData<T>
            {
                Data = value,
                Code = code
            };
            
            responseData.Message = string.IsNullOrEmpty(msg) ? "系统错误" : msg;
            
            return responseData;
        }
    }
}