using System.Collections.Generic;

namespace Menherachan.Domain.Entities.Responses
{
    public class Response<T>
    {
        public int Status { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }

        public Response()
        {
        }

        public Response(T data, string message = null)
        {
            Succeeded = true;
            Status = 200;
            Message = message;
            Data = data;
        }

        public Response(string message, int errorCode = 401)
        {
            Succeeded = false;
            Status = errorCode;
            Message = message;
        }
    }
}