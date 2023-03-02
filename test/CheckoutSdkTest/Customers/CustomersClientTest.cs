using Checkout.Common;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Customers
{
    public class CustomersClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.Previous, ValidPreviousSk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.Previous);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public CustomersClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKeyOrOAuth))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object, null);
        }

        [Fact]
        private async Task ShouldGetCustomer()
        {
            _apiClient.Setup(apiClient =>
                    apiClient.Get<CustomerDetailsResponse>("customers/cus_12345", _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => new CustomerDetailsResponse());
            ICustomersClient client = new CustomersClient(_apiClient.Object, _configuration.Object);

            var response = await client.Get("cus_12345", CancellationToken.None);

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldCreateCustomer()
        {
            var customerRequest = new CustomerRequest();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<IdResponse>("customers", _authorization, customerRequest,
                        CancellationToken.None
                        , null))
                .ReturnsAsync(() => new IdResponse());

            ICustomersClient client = new CustomersClient(_apiClient.Object, _configuration.Object);

            var response = await client.Create(customerRequest, CancellationToken.None);

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldUpdateCustomer()
        {
            var customerRequest = new CustomerRequest();

            _apiClient.Setup(apiClient =>
                    apiClient.Patch<EmptyResponse>("customers/cus_12345", _authorization, customerRequest,
                        CancellationToken.None
                        , null))
                .ReturnsAsync(() => new EmptyResponse());

            ICustomersClient client = new CustomersClient(_apiClient.Object, _configuration.Object);

            var response = await client.Update("cus_12345", customerRequest, CancellationToken.None);

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldDeleteCustomer()
        {
            _apiClient.Setup(apiClient =>
                    apiClient.Delete<EmptyResponse>("customers/cus_12345", _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => new EmptyResponse());

            ICustomersClient client = new CustomersClient(_apiClient.Object, _configuration.Object);

            var response = await client.Delete("cus_12345", CancellationToken.None);

            response.ShouldNotBeNull();
        }
    }
}