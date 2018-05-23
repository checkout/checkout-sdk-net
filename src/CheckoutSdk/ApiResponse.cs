using System.Net;

namespace Checkout
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public Error Error { get; set; }
    }

    public class ApiResponse<TResult> : ApiResponse
    {
        public TResult Result { get; set; }
    }
}