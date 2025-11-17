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
        public async Task CreateAgentic_Should_Call_ApiClient_Post()
        {
            // Arrange
            var request = new CreateAgenticRequest
            {
                Name = "Test Agentic",
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

            var response = new CreateAgenticResponse();

            _apiClient.Setup(apiClient => 
                    apiClient.Post<CreateAgenticResponse>(
                    "agentic/commerce",
                    _authorization,
                    request,
                    CancellationToken.None,
                    null))
                .ReturnsAsync(response);
            
            var client = new AgenticClient(_apiClient.Object, _configuration.Object);

            // Act
            var result = await client.CreateAgentic(request);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
        }

        [Fact]
        public async Task GetAgentic_Should_Call_ApiClient_Get()
        {
            // Arrange
            const string agenticId = "agentic_commerce_id";
            var response = new GetAgenticResponse();

            _apiClient.Setup(apiClient => 
                    apiClient.Get<GetAgenticResponse>(
                    "agentic/commerce/agentic_commerce_id",
                    _authorization,
                    CancellationToken.None))
                .ReturnsAsync(response);
            
            var client = new AgenticClient(_apiClient.Object, _configuration.Object);

            // Act
            var result = await client.GetAgentic(agenticId);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
        }

        [Fact]
        public async Task UpdateAgentic_Should_Call_ApiClient_Patch()
        {
            // Arrange
            const string agenticId = "agentic_commerce_id";
            var request = new UpdateAgenticRequest
            {
                Name = "Updated Test Agentic",
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

            var response = new UpdateAgenticResponse();

            _apiClient.Setup(apiClient => 
                    apiClient.Patch<UpdateAgenticResponse>(
                    "agentic/commerce/agentic_commerce_id",
                    _authorization,
                    request,
                    CancellationToken.None,
                    null))
                .ReturnsAsync(response);
            
            var client = new AgenticClient(_apiClient.Object, _configuration.Object);

            // Act
            var result = await client.UpdateAgentic(agenticId, request);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
        }

        [Fact]
        public async Task DeleteAgentic_Should_Call_ApiClient_Delete()
        {
            // Arrange
            const string agenticId = "agentic_commerce_id";
            var response = new DeleteAgenticResponse();

            _apiClient.Setup(apiClient => 
                    apiClient.Delete<DeleteAgenticResponse>(
                    "agentic/commerce/agentic_commerce_id",
                    _authorization,
                    CancellationToken.None))
                .ReturnsAsync(response);
            
            var client = new AgenticClient(_apiClient.Object, _configuration.Object);

            // Act
            var result = await client.DeleteAgentic(agenticId);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
        }

        [Fact]
        public async Task GetAgentics_Should_Call_ApiClient_Query()
        {
            // Arrange
            var request = new GetAgenticsRequest
            {
                Skip = 0,
                Limit = 10,
                IsActive = true,
                NameFilter = "test",
                AiModelFilter = "gpt-4",
                SortBy = "created_at",
                SortDirection = "desc"
            };

            var response = new GetAgenticsResponse();

            _apiClient.Setup(apiClient => 
                    apiClient.Query<GetAgenticsResponse>(
                    "agentic/commerce",
                    _authorization,
                    request,
                    CancellationToken.None))
                .ReturnsAsync(response);
            
            var client = new AgenticClient(_apiClient.Object, _configuration.Object);

            // Act
            var result = await client.GetAgentics(request);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
        }

        [Fact]
        public async Task GetAgentics_Without_Request_Should_Call_ApiClient_Query()
        {
            // Arrange
            var response = new GetAgenticsResponse();

            _apiClient.Setup(apiClient => 
                    apiClient.Query<GetAgenticsResponse>(
                    "agentic/commerce",
                    _authorization,
                    null,
                    CancellationToken.None))
                .ReturnsAsync(response);
            
            var client = new AgenticClient(_apiClient.Object, _configuration.Object);

            // Act
            var result = await client.GetAgentics();

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
        }

        [Fact]
        public void CreateAgentic_Should_Throw_Exception_When_Request_Is_Null()
        {
            // Arrange
            var client = new AgenticClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            Should.Throw<CheckoutArgumentException>(() => client.CreateAgentic(null));
        }

        [Fact]
        public void GetAgentic_Should_Throw_Exception_When_Id_Is_Null()
        {
            // Arrange
            var client = new AgenticClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            Should.Throw<CheckoutArgumentException>(() => client.GetAgentic(null));
        }

        [Fact]
        public void UpdateAgentic_Should_Throw_Exception_When_Id_Is_Null()
        {
            // Arrange
            var client = new AgenticClient(_apiClient.Object, _configuration.Object);
            var request = new UpdateAgenticRequest();

            // Act & Assert
            Should.Throw<CheckoutArgumentException>(() => client.UpdateAgentic(null, request));
        }

        [Fact]
        public void UpdateAgentic_Should_Throw_Exception_When_Request_Is_Null()
        {
            // Arrange
            var client = new AgenticClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            Should.Throw<CheckoutArgumentException>(() => client.UpdateAgentic("id", null));
        }

        [Fact]
        public void DeleteAgentic_Should_Throw_Exception_When_Id_Is_Null()
        {
            // Arrange
            var client = new AgenticClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            Should.Throw<CheckoutArgumentException>(() => client.DeleteAgentic(null));
        }
    }
}