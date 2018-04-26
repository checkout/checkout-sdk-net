using System.Net;
using Checkout.Payments;

namespace Checkout.Payments
{
    public static class ApiResponseExtensions
    {
        public static bool RequiresRedirect<TResult>(this ApiResponse<TResult> apiResponse) where TResult : IPaymentResponse
        {
            return apiResponse.StatusCode == HttpStatusCode.Accepted; // && check links etc.
        }
    }
}