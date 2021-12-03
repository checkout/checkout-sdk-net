using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Payments.Hosted
{
	public interface IHostedPaymentsClient
	{
		Task<HostedPaymentResponse> CreateAsync(HostedPaymentRequest hostedPaymentRequest, CancellationToken cancellationToken = default);
	}
}