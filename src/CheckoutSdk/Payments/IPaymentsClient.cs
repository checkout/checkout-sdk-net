using System.Threading.Tasks;

namespace Checkout.Payments
{
    public interface IPaymentsClient
    {
        Task<ApiResponse<PaymentResponse<CardSourceResponse>>> RequestAsync(PaymentRequest<CardSource> cardPaymentRequest);
        Task<ApiResponse<PaymentResponse<CardSourceResponse>>> RequestAsync(PaymentRequest<TokenSource> tokenPaymentRequest);
        Task<ApiResponse<CaptureResponse>> CaptureAsync(string paymentId, CaptureRequest captureRequest = null);
    }
}