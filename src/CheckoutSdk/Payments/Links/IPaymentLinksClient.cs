using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Payments.Links
{
    public interface IPaymentLinksClient
    {
        Task<PaymentLinkDetailsResponse> Get(string reference, CancellationToken cancellationToken = default);

        Task<PaymentLinkResponse> Create(PaymentLinkRequest paymentLinkRequest,
            CancellationToken cancellationToken = default);
    }
}