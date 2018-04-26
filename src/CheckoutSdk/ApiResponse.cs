using System.Net;

namespace Checkout
{
    public class ApiResponse<TResult>
    {
        public HttpStatusCode StatusCode { get; }
        public Error Error { get; set; }
        public TResult Result { get; }
    }
}