using System.Threading.Tasks;
using Checkout.Payments.Request;
using Checkout.Payments.Response;

namespace Checkout.Payments
{
    public interface IPaymentsClient
    {
        Task<ApiResponse<PaymentResponse<Response.CardSource>>> RequestAsync(PaymentRequest<Request.CardSource> cardPaymentRequest);
        Task<ApiResponse<PaymentResponse<Response.CardSource>>> RequestAsync(PaymentRequest<Request.TokenSource> tokenPaymentRequest);
    }
}