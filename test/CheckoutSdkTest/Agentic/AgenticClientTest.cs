using Checkout.Agentic.Requests;
using Checkout.Agentic.Responses;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Agentic
{
    public class AgenticClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.DefaultOAuth, ValidDefaultSk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.DefaultOAuth);
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
        public async Task CreateAgenticCommerce_Should_Call_ApiClient_Post()
        {
            // Arrange
            var request = new CreateAgenticCommerceRequest
            {
                Name = "Test Agentic Commerce",
                Description = "Test Description",
                Configuration = new AgenticConfiguration
                {
                    AiModel = "gpt-4",
                    MaxAutonomousActions = 100,
                    EnableAutonomousPayments = true,
                    RiskThreshold = 0.8m,
                    AllowedPaymentMethods = new List<string> { "card", "bank_transfer" }
                },
                Metadata = new Dictionary<string, object> { { "test", "value" } },
                WebhookEndpoints = new List<string> { "https://example.com/webhook" }
            };

            var response = new CreateAgenticCommerceResponse();

            _apiClient.Setup(apiClient => 
                    apiClient.Post<CreateAgenticCommerceResponse>(
                    "agentic/commerce",
                    _authorization,
                    request,
                    CancellationToken.None,
                    null))
                .ReturnsAsync(response);
            
            var client = new AgenticClient(_apiClient.Object, _configuration.Object);

            // Act
            var result = await client.CreateAgenticCommerce(request);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
        }

        [Fact]
        public async Task GetAgenticCommerce_Should_Call_ApiClient_Get()
        {
            // Arrange
            const string agenticCommerceId = "agentic_commerce_id";
            var response = new GetAgenticCommerceResponse();

            _apiClient.Setup(apiClient => 
                    apiClient.Get<GetAgenticCommerceResponse>(
                    "agentic/commerce/agentic_commerce_id",
                    _authorization,
                    CancellationToken.None))
                .ReturnsAsync(response);
            
            var client = new AgenticClient(_apiClient.Object, _configuration.Object);

            // Act
            var result = await client.GetAgenticCommerce(agenticCommerceId);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
        }

        [Fact]
        public async Task UpdateAgenticCommerce_Should_Call_ApiClient_Patch()
        {
            // Arrange
            const string agenticCommerceId = "agentic_commerce_id";
            var request = new UpdateAgenticCommerceRequest
            {
                Name = "Updated Test Agentic Commerce",
                Description = "Updated Description",
                Configuration = new AgenticConfiguration
                {
                    AiModel = "gpt-4-turbo",
                    MaxAutonomousActions = 200,
                    EnableAutonomousPayments = false,
                    RiskThreshold = 0.9m,
                    AllowedPaymentMethods = new List<string> { "card" }
                },
                IsActive = true
            };

            var response = new UpdateAgenticCommerceResponse();

            _apiClient.Setup(apiClient => 
                    apiClient.Patch<UpdateAgenticCommerceResponse>(
                    "agentic/commerce/agentic_commerce_id",
                    _authorization,
                    request,
                    CancellationToken.None,
                    null))
                .ReturnsAsync(response);
            
            var client = new AgenticClient(_apiClient.Object, _configuration.Object);

            // Act
            var result = await client.UpdateAgenticCommerce(agenticCommerceId, request);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
        }

        [Fact]
        public async Task DeleteAgenticCommerce_Should_Call_ApiClient_Delete()
        {
            // Arrange
            const string agenticCommerceId = "agentic_commerce_id";
            var response = new DeleteAgenticCommerceResponse();

            _apiClient.Setup(apiClient => 
                    apiClient.Delete<DeleteAgenticCommerceResponse>(
                    "agentic/commerce/agentic_commerce_id",
                    _authorization,
                    CancellationToken.None))
                .ReturnsAsync(response);
            
            var client = new AgenticClient(_apiClient.Object, _configuration.Object);

            // Act
            var result = await client.DeleteAgenticCommerce(agenticCommerceId);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
        }

        [Fact]
        public async Task ListAgenticCommerce_Should_Call_ApiClient_Query()
        {
            // Arrange
            var request = new ListAgenticCommerceRequest
            {
                Skip = 0,
                Limit = 10,
                IsActive = true,
                NameFilter = "test",
                AiModelFilter = "gpt-4",
                SortBy = "created_at",
                SortDirection = "desc"
            };

            var response = new ListAgenticCommerceResponse();

            _apiClient.Setup(apiClient => 
                    apiClient.Query<ListAgenticCommerceResponse>(
                    "agentic/commerce",
                    _authorization,
                    request,
                    CancellationToken.None))
                .ReturnsAsync(response);
            
            var client = new AgenticClient(_apiClient.Object, _configuration.Object);

            // Act
            var result = await client.ListAgenticCommerce(request);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
        }

        [Fact]
        public async Task ListAgenticCommerce_Without_Request_Should_Call_ApiClient_Query()
        {
            // Arrange
            var response = new ListAgenticCommerceResponse();

            _apiClient.Setup(apiClient => 
                    apiClient.Query<ListAgenticCommerceResponse>(
                    "agentic/commerce",
                    _authorization,
                    null,
                    CancellationToken.None))
                .ReturnsAsync(response);
            
            var client = new AgenticClient(_apiClient.Object, _configuration.Object);

            // Act
            var result = await client.ListAgenticCommerce();

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
        }

        [Fact]
        public void CreateAgenticCommerce_Should_Throw_Exception_When_Request_Is_Null()
        {
            // Arrange
            var client = new AgenticClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            Should.Throw<CheckoutArgumentException>(() => client.CreateAgenticCommerce(null));
        }

        [Fact]
        public void GetAgenticCommerce_Should_Throw_Exception_When_Id_Is_Null()
        {
            // Arrange
            var client = new AgenticClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            Should.Throw<CheckoutArgumentException>(() => client.GetAgenticCommerce(null));
        }

        [Fact]
        public void UpdateAgenticCommerce_Should_Throw_Exception_When_Id_Is_Null()
        {
            // Arrange
            var client = new AgenticClient(_apiClient.Object, _configuration.Object);
            var request = new UpdateAgenticCommerceRequest();

            // Act & Assert
            Should.Throw<CheckoutArgumentException>(() => client.UpdateAgenticCommerce(null, request));
        }

        [Fact]
        public void UpdateAgenticCommerce_Should_Throw_Exception_When_Request_Is_Null()
        {
            // Arrange
            var client = new AgenticClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            Should.Throw<CheckoutArgumentException>(() => client.UpdateAgenticCommerce("id", null));
        }

        [Fact]
        public void DeleteAgenticCommerce_Should_Throw_Exception_When_Id_Is_Null()
        {
            // Arrange
            var client = new AgenticClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            Should.Throw<CheckoutArgumentException>(() => client.DeleteAgenticCommerce(null));
        }
    }
}