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
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.OAuth))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);
        }

        [Fact]
        private async Task EnrollShouldEnroll()
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
        private async Task EnrollShouldThrowExceptionWhenEnrollRequestIsNull()
        {
            IAgenticClient client = new AgenticClient(_apiClient.Object, _configuration.Object);

            var exception = await Should.ThrowAsync<CheckoutArgumentException>(
                async () => await client.Enroll(null, CancellationToken.None));

            exception.Message.ShouldContain("agenticEnrollRequest");
        }

        [Fact]
        private async Task EnrollShouldCallCorrectEnrollEndpoint()
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
        private async Task EnrollShouldUseCorrectAuthorizationForEnroll()
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

        [Fact]
        private async Task CreatePurchaseIntentShouldCreatePurchaseIntent()
        {
            var createPurchaseIntentRequest = new AgenticPurchaseIntentCreateRequest
            {
                NetworkTokenId = "nt_e7fjr77crbgmlhpjvuq3bj6jba",
                Device = new DeviceInfo
                {
                    IpAddress = "192.168.1.100",
                    UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36",
                    DeviceBrand = "apple",
                    DeviceType = "tablet"
                },
                CustomerPrompt = "I'm looking for running shoes in a size 10, for under $150.",
                Mandates = new[]
                {
                    new Mandate
                    {
                        Id = "mandate_123",
                        PurchaseThreshold = new PurchaseThreshold
                        {
                            Amount = 100,
                            CurrencyCode = "USD"
                        },
                        Description = "Purchase running shoes in size 10.",
                        ExpirationDate = System.DateTime.Parse("2026-08-31T23:59:59.000Z")
                    }
                }
            };

            var expectedResponse = new AgenticPurchaseIntentResponse
            {
                Id = "pi_f3egwppx6rde3hg6itlqzp3h7e",
                Scheme = "visa",
                Status = "active",
                TokenId = "nt_e7fjr77crbgmlhpjvuq3bj6jba",
                DeviceData = new DeviceInfo
                {
                    IpAddress = "192.168.1.100",
                    UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36",
                    DeviceBrand = "apple",
                    DeviceType = "tablet"
                },
                CustomerPrompt = "Hey AI, I need Nike running shoes in size 10 under $130.00",
                Mandates = new[]
                {
                    new Mandate
                    {
                        Id = "mandate_123",
                        PurchaseThreshold = new PurchaseThreshold
                        {
                            Amount = 100,
                            CurrencyCode = "USD"
                        },
                        Description = "Purchase Nike Air Max 270 running shoes in size 10",
                        ExpirationDate = System.DateTime.Parse("2026-08-31T23:59:59.000Z")
                    }
                },
                Links = new AgenticLinks
                {
                    Self = "https://api.example.com/agentic/purchase-intents/intent_123",
                    CreateCredentials = "https://api.example.com/agentic/purchase-intents/intent_123/credentials",
                    Update = "https://api.example.com/agentic/purchase-intents/intent_123",
                    Cancel = "https://api.example.com/agentic/purchase-intents/intent_123/cancel"
                }
            };

            _apiClient.Setup(apiClient =>
                    apiClient.Post<AgenticPurchaseIntentResponse>("agentic/purchase-intent", _authorization, createPurchaseIntentRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => expectedResponse);

            IAgenticClient client = new AgenticClient(_apiClient.Object, _configuration.Object);

            var response = await client.CreatePurchaseIntent(createPurchaseIntentRequest, CancellationToken.None);

            response.ShouldNotBeNull();
            response.Id.ShouldBe(expectedResponse.Id);
            response.Scheme.ShouldBe(expectedResponse.Scheme);
            response.Status.ShouldBe(expectedResponse.Status);
            response.TokenId.ShouldBe(expectedResponse.TokenId);
            response.CustomerPrompt.ShouldBe(expectedResponse.CustomerPrompt);
            response.Links.ShouldNotBeNull();
            response.Links.Self.ShouldBe(expectedResponse.Links.Self);
        }

        [Fact]
        private async Task CreatePurchaseIntentShouldThrowExceptionWhenCreatePurchaseIntentRequestIsNull()
        {
            IAgenticClient client = new AgenticClient(_apiClient.Object, _configuration.Object);

            var exception = await Should.ThrowAsync<CheckoutArgumentException>(
                async () => await client.CreatePurchaseIntent(null, CancellationToken.None));

            exception.Message.ShouldContain("agenticPurchaseIntentCreateRequest");
        }

        [Fact]
        private async Task CreatePurchaseIntentShouldCallCorrectCreatePurchaseIntentEndpoint()
        {
            var createPurchaseIntentRequest = new AgenticPurchaseIntentCreateRequest
            {
                NetworkTokenId = "nt_test_123",
                Device = new DeviceInfo
                {
                    IpAddress = "192.168.1.100",
                    UserAgent = "Mozilla/5.0 Test"
                },
                CustomerPrompt = "Test prompt",
                Mandates = new[]
                {
                    new Mandate
                    {
                        Id = "mandate_test",
                        Description = "Test mandate"
                    }
                }
            };

            _apiClient.Setup(apiClient =>
                    apiClient.Post<AgenticPurchaseIntentResponse>("agentic/purchase-intent", _authorization, createPurchaseIntentRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => new AgenticPurchaseIntentResponse());

            IAgenticClient client = new AgenticClient(_apiClient.Object, _configuration.Object);

            await client.CreatePurchaseIntent(createPurchaseIntentRequest, CancellationToken.None);

            _apiClient.Verify(apiClient =>
                apiClient.Post<AgenticPurchaseIntentResponse>("agentic/purchase-intent", _authorization, createPurchaseIntentRequest,
                    CancellationToken.None, null), Times.Once);
        }

        [Fact]
        private async Task CreatePurchaseIntentShouldUseCorrectAuthorizationForCreatePurchaseIntent()
        {
            var createPurchaseIntentRequest = new AgenticPurchaseIntentCreateRequest
            {
                NetworkTokenId = "nt_test_123",
                Device = new DeviceInfo(),
                CustomerPrompt = "Test prompt"
            };

            _apiClient.Setup(apiClient =>
                    apiClient.Post<AgenticPurchaseIntentResponse>(It.IsAny<string>(), It.IsAny<SdkAuthorization>(), 
                        It.IsAny<AgenticPurchaseIntentCreateRequest>(), It.IsAny<CancellationToken>(), It.IsAny<string>()))
                .ReturnsAsync(() => new AgenticPurchaseIntentResponse());

            IAgenticClient client = new AgenticClient(_apiClient.Object, _configuration.Object);

            await client.CreatePurchaseIntent(createPurchaseIntentRequest, CancellationToken.None);

            _apiClient.Verify(apiClient =>
                apiClient.Post<AgenticPurchaseIntentResponse>(It.IsAny<string>(), _authorization, 
                    It.IsAny<AgenticPurchaseIntentCreateRequest>(), It.IsAny<CancellationToken>(), It.IsAny<string>()), Times.Once);
        }
    }
}