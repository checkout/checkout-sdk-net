using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Payments.Sessions
{
    public interface IPaymentSessionsClient
    {
        Task<PaymentSessionsResponse> RequestPaymentSessions(PaymentSessionsRequest paymentSessionsRequest,
            CancellationToken cancellationToken = default);
    }
}