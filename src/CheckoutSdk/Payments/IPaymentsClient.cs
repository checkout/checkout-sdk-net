using System.Threading.Tasks;

namespace Checkout.Payments
{
    public interface IPaymentsClient
    {
        Task<ApiResponse<PaymentResponse<CardSourceResponse>>> RequestAsync(PaymentRequest<CardSource> cardPaymentRequest);
        Task<ApiResponse<PaymentResponse<CardSourceResponse>>> RequestAsync(PaymentRequest<TokenSource> tokenPaymentRequest);
        Task<ApiResponse<VoidResponse>> VoidAsync(string paymentId, VoidRequest voidRequest = null);
        Task<ApiResponse<CaptureResponse>> CaptureAsync(string paymentId, CaptureRequest captureRequest = null);
        Task<ApiResponse<RefundResponse>> RefundAsync(string paymentId, RefundRequest refundRequest = null);
    }
}