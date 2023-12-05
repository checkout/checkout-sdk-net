using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Payments.Contexts
{
    public interface IPaymentContextsClient
    {
        Task<PaymentContextsRequestResponse> RequestPaymentContexts(PaymentContextsRequest paymentContextsRequest,
            CancellationToken cancellationToken = default);

        Task<PaymentContextDetailsResponse> GetPaymentContextDetails(string paymentContextId,
            CancellationToken cancellationToken = default);
    }
}