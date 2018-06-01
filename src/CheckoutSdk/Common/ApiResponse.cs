using System.Net;

namespace Checkout
{
    public class ApiResponse
    {        
        public HttpStatusCode StatusCode { get; set; }
        public Error Error { get; set; }
        public string RequestId { get; set; }

        public bool HasErrors => Error != null;
    }

    public class ApiResponse<TResult> : ApiResponse
    {
        public TResult Result { get; set; }

        public static implicit operator TResult(ApiResponse<TResult> apiResponse)
        {
            return apiResponse.Result;
        }
    }
}