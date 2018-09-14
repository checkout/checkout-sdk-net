using System.Threading;
using System.Threading.Tasks;
using Checkout.Sdk.Payments;

namespace Checkout.Sdk.Payments
{
    public interface IPaymentsClient
    {
        Task<PaymentResponse> RequestAsync<TPaymentSource>(PaymentRequest<TPaymentSource> cardPaymentRequest, CancellationToken cancellationToken = default(CancellationToken)) 
            where TPaymentSource : IPaymentSource; 
        Task<VoidResponse> VoidAsync(string paymentId, VoidRequest voidRequest = null, CancellationToken cancellationToken = default(CancellationToken));
        Task<CaptureResponse> CaptureAsync(string paymentId, CaptureRequest captureRequest = null, CancellationToken cancellationToken = default(CancellationToken));
        Task<RefundResponse> RefundAsync(string paymentId, RefundRequest refundRequest = null, CancellationToken cancellationToken = default(CancellationToken));
        Task<GetPaymentResponse> GetAsync(string paymentId, CancellationToken cancellationToken = default(CancellationToken));
    }
}