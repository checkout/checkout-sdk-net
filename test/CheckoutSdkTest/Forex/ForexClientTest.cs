using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Forex
{
    public class ForexClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.DefaultOAuth, ValidDefaultSk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.DefaultOAuth);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public ForexClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.OAuth))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object, null);
        }

        [Fact]
        private async Task ShouldRequestQuote()
        {
            QuoteRequest quoteRequest = new QuoteRequest();
            QuoteResponse quoteResponse = new QuoteResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<QuoteResponse>("forex/quotes", _authorization,
                        quoteRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => quoteResponse);

            IForexClient client =
                new ForexClient(_apiClient.Object, _configuration.Object);

            QuoteResponse response = await client.RequestQuote(quoteRequest);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(quoteResponse);
        }
        
        [Fact]
        private async Task ShouldGetRates()
        {
            RatesQueryFilter ratesQueryFilter = new RatesQueryFilter()
            {
                Product = "card_payouts",
                Source = ForexSource.Visa,
                CurrencyPairs = "GBPEUR,USDNOK,JPNCAD",
                ProcessChannelId = "pc_vxt6yftthv4e5flqak6w2i7rim"
            };
            RatesQueryResponse ratesQueryResponse = new RatesQueryResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Query<RatesQueryResponse>(
                        "forex/rates", 
                        _authorization,
                        ratesQueryFilter,
                        CancellationToken.None))
                .ReturnsAsync(() => ratesQueryResponse);

            IForexClient client =
                new ForexClient(_apiClient.Object, _configuration.Object);

            RatesQueryResponse response = await client.GetRates(ratesQueryFilter);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(ratesQueryResponse);
        }
    }
}