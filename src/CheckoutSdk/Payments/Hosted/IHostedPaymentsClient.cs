using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Payments.Hosted
{
    public interface IHostedPaymentsClient
    {
        Task<HostedPaymentDetailsResponse> GetHostedPaymentsPageDetails(string hostedPaymentId,
            CancellationToken cancellationToken = default);

        Task<HostedPaymentResponse> CreateHostedPaymentsPageSession(HostedPaymentRequest hostedPaymentRequest,
            CancellationToken cancellationToken = default);
    }
}