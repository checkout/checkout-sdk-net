using System.Threading;
using System.Threading.Tasks;
using Moq;
using Shouldly;
using Xunit;
using Checkout.Agentic.Requests;
using Checkout.Agentic.Responses;
using Checkout.Common;

namespace Checkout.Agentic
{
    public class AgenticClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.Default, ValidDefaultSk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.Default);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public AgenticClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKeyOrOAuth))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);
        }

        [Fact]
        private async Task ShouldEnrollInAgenticServices()
        {
            var agenticEnrollRequest = new AgenticEnrollRequest
            {
                Source = new PaymentSource
                {
                    Number = "4242424242424242",
                    ExpiryMonth = 12,
                    ExpiryYear = 2025,
                    Cvv = "123",
                    Type = PaymentSourceType.Card
                },
                Device = new DeviceInfo
                {
                    IpAddress = "192.168.1.1",
                    UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36"
                },
                Customer = new AgenticCustomer
                {
                    Email = "test@example.com",
                    CountryCode = CountryCode.US,
                    LanguageCode = "en"
                }
            };

            var expectedResponse = new AgenticEnrollResponse
            {
                TokenId = "nt_e7fjr77crbgmlhpjvuq3bj6jba",
                Status = "enrolled",
                CreatedAt = System.DateTime.UtcNow
            };

            _apiClient.Setup(apiClient =>
                    apiClient.Post<AgenticEnrollResponse>("agentic/enroll", _authorization, agenticEnrollRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => expectedResponse);

            IAgenticClient client = new AgenticClient(_apiClient.Object, _configuration.Object);

            var response = await client.Enroll(agenticEnrollRequest, CancellationToken.None);

            response.ShouldNotBeNull();
            response.TokenId.ShouldBe(expectedResponse.TokenId);
            response.Status.ShouldBe(expectedResponse.Status);
            response.CreatedAt.ShouldBe(expectedResponse.CreatedAt);
        }

        [Fact]
        private async Task ShouldThrowExceptionWhenRequestIsNull()
        {
            IAgenticClient client = new AgenticClient(_apiClient.Object, _configuration.Object);

            var exception = await Should.ThrowAsync<CheckoutArgumentException>(
                async () => await client.Enroll(null, CancellationToken.None));

            exception.Message.ShouldContain("agenticEnrollRequest");
        }

        [Fact]
        private async Task ShouldCallCorrectEndpoint()
        {
            var agenticEnrollRequest = new AgenticEnrollRequest
            {
                Source = new PaymentSource
                {
                    Number = "4242424242424242",
                    ExpiryMonth = 12,
                    ExpiryYear = 2025,
                    Cvv = "123",
                    Type = PaymentSourceType.Card
                },
                Device = new DeviceInfo
                {
                    IpAddress = "192.168.1.1",
                    UserAgent = "Mozilla/5.0 Test"
                },
                Customer = new AgenticCustomer
                {
                    Email = "test@example.com",
                    CountryCode = CountryCode.US,
                    LanguageCode = "en"
                }
            };

            _apiClient.Setup(apiClient =>
                    apiClient.Post<AgenticEnrollResponse>("agentic/enroll", _authorization, agenticEnrollRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => new AgenticEnrollResponse());

            IAgenticClient client = new AgenticClient(_apiClient.Object, _configuration.Object);

            await client.Enroll(agenticEnrollRequest, CancellationToken.None);

            _apiClient.Verify(apiClient =>
                apiClient.Post<AgenticEnrollResponse>("agentic/enroll", _authorization, agenticEnrollRequest,
                    CancellationToken.None, null), Times.Once);
        }

        [Fact]
        private async Task ShouldUseCorrectAuthorization()
        {
            var agenticEnrollRequest = new AgenticEnrollRequest
            {
                Source = new PaymentSource { Type = PaymentSourceType.Card },
                Device = new DeviceInfo(),
                Customer = new AgenticCustomer { Email = "test@example.com", CountryCode = CountryCode.US, LanguageCode = "en" }
            };

            _apiClient.Setup(apiClient =>
                    apiClient.Post<AgenticEnrollResponse>(It.IsAny<string>(), It.IsAny<SdkAuthorization>(), 
                        It.IsAny<AgenticEnrollRequest>(), It.IsAny<CancellationToken>(), It.IsAny<string>()))
                .ReturnsAsync(() => new AgenticEnrollResponse());

            IAgenticClient client = new AgenticClient(_apiClient.Object, _configuration.Object);

            await client.Enroll(agenticEnrollRequest, CancellationToken.None);

            _apiClient.Verify(apiClient =>
                apiClient.Post<AgenticEnrollResponse>(It.IsAny<string>(), _authorization, 
                    It.IsAny<AgenticEnrollRequest>(), It.IsAny<CancellationToken>(), It.IsAny<string>()), Times.Once);
        }
    }
}