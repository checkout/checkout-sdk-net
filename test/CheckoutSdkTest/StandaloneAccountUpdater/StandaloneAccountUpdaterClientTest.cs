using System.Threading;
using System.Threading.Tasks;
using Moq;
using Shouldly;
using Xunit;

using Checkout.StandaloneAccountUpdater.Entities;
using Checkout.StandaloneAccountUpdater.Requests;
using Checkout.StandaloneAccountUpdater.Responses;

namespace Checkout.StandaloneAccountUpdater
{
    public class StandaloneAccountUpdaterClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.DefaultOAuth, ValidDefaultSk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.DefaultOAuth);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public StandaloneAccountUpdaterClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.OAuth))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);
        }

        [Fact]
        public async Task GetUpdatedCardCredentials_WithValidRequest_ShouldReturnResponse()
        {
            // Arrange
            var request = CreateValidRequest();
            var expectedResponse = CreateSampleResponse();
            
            _apiClient.Setup(apiClient => 
                    apiClient.Post<GetUpdatedCardCredentialsResponse>(
                    "account-updater/cards",
                    _authorization,
                    request,
                    It.IsAny<CancellationToken>(),
                    It.IsAny<string>()))
                .ReturnsAsync(expectedResponse);
            
            IStandaloneAccountUpdaterClient client = new StandaloneAccountUpdaterClient(_apiClient.Object, _configuration.Object);

            // Act
            var response = await client.GetUpdatedCardCredentials(request);

            // Assert
            response.ShouldNotBeNull();
            response.ShouldBeSameAs(expectedResponse);
        }

        [Fact]
        public async Task GetUpdatedCardCredentials_WithNullRequest_ShouldThrowArgumentException()
        {
            // Arrange
            IStandaloneAccountUpdaterClient client = new StandaloneAccountUpdaterClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            await Should.ThrowAsync<CheckoutArgumentException>(async () => 
                await client.GetUpdatedCardCredentials(null));
        }

        [Fact]
        public async Task GetUpdatedCardCredentials_WithCancellationToken_ShouldPassTokenToApiClient()
        {
            // Arrange
            var request = CreateValidRequest();
            var expectedResponse = CreateSampleResponse();
            var cancellationToken = new CancellationToken(true);
            
            _apiClient.Setup(apiClient => 
                    apiClient.Post<GetUpdatedCardCredentialsResponse>(
                    "account-updater/cards",
                    _authorization,
                    request,
                    cancellationToken,
                    It.IsAny<string>()))
                .ReturnsAsync(expectedResponse);
            
            IStandaloneAccountUpdaterClient client = new StandaloneAccountUpdaterClient(_apiClient.Object, _configuration.Object);

            // Act
            var response = await client.GetUpdatedCardCredentials(request, cancellationToken);

            // Assert
            response.ShouldNotBeNull();
            _apiClient.Verify(apiClient => apiClient.Post<GetUpdatedCardCredentialsResponse>(
                "account-updater/cards",
                _authorization,
                request,
                cancellationToken,
                It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task GetUpdatedCardCredentials_ShouldUseCorrectEndpointPath()
        {
            // Arrange
            var request = CreateValidRequest();
            var expectedResponse = CreateSampleResponse();
            
            _apiClient.Setup(apiClient => 
                    apiClient.Post<GetUpdatedCardCredentialsResponse>(
                    It.IsAny<string>(),
                    _authorization,
                    request,
                    It.IsAny<CancellationToken>(),
                    It.IsAny<string>()))
                .ReturnsAsync(expectedResponse);
            
            IStandaloneAccountUpdaterClient client = new StandaloneAccountUpdaterClient(_apiClient.Object, _configuration.Object);

            // Act
            await client.GetUpdatedCardCredentials(request);

            // Assert
            _apiClient.Verify(apiClient => apiClient.Post<GetUpdatedCardCredentialsResponse>(
                "account-updater/cards",
                _authorization,
                request,
                It.IsAny<CancellationToken>(),
                It.IsAny<string>()), Times.Once);
        }

        // Test data builders
        private static GetUpdatedCardCredentialsRequest CreateValidRequest()
        {
            return new GetUpdatedCardCredentialsRequest
            {
                SourceOptions = new SourceOptions
                {
                    Card = new CardDetails
                    {
                        Number = "4242424242424242",
                        ExpiryMonth = 12,
                        ExpiryYear = 2025
                    }
                }
            };
        }

        private static GetUpdatedCardCredentialsResponse CreateSampleResponse()
        {
            return new GetUpdatedCardCredentialsResponse
            {
                AccountUpdateStatus = AccountUpdateStatus.CardUpdated,
                Card = new CardUpdated
                {
                    ExpiryMonth = 12,
                    ExpiryYear = 2026
                }
            };
        }
    }
}