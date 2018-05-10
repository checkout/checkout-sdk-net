using System.Net;

namespace Checkout
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; }
        public Error Error { get; set; }
    }
}