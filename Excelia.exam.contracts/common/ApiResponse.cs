using System.Net;

namespace Excelia.exam.contracts.common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public  HttpStatusCode StatusCode { get; set; }
        public List<Error> Errors { get; set; }

    }
}