using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Forex
{
    public class ForexClient : AbstractClient, IForexClient
    {
        private const string ForexPath = "forex";
        private const string QuotesPath = "quotes";
        private const string RatesPath = "rates";

        public ForexClient(IApiClient apiClient, CheckoutConfiguration configuration) :
            base(apiClient, configuration, SdkAuthorizationType.OAuth)
        {
        }

        public Task<QuoteResponse> RequestQuote(
            QuoteRequest quoteRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("quoteRequest", quoteRequest);
            return ApiClient.Post<QuoteResponse>(
                BuildPath(ForexPath, QuotesPath), 
                SdkAuthorization(), 
                quoteRequest, 
                cancellationToken
            );
        }

        public Task<RatesQueryResponse> GetRates(
            RatesQueryFilter ratesQueryFilter,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("ratesQueryFilter", ratesQueryFilter);
            return ApiClient.Query<RatesQueryResponse>(
                BuildPath(ForexPath, RatesPath), 
                SdkAuthorization(), 
                ratesQueryFilter, 
                cancellationToken
            );
        }
    }
}