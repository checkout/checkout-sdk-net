using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Checkout.Identities.Entities;
using Checkout.Identities.IdentityVerification.Requests;
using Checkout.Identities.IdentityVerification.Responses;
using Moq;
using Shouldly;
using Xunit;

namespace Checkout.Identities.IdentityVerification
{
    public class IdentityVerificationClientTest : UnitTestFixture
    {
        private const string CreateAndOpenPath = "create-and-open-idv";
        private const string IdentityVerificationsPath = "identity-verifications";
        private const string IdentityVerificationId = "idv_12345";
        private const string AttemptId = "attempt_67890";
        
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.Default, ValidDefaultSk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.Default);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public IdentityVerificationClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKeyOrOAuth))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);
        }

        [Fact]
        public async Task CreateIdentityVerificationAndAttempt_Should_Call_ApiClient_Post()
        {
            // Arrange
            var request = CreateIdentityVerificationAndAttemptRequest();
            var response = CreateIdentityVerificationAndAttemptResponse();
            
            _apiClient.Setup(apiClient =>
                    apiClient.Post<IdentityVerificationAndAttemptResponse>(
                        CreateAndOpenPath,
                        _authorization,
                        request,
                        CancellationToken.None,
                        null))
                .ReturnsAsync(response);

            IIdentityVerificationClient client = new IdentityVerificationClient(_apiClient.Object, _configuration.Object);

            // Act
            IdentityVerificationAndAttemptResponse result = await client.CreateIdentityVerificationAndAttempt(request, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
            ValidateIdentityVerificationAndAttemptResponse(result);
        }

        [Fact]
        public async Task CreateIdentityVerificationAndAttempt_Should_Throw_When_Request_Null()
        {
            // Arrange
            IIdentityVerificationClient client = new IdentityVerificationClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(async () =>
                await client.CreateIdentityVerificationAndAttempt(null, CancellationToken.None));
            
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task CreateIdentityVerification_Should_Call_ApiClient_Post()
        {
            // Arrange
            var request = CreateIdentityVerificationRequest();
            var response = CreateIdentityVerificationResponse();
            
            _apiClient.Setup(apiClient =>
                    apiClient.Post<IdentityVerificationResponse>(
                        IdentityVerificationsPath,
                        _authorization,
                        request,
                        CancellationToken.None,
                        null))
                .ReturnsAsync(response);

            IIdentityVerificationClient client = new IdentityVerificationClient(_apiClient.Object, _configuration.Object);

            // Act
            IdentityVerificationResponse result = await client.CreateIdentityVerification(request, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
            ValidateIdentityVerificationResponse(result);
        }

        [Fact]
        public async Task CreateIdentityVerification_Should_Throw_When_Request_Null()
        {
            // Arrange
            IIdentityVerificationClient client = new IdentityVerificationClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(async () =>
                await client.CreateIdentityVerification(null, CancellationToken.None));
            
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task GetIdentityVerification_Should_Call_ApiClient_Get()
        {
            // Arrange
            var response = CreateIdentityVerificationResponse();
            
            _apiClient.Setup(apiClient =>
                    apiClient.Get<IdentityVerificationResponse>(
                        $"{IdentityVerificationsPath}/{IdentityVerificationId}",
                        _authorization,
                        CancellationToken.None))
                .ReturnsAsync(response);

            IIdentityVerificationClient client = new IdentityVerificationClient(_apiClient.Object, _configuration.Object);

            // Act
            IdentityVerificationResponse result = await client.GetIdentityVerification(IdentityVerificationId, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
            ValidateIdentityVerificationResponse(result);
        }

        [Fact]
        public async Task GetIdentityVerification_Should_Throw_When_Id_Null_Or_Empty()
        {
            // Arrange
            IIdentityVerificationClient client = new IdentityVerificationClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(async () =>
                await client.GetIdentityVerification(null, CancellationToken.None));
            
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task AnonymizeIdentityVerification_Should_Call_ApiClient_Post()
        {
            // Arrange
            var response = CreateIdentityVerificationResponse();
            
            _apiClient.Setup(apiClient =>
                    apiClient.Post<IdentityVerificationResponse>(
                        $"{IdentityVerificationsPath}/{IdentityVerificationId}/anonymize",
                        _authorization,
                        (object)null,
                        CancellationToken.None,
                        null))
                .ReturnsAsync(response);

            IIdentityVerificationClient client = new IdentityVerificationClient(_apiClient.Object, _configuration.Object);

            // Act
            IdentityVerificationResponse result = await client.AnonymizeIdentityVerification(IdentityVerificationId, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
            ValidateIdentityVerificationResponse(result);
        }

        [Fact]
        public async Task AnonymizeIdentityVerification_Should_Throw_When_Id_Null_Or_Empty()
        {
            // Arrange
            IIdentityVerificationClient client = new IdentityVerificationClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(async () =>
                await client.AnonymizeIdentityVerification(null, CancellationToken.None));
            
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task CreateIdentityVerificationAttempt_Should_Call_ApiClient_Post()
        {
            // Arrange
            var request = CreateIdentityVerificationAttemptRequest();
            var response = CreateIdentityVerificationAttemptResponse();
            
            _apiClient.Setup(apiClient =>
                    apiClient.Post<IdentityVerificationAttemptResponse>(
                        $"{IdentityVerificationsPath}/{IdentityVerificationId}/attempts",
                        _authorization,
                        request,
                        CancellationToken.None,
                        null))
                .ReturnsAsync(response);

            IIdentityVerificationClient client = new IdentityVerificationClient(_apiClient.Object, _configuration.Object);

            // Act
            IdentityVerificationAttemptResponse result = await client.CreateIdentityVerificationAttempt(IdentityVerificationId, request, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
            ValidateIdentityVerificationAttemptResponse(result);
        }

        [Fact]
        public async Task CreateIdentityVerificationAttempt_Should_Throw_When_IdentityVerificationId_Null()
        {
            // Arrange
            var request = CreateIdentityVerificationAttemptRequest();
            IIdentityVerificationClient client = new IdentityVerificationClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(async () =>
                await client.CreateIdentityVerificationAttempt(null, request, CancellationToken.None));
            
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task CreateIdentityVerificationAttempt_Should_Throw_When_Request_Null()
        {
            // Arrange
            IIdentityVerificationClient client = new IdentityVerificationClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(async () =>
                await client.CreateIdentityVerificationAttempt(IdentityVerificationId, null, CancellationToken.None));
            
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task GetIdentityVerificationAttempts_Should_Call_ApiClient_Get()
        {
            // Arrange
            var response = CreateIdentityVerificationAttemptsResponse();
            
            _apiClient.Setup(apiClient =>
                    apiClient.Get<IdentityVerificationAttemptsResponse>(
                        $"{IdentityVerificationsPath}/{IdentityVerificationId}/attempts",
                        _authorization,
                        CancellationToken.None))
                .ReturnsAsync(response);

            IIdentityVerificationClient client = new IdentityVerificationClient(_apiClient.Object, _configuration.Object);

            // Act
            IdentityVerificationAttemptsResponse result = await client.GetIdentityVerificationAttempts(IdentityVerificationId, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
            ValidateIdentityVerificationAttemptsResponse(result);
        }

        [Fact]
        public async Task GetIdentityVerificationAttempts_Should_Throw_When_Id_Null_Or_Empty()
        {
            // Arrange
            IIdentityVerificationClient client = new IdentityVerificationClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(async () =>
                await client.GetIdentityVerificationAttempts(null, CancellationToken.None));
            
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task GetIdentityVerificationAttempt_Should_Call_ApiClient_Get()
        {
            // Arrange
            var response = CreateIdentityVerificationAttemptResponse();
            
            _apiClient.Setup(apiClient =>
                    apiClient.Get<IdentityVerificationAttemptResponse>(
                        $"{IdentityVerificationsPath}/{IdentityVerificationId}/attempts/{AttemptId}",
                        _authorization,
                        CancellationToken.None))
                .ReturnsAsync(response);

            IIdentityVerificationClient client = new IdentityVerificationClient(_apiClient.Object, _configuration.Object);

            // Act
            IdentityVerificationAttemptResponse result = await client.GetIdentityVerificationAttempt(IdentityVerificationId, AttemptId, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
            ValidateIdentityVerificationAttemptResponse(result);
        }

        [Fact]
        public async Task GetIdentityVerificationAttempt_Should_Throw_When_IdentityVerificationId_Null()
        {
            // Arrange
            IIdentityVerificationClient client = new IdentityVerificationClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(async () =>
                await client.GetIdentityVerificationAttempt(null, AttemptId, CancellationToken.None));
            
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task GetIdentityVerificationAttempt_Should_Throw_When_AttemptId_Null()
        {
            // Arrange
            IIdentityVerificationClient client = new IdentityVerificationClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(async () =>
                await client.GetIdentityVerificationAttempt(IdentityVerificationId, null, CancellationToken.None));
            
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task GetIdentityVerificationReport_Should_Call_ApiClient_Get()
        {
            // Arrange
            var response = CreateIdentityVerificationReportResponse();
            
            _apiClient.Setup(apiClient =>
                    apiClient.Get<IdentityVerificationReportResponse>(
                        $"{IdentityVerificationsPath}/{IdentityVerificationId}/pdf-report",
                        _authorization,
                        CancellationToken.None))
                .ReturnsAsync(response);

            IIdentityVerificationClient client = new IdentityVerificationClient(_apiClient.Object, _configuration.Object);

            // Act
            IdentityVerificationReportResponse result = await client.GetIdentityVerificationReport(IdentityVerificationId, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
            ValidateIdentityVerificationReportResponse(result);
        }

        [Fact]
        public async Task GetIdentityVerificationReport_Should_Throw_When_Id_Null_Or_Empty()
        {
            // Arrange
            IIdentityVerificationClient client = new IdentityVerificationClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(async () =>
                await client.GetIdentityVerificationReport(null, CancellationToken.None));
            
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        private static IdentityVerificationAndAttemptRequest CreateIdentityVerificationAndAttemptRequest()
        {
            return new IdentityVerificationAndAttemptRequest
            {
                ApplicantId = "app_12345",
                UserJourneyId = "uj_67890",
                RedirectUrl = "https://example.com/redirect",
                DeclaredData = new DeclaredData
                {
                    Name = "John Doe"
                }
            };
        }

        private static IdentityVerificationRequest CreateIdentityVerificationRequest()
        {
            return new IdentityVerificationRequest
            {
                ApplicantId = "app_12345",
                UserJourneyId = "uj_67890",
                DeclaredData = new DeclaredData
                {
                    Name = "John Doe"
                }
            };
        }

        private static IdentityVerificationAttemptRequest CreateIdentityVerificationAttemptRequest()
        {
            return new IdentityVerificationAttemptRequest
            {
                RedirectUrl = "https://example.com/redirect",
                ClientInformation = new ClientInformation
                {
                    PreSelectedResidenceCountry = "US",
                    PreSelectedLanguage = "en-US"
                }
            };
        }

        private static IdentityVerificationAndAttemptResponse CreateIdentityVerificationAndAttemptResponse()
        {
            return new IdentityVerificationAndAttemptResponse
            {
                Id = IdentityVerificationId,
                UserJourneyId = "uj_67890",
                ApplicantId = "app_12345",
                Status = IdentityVerificationStatus.Pending,
                RedirectUrl = "https://example.com/redirect",
                DeclaredData = new DeclaredData
                {
                    Name = "John Doe"
                },
                Documents = new List<DocumentDetails>
                {
                    new DocumentDetails
                    {
                        DocumentType = DocumentType.Passport,
                        DocumentIssuingCountry = "US",
                        FrontImageSignedUrl = "https://example.com/front-image.jpg",
                        FullName = "John Doe",
                        BirthDate = "1990-01-01"
                    }
                },
                FaceImage = new FaceImage
                {
                    ImageSignedUrl = "https://example.com/face-image.jpg"
                },
                VerifiedIdentity = new VerifiedIdentity
                {
                    FullName = "John Doe",
                    BirthDate = "1990-01-01"
                }
            };
        }

        private static IdentityVerificationResponse CreateIdentityVerificationResponse()
        {
            return new IdentityVerificationResponse
            {
                Id = IdentityVerificationId,
                UserJourneyId = "uj_67890",
                ApplicantId = "app_12345",
                Status = IdentityVerificationStatus.Pending,
                DeclaredData = new DeclaredData
                {
                    Name = "John Doe"
                },
                Documents = new List<DocumentDetails>
                {
                    new DocumentDetails
                    {
                        DocumentType = DocumentType.Passport,
                        DocumentIssuingCountry = "US",
                        FrontImageSignedUrl = "https://example.com/front-image.jpg",
                        FullName = "John Doe",
                        BirthDate = "1990-01-01"
                    }
                },
                FaceImage = new FaceImage
                {
                    ImageSignedUrl = "https://example.com/face-image.jpg"
                },
                VerifiedIdentity = new VerifiedIdentity
                {
                    FullName = "John Doe",
                    BirthDate = "1990-01-01"
                }
            };
        }

        private static IdentityVerificationAttemptResponse CreateIdentityVerificationAttemptResponse()
        {
            return new IdentityVerificationAttemptResponse
            {
                Id = AttemptId,
                Status = AttemptVerificationStatus.Completed,
                RedirectUrl = "https://example.com/redirect",
                ClientInformation = new ClientInformation
                {
                    PreSelectedResidenceCountry = "US",
                    PreSelectedLanguage = "en-US"
                },
                DeclaredData = new DeclaredData
                {
                    Name = "John Doe"
                }
            };
        }

        private static IdentityVerificationAttemptsResponse CreateIdentityVerificationAttemptsResponse()
        {
            return new IdentityVerificationAttemptsResponse
            {
                TotalCount = 1,
                Skip = 0,
                Limit = 10,
                Data = new List<IdentityVerificationAttemptResponse>
                {
                    CreateIdentityVerificationAttemptResponse()
                }
            };
        }

        private static IdentityVerificationReportResponse CreateIdentityVerificationReportResponse()
        {
            return new IdentityVerificationReportResponse
            {
                SignedUrl = "https://example.com/report.pdf"
            };
        }

        private static void ValidateIdentityVerificationAndAttemptResponse(IdentityVerificationAndAttemptResponse response)
        {
            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNullOrEmpty();
            response.UserJourneyId.ShouldNotBeNullOrEmpty();
            response.ApplicantId.ShouldNotBeNullOrEmpty();
            response.Status.ShouldNotBeNull();
            response.RedirectUrl.ShouldNotBeNullOrEmpty();
        }

        private static void ValidateIdentityVerificationResponse(IdentityVerificationResponse response)
        {
            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNullOrEmpty();
            response.UserJourneyId.ShouldNotBeNullOrEmpty();
            response.ApplicantId.ShouldNotBeNullOrEmpty();
            response.Status.ShouldNotBeNull();
        }

        private static void ValidateIdentityVerificationAttemptResponse(IdentityVerificationAttemptResponse response)
        {
            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNullOrEmpty();
            response.Status.ShouldNotBeNull();
        }

        private static void ValidateIdentityVerificationAttemptsResponse(IdentityVerificationAttemptsResponse response)
        {
            response.ShouldNotBeNull();
            response.Data.ShouldNotBeNull();
            response.TotalCount.ShouldBeGreaterThanOrEqualTo(0);
            response.Skip.ShouldBeGreaterThanOrEqualTo(0);
            response.Limit.ShouldBeGreaterThanOrEqualTo(0);
        }

        private static void ValidateIdentityVerificationReportResponse(IdentityVerificationReportResponse response)
        {
            response.ShouldNotBeNull();
            response.SignedUrl.ShouldNotBeNullOrEmpty();
        }
    }
}