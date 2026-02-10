using Checkout.Identities.AmlScreening.Requests;
using Checkout.Identities.AmlScreening.Responses;
using Checkout.Identities.Entities;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Identities.AmlScreening
{
    public class AmlScreeningClientTest : UnitTestFixture
    {
        private const string AmlPath = "aml-verifications";
        private const string ScreeningId = "scr_7hr7swleu6guzjqesyxmyodnya";
        
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.Default, ValidDefaultSk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.Default);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public AmlScreeningClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKeyOrOAuth))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);
        }

        [Fact]
        public async Task CreateAmlScreening_Should_Call_ApiClient_Post()
        {
            // Arrange
            var request = CreateAmlScreeningRequest();
            var response = CreateAmlScreeningResponse();
            
            _apiClient.Setup(apiClient =>
                    apiClient.Post<AmlScreeningResponse>(
                        AmlPath,
                        _authorization,
                        request,
                        CancellationToken.None,
                        null))
                .ReturnsAsync(response);

            IAmlScreeningClient client = new AmlScreeningClient(_apiClient.Object, _configuration.Object);

            // Act
            AmlScreeningResponse result = await client.CreateAmlScreening(request, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
            ValidateAmlScreeningResponse(result);
        }

        [Fact]
        public async Task GetAmlScreening_Should_Call_ApiClient_Get()
        {
            // Arrange
            var response = CreateAmlScreeningResponse();
            
            _apiClient.Setup(apiClient =>
                    apiClient.Get<AmlScreeningResponse>(
                        $"{AmlPath}/{ScreeningId}",
                        _authorization,
                        CancellationToken.None))
                .ReturnsAsync(response);

            IAmlScreeningClient client = new AmlScreeningClient(_apiClient.Object, _configuration.Object);

            // Act
            AmlScreeningResponse result = await client.GetAmlScreening(ScreeningId);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
            ValidateAmlScreeningResponse(result);
        }

        [Fact]
        public async Task CreateAmlScreening_Should_Throw_CheckoutArgumentException_When_Request_Is_Null()
        {
            // Arrange
            IAmlScreeningClient client = new AmlScreeningClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(
                async () => await client.CreateAmlScreening(null)
            );
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task GetAmlScreening_Should_Throw_CheckoutArgumentException_When_ScreeningId_Is_Null()
        {
            // Arrange
            IAmlScreeningClient client = new AmlScreeningClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(
                async () => await client.GetAmlScreening(null)
            );
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task GetAmlScreening_Should_Throw_CheckoutArgumentException_When_ScreeningId_Is_Empty()
        {
            // Arrange
            IAmlScreeningClient client = new AmlScreeningClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(
                async () => await client.GetAmlScreening("")
            );
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task CreateAmlScreening_Should_Handle_CancellationToken()
        {
            // Arrange
            var request = CreateAmlScreeningRequest();
            var response = CreateAmlScreeningResponse();
            var cancellationToken = new CancellationToken();
            
            _apiClient.Setup(apiClient =>
                    apiClient.Post<AmlScreeningResponse>(
                        AmlPath,
                        _authorization,
                        request,
                        cancellationToken,
                        null))
                .ReturnsAsync(response);

            IAmlScreeningClient client = new AmlScreeningClient(_apiClient.Object, _configuration.Object);

            // Act
            AmlScreeningResponse result = await client.CreateAmlScreening(request, cancellationToken);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
        }

        [Fact]
        public void Constructor_Should_Initialize_Properly()
        {
            // Act
            var client = new AmlScreeningClient(_apiClient.Object, _configuration.Object);

            // Assert
            client.ShouldNotBeNull();
        }

        private static AmlScreeningRequest CreateAmlScreeningRequest()
        {
            return new AmlScreeningRequest
            {
                ApplicantId = "aplt_7hr7swleu6guzjqesyxmyodnya",
                SearchParameters = new SearchParameters
                {
                    ConfigurationIdentifier = "config_123456"
                },
                Monitored = true
            };
        }

        private static AmlScreeningResponse CreateAmlScreeningResponse()
        {
            return new AmlScreeningResponse
            {
                Id = ScreeningId,
                ApplicantId = "aplt_7hr7swleu6guzjqesyxmyodnya",
                Status = AmlScreeningStatus.Created,
                SearchParameters = new SearchParameters
                {
                    ConfigurationIdentifier = "config_123456"
                },
                Monitored = true,
                CreatedOn = DateTime.UtcNow.AddMinutes(-5),
                ModifiedOn = DateTime.UtcNow
            };
        }

        private static void ValidateAmlScreeningResponse(AmlScreeningResponse response)
        {
            response.Id.ShouldNotBeNullOrEmpty();
            response.ApplicantId.ShouldNotBeNullOrEmpty();
            response.Status.ShouldNotBeNull();
            response.SearchParameters.ShouldNotBeNull();
            response.SearchParameters.ConfigurationIdentifier.ShouldNotBeNullOrEmpty();
            response.Monitored.ShouldNotBeNull();
            response.CreatedOn.ShouldNotBeNull();
            response.ModifiedOn.ShouldNotBeNull();
        }
    }
}