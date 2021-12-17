using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Forex
{
    public class ForexClient : AbstractClient, IForexClient
    {
        public ForexClient(IApiClient apiClient, CheckoutConfiguration configuration) :
            base(apiClient, configuration, SdkAuthorizationType.OAuth)
        {
        }

        public Task<QuoteResponse> RequestQuote(QuoteRequest quoteRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("quoteRequest", quoteRequest);
            return ApiClient.Post<QuoteResponse>("forex/quotes", SdkAuthorization(), quoteRequest, cancellationToken);
        }
    }
}