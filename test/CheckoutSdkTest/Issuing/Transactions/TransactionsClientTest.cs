using Checkout.Issuing.Transactions.Requests.Query;
using Checkout.Issuing.Transactions.Responses.Query;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Issuing.Transactions
{
    public class TransactionsClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization =
            new SdkAuthorization(PlatformType.DefaultOAuth, ValidDefaultSk);

        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.DefaultOAuth);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public TransactionsClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.OAuth))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);
        }
        
        [Fact]
        private async Task ShouldGetListTransactions()
        {
            TransactionsQueryFilter query = new TransactionsQueryFilter();
            TransactionsListResponse response = new TransactionsListResponse();
            
            _apiClient.Setup(apiClient =>
                    apiClient.Query<TransactionsListResponse>("issuing/transactions", _authorization,
                        query,
                        CancellationToken.None))
                .ReturnsAsync(() => response);

            IIssuingClient client = new IssuingClient(_apiClient.Object, _configuration.Object);

            TransactionsListResponse result = await client.GetListTransactions(query);

            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
        }
        
        [Fact]
        private async Task ShouldGetSingleTransaction()
        {
            TransactionSingleResponse response = new TransactionSingleResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<TransactionSingleResponse>("issuing/transactions/transaction_id", _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => response);

            IIssuingClient client = new IssuingClient(_apiClient.Object, _configuration.Object);
            
            TransactionSingleResponse result = await client.GetSingleTransaction("transaction_id");

            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
        }
    }
}