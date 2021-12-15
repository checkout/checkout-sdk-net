using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Payments.Hosted
{
    public interface IHostedPaymentsClient
    {
        Task<HostedPaymentDetailsResponse> Get(string hostedPaymentId,
            CancellationToken cancellationToken = default);

        Task<HostedPaymentResponse> Create(HostedPaymentRequest hostedPaymentRequest,
            CancellationToken cancellationToken = default);
    }
}