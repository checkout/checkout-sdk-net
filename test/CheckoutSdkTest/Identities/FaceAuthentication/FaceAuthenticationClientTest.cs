using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Checkout.Identities.Entities;
using Checkout.Identities.FaceAuthentication.Requests;
using Checkout.Identities.FaceAuthentication.Responses;
using Moq;
using Shouldly;
using Xunit;

namespace Checkout.Identities.FaceAuthentication
{
    public class FaceAuthenticationClientTest : UnitTestFixture
    {
        private const string FaceAuthenticationsPath = "face-authentications";
        private const string FaceAuthenticationId = "face_auth_12345";
        private const string AttemptId = "attempt_67890";
        
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.Default, ValidDefaultSk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.Default);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public FaceAuthenticationClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKeyOrOAuth))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);
        }

        [Fact]
        public async Task CreateFaceAuthentication_Should_Call_ApiClient_Post()
        {
            // Arrange
            var request = CreateFaceAuthenticationRequest();
            var response = CreateFaceAuthenticationResponse();
            
            _apiClient.Setup(apiClient =>
                    apiClient.Post<FaceAuthenticationResponse>(
                        FaceAuthenticationsPath,
                        _authorization,
                        request,
                        CancellationToken.None,
                        null))
                .ReturnsAsync(response);

            IFaceAuthenticationClient client = new FaceAuthenticationClient(_apiClient.Object, _configuration.Object);

            // Act
            FaceAuthenticationResponse result = await client.CreateFaceAuthentication(request, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
            ValidateFaceAuthenticationResponse(result);
        }

        [Fact]
        public async Task CreateFaceAuthentication_Should_Throw_When_Request_Null()
        {
            // Arrange
            IFaceAuthenticationClient client = new FaceAuthenticationClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(async () =>
                await client.CreateFaceAuthentication(null, CancellationToken.None));
            
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task GetFaceAuthentication_Should_Call_ApiClient_Get()
        {
            // Arrange
            var response = CreateFaceAuthenticationResponse();
            
            _apiClient.Setup(apiClient =>
                    apiClient.Get<FaceAuthenticationResponse>(
                        $"{FaceAuthenticationsPath}/{FaceAuthenticationId}",
                        _authorization,
                        CancellationToken.None))
                .ReturnsAsync(response);

            IFaceAuthenticationClient client = new FaceAuthenticationClient(_apiClient.Object, _configuration.Object);

            // Act
            FaceAuthenticationResponse result = await client.GetFaceAuthentication(FaceAuthenticationId, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
            ValidateFaceAuthenticationResponse(result);
        }

        [Fact]
        public async Task GetFaceAuthentication_Should_Throw_When_Id_Null_Or_Empty()
        {
            // Arrange
            IFaceAuthenticationClient client = new FaceAuthenticationClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(async () =>
                await client.GetFaceAuthentication(null, CancellationToken.None));
            
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task AnonymizeFaceAuthentication_Should_Call_ApiClient_Post()
        {
            // Arrange
            var response = CreateFaceAuthenticationResponse();
            
            _apiClient.Setup(apiClient =>
                    apiClient.Post<FaceAuthenticationResponse>(
                        $"{FaceAuthenticationsPath}/{FaceAuthenticationId}/anonymize",
                        _authorization,
                        (object)null,
                        CancellationToken.None,
                        null))
                .ReturnsAsync(response);

            IFaceAuthenticationClient client = new FaceAuthenticationClient(_apiClient.Object, _configuration.Object);

            // Act
            FaceAuthenticationResponse result = await client.AnonymizeFaceAuthentication(FaceAuthenticationId, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
            ValidateFaceAuthenticationResponse(result);
        }

        [Fact]
        public async Task AnonymizeFaceAuthentication_Should_Throw_When_Id_Null_Or_Empty()
        {
            // Arrange
            IFaceAuthenticationClient client = new FaceAuthenticationClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(async () =>
                await client.AnonymizeFaceAuthentication(null, CancellationToken.None));
            
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task CreateFaceAuthenticationAttempt_Should_Call_ApiClient_Post()
        {
            // Arrange
            var request = CreateFaceAuthenticationAttemptRequest();
            var response = CreateFaceAuthenticationAttemptResponse();
            
            _apiClient.Setup(apiClient =>
                    apiClient.Post<FaceAuthenticationAttemptResponse>(
                        $"{FaceAuthenticationsPath}/{FaceAuthenticationId}/attempts",
                        _authorization,
                        request,
                        CancellationToken.None,
                        null))
                .ReturnsAsync(response);

            IFaceAuthenticationClient client = new FaceAuthenticationClient(_apiClient.Object, _configuration.Object);

            // Act
            FaceAuthenticationAttemptResponse result = await client.CreateFaceAuthenticationAttempt(FaceAuthenticationId, request, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
            ValidateFaceAuthenticationAttemptResponse(result);
        }

        [Fact]
        public async Task CreateFaceAuthenticationAttempt_Should_Throw_When_FaceAuthenticationId_Null()
        {
            // Arrange
            var request = CreateFaceAuthenticationAttemptRequest();
            IFaceAuthenticationClient client = new FaceAuthenticationClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(async () =>
                await client.CreateFaceAuthenticationAttempt(null, request, CancellationToken.None));
            
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task CreateFaceAuthenticationAttempt_Should_Throw_When_Request_Null()
        {
            // Arrange
            IFaceAuthenticationClient client = new FaceAuthenticationClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(async () =>
                await client.CreateFaceAuthenticationAttempt(FaceAuthenticationId, null, CancellationToken.None));
            
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task GetFaceAuthenticationAttempts_Should_Call_ApiClient_Get()
        {
            // Arrange
            var response = CreateFaceAuthenticationAttemptsResponse();
            
            _apiClient.Setup(apiClient =>
                    apiClient.Get<FaceAuthenticationAttemptsResponse>(
                        $"{FaceAuthenticationsPath}/{FaceAuthenticationId}/attempts",
                        _authorization,
                        CancellationToken.None))
                .ReturnsAsync(response);

            IFaceAuthenticationClient client = new FaceAuthenticationClient(_apiClient.Object, _configuration.Object);

            // Act
            FaceAuthenticationAttemptsResponse result = await client.GetFaceAuthenticationAttempts(FaceAuthenticationId, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
            ValidateFaceAuthenticationAttemptsResponse(result);
        }

        [Fact]
        public async Task GetFaceAuthenticationAttempts_Should_Throw_When_Id_Null_Or_Empty()
        {
            // Arrange
            IFaceAuthenticationClient client = new FaceAuthenticationClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(async () =>
                await client.GetFaceAuthenticationAttempts(null, CancellationToken.None));
            
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task GetFaceAuthenticationAttempt_Should_Call_ApiClient_Get()
        {
            // Arrange
            var response = CreateFaceAuthenticationAttemptResponse();
            
            _apiClient.Setup(apiClient =>
                    apiClient.Get<FaceAuthenticationAttemptResponse>(
                        $"{FaceAuthenticationsPath}/{FaceAuthenticationId}/attempts/{AttemptId}",
                        _authorization,
                        CancellationToken.None))
                .ReturnsAsync(response);

            IFaceAuthenticationClient client = new FaceAuthenticationClient(_apiClient.Object, _configuration.Object);

            // Act
            FaceAuthenticationAttemptResponse result = await client.GetFaceAuthenticationAttempt(FaceAuthenticationId, AttemptId, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
            ValidateFaceAuthenticationAttemptResponse(result);
        }

        [Fact]
        public async Task GetFaceAuthenticationAttempt_Should_Throw_When_FaceAuthenticationId_Null()
        {
            // Arrange
            IFaceAuthenticationClient client = new FaceAuthenticationClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(async () =>
                await client.GetFaceAuthenticationAttempt(null, AttemptId, CancellationToken.None));
            
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task GetFaceAuthenticationAttempt_Should_Throw_When_AttemptId_Null()
        {
            // Arrange
            IFaceAuthenticationClient client = new FaceAuthenticationClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(async () =>
                await client.GetFaceAuthenticationAttempt(FaceAuthenticationId, null, CancellationToken.None));
            
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        private static FaceAuthenticationRequest CreateFaceAuthenticationRequest()
        {
            return new FaceAuthenticationRequest
            {
                ApplicantId = "app_12345",
                UserJourneyId = "uj_67890"
            };
        }

        private static FaceAuthenticationAttemptRequest CreateFaceAuthenticationAttemptRequest()
        {
            return new FaceAuthenticationAttemptRequest
            {
                RedirectUrl = "https://example.com/redirect",
                ClientInformation = new ClientInformation
                {
                    PreSelectedResidenceCountry = "US",
                    PreSelectedLanguage = "en-US"
                }
            };
        }

        private static FaceAuthenticationResponse CreateFaceAuthenticationResponse()
        {
            return new FaceAuthenticationResponse
            {
                Id = FaceAuthenticationId,
                UserJourneyId = "uj_67890",
                ApplicantId = "app_12345",
                Status = FaceAuthenticationStatus.Created,
                Face = new FaceImage
                {
                    ImageSignedUrl = "https://example.com/face-image.jpg"
                }
            };
        }

        private static FaceAuthenticationAttemptResponse CreateFaceAuthenticationAttemptResponse()
        {
            return new FaceAuthenticationAttemptResponse
            {
                Id = AttemptId,
                Status = FaceAuthenticationAttemptStatus.PendingRedirection,
                RedirectUrl = "https://example.com/redirect",
                ClientInformation = new ClientInformation
                {
                    PreSelectedResidenceCountry = "US",
                    PreSelectedLanguage = "en-US"
                }
            };
        }

        private static FaceAuthenticationAttemptsResponse CreateFaceAuthenticationAttemptsResponse()
        {
            return new FaceAuthenticationAttemptsResponse
            {
                TotalCount = 1,
                Skip = 0,
                Limit = 10,
                Data = new List<FaceAuthenticationAttemptResponse>
                {
                    CreateFaceAuthenticationAttemptResponse()
                }
            };
        }

        private static void ValidateFaceAuthenticationResponse(FaceAuthenticationResponse response)
        {
            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNullOrEmpty();
            response.UserJourneyId.ShouldNotBeNullOrEmpty();
            response.ApplicantId.ShouldNotBeNullOrEmpty();
            response.Status.ShouldNotBeNull();
        }

        private static void ValidateFaceAuthenticationAttemptResponse(FaceAuthenticationAttemptResponse response)
        {
            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNullOrEmpty();
            response.Status.ShouldNotBeNull();
        }

        private static void ValidateFaceAuthenticationAttemptsResponse(FaceAuthenticationAttemptsResponse response)
        {
            response.ShouldNotBeNull();
            response.Data.ShouldNotBeNull();
            response.TotalCount.ShouldBeGreaterThanOrEqualTo(0);
            response.Skip.ShouldBeGreaterThanOrEqualTo(0);
            response.Limit.ShouldBeGreaterThanOrEqualTo(0);
        }
    }
}