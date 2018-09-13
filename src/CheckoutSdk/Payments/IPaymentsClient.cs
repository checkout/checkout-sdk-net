using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Sdk.Payments
{
    public interface IPaymentsClient
    {
        Task<PaymentResponse<CardSourceResponse>> RequestAsync(PaymentRequest<CardSource> cardPaymentRequest, CancellationToken cancellationToken = default(CancellationToken));
        Task<PaymentResponse<CardSourceResponse>> RequestAsync(PaymentRequest<TokenSource> tokenPaymentRequest, CancellationToken cancellationToken = default(CancellationToken));
        Task<PaymentResponse<CardSourceResponse>> RequestAsync(PaymentRequest<CustomerSource> customerPaymentRequest, CancellationToken cancellationToken = default(CancellationToken));
        Task<VoidResponse> VoidAsync(string paymentId, VoidRequest voidRequest = null, CancellationToken cancellationToken = default(CancellationToken));
        Task<CaptureResponse> CaptureAsync(string paymentId, CaptureRequest captureRequest = null, CancellationToken cancellationToken = default(CancellationToken));
        Task<RefundResponse> RefundAsync(string paymentId, RefundRequest refundRequest = null, CancellationToken cancellationToken = default(CancellationToken));
        Task<GetPaymentResponse> GetAsync(string paymentId, CancellationToken cancellationToken = default(CancellationToken));
    }
}