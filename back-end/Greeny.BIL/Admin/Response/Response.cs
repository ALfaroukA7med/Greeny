
using Greeny.BLL.Abstraction;

namespace Greeny.BLL.Admin.Response
{
    public class Response<T>
    {
        public bool IsSuccess { get; set; }

        public Error? Error { get; set; }

        public T? Data { get; set; }

        public static Response<T> Success(T data)
        {
            return new Response<T>
            {
                IsSuccess = true,
                Data = data
            };
        }

        public static Response<T> Fail(Error errorMessage)
        {
            return new Response<T>
            {
                IsSuccess = false,
                Error = errorMessage,
            };
        }
    }
}
