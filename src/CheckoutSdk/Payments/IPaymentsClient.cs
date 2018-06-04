using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Payments
{
    public interface IPaymentsClient
    {
        Task<ApiResponse<PaymentResponse<CardSourceResponse>>> RequestAsync(PaymentRequest<CardSource> cardPaymentRequest, CancellationToken cancellationToken = default(CancellationToken));
        Task<ApiResponse<PaymentResponse<CardSourceResponse>>> RequestAsync(PaymentRequest<TokenSource> tokenPaymentRequest, CancellationToken cancellationToken = default(CancellationToken));
        Task<ApiResponse<VoidResponse>> VoidAsync(string paymentId, VoidRequest voidRequest = null, CancellationToken cancellationToken = default(CancellationToken));
        Task<ApiResponse<CaptureResponse>> CaptureAsync(string paymentId, CaptureRequest captureRequest = null, CancellationToken cancellationToken = default(CancellationToken));
        Task<ApiResponse<RefundResponse>> RefundAsync(string paymentId, RefundRequest refundRequest = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}