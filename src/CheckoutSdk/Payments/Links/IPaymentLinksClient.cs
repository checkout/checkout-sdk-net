using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Payments.Links
{

	public interface IPaymentLinksClient
	{
		Task<PaymentLinkDetailsResponse> GetAsync(string reference, CancellationToken cancellationToken = default);

		Task<PaymentLinkResponse> CreateAsync(PaymentLinkRequest paymentLinkRequest, CancellationToken cancellationToken = default);
	}

}