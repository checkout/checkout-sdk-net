using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Checkout.Identities.Entities;
using Checkout.Identities.IdDocumentVerification.Requests;
using Checkout.Identities.IdDocumentVerification.Responses;
using Moq;
using Shouldly;
using Xunit;

namespace Checkout.Identities.IdDocumentVerification
{
    public class IdDocumentVerificationClientTest : UnitTestFixture
    {
        private const string IdDocumentVerificationsPath = "id-document-verifications";
        private const string IdDocumentVerificationId = "iddoc_12345";
        private const string AttemptId = "attempt_67890";
        
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.Default, ValidDefaultSk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.Default);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public IdDocumentVerificationClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKeyOrOAuth))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);
        }

        [Fact]
        public async Task CreateIdDocumentVerification_Should_Call_ApiClient_Post()
        {
            // Arrange
            var request = CreateIdDocumentVerificationRequest();
            var response = CreateIdDocumentVerificationResponse();
            
            _apiClient.Setup(apiClient =>
                    apiClient.Post<IdDocumentVerificationResponse>(
                        IdDocumentVerificationsPath,
                        _authorization,
                        request,
                        CancellationToken.None,
                        null))
                .ReturnsAsync(response);

            IIdDocumentVerificationClient client = new IdDocumentVerificationClient(_apiClient.Object, _configuration.Object);

            // Act
            IdDocumentVerificationResponse result = await client.CreateIdDocumentVerification(request, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
            ValidateIdDocumentVerificationResponse(result);
        }

        [Fact]
        public async Task CreateIdDocumentVerification_Should_Throw_When_Request_Null()
        {
            // Arrange
            IIdDocumentVerificationClient client = new IdDocumentVerificationClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(async () =>
                await client.CreateIdDocumentVerification(null, CancellationToken.None));
            
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task GetIdDocumentVerification_Should_Call_ApiClient_Get()
        {
            // Arrange
            var response = CreateIdDocumentVerificationResponse();
            
            _apiClient.Setup(apiClient =>
                    apiClient.Get<IdDocumentVerificationResponse>(
                        $"{IdDocumentVerificationsPath}/{IdDocumentVerificationId}",
                        _authorization,
                        CancellationToken.None))
                .ReturnsAsync(response);

            IIdDocumentVerificationClient client = new IdDocumentVerificationClient(_apiClient.Object, _configuration.Object);

            // Act
            IdDocumentVerificationResponse result = await client.GetIdDocumentVerification(IdDocumentVerificationId, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
            ValidateIdDocumentVerificationResponse(result);
        }

        [Fact]
        public async Task GetIdDocumentVerification_Should_Throw_When_Id_Null_Or_Empty()
        {
            // Arrange
            IIdDocumentVerificationClient client = new IdDocumentVerificationClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(async () =>
                await client.GetIdDocumentVerification(null, CancellationToken.None));
            
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task AnonymizeIdDocumentVerification_Should_Call_ApiClient_Post()
        {
            // Arrange
            var response = CreateIdDocumentVerificationResponse();
            
            _apiClient.Setup(apiClient =>
                    apiClient.Post<IdDocumentVerificationResponse>(
                        $"{IdDocumentVerificationsPath}/{IdDocumentVerificationId}/anonymize",
                        _authorization,
                        (object)null,
                        CancellationToken.None,
                        null))
                .ReturnsAsync(response);

            IIdDocumentVerificationClient client = new IdDocumentVerificationClient(_apiClient.Object, _configuration.Object);

            // Act
            IdDocumentVerificationResponse result = await client.AnonymizeIdDocumentVerification(IdDocumentVerificationId, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
            ValidateIdDocumentVerificationResponse(result);
        }

        [Fact]
        public async Task AnonymizeIdDocumentVerification_Should_Throw_When_Id_Null_Or_Empty()
        {
            // Arrange
            IIdDocumentVerificationClient client = new IdDocumentVerificationClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(async () =>
                await client.AnonymizeIdDocumentVerification(null, CancellationToken.None));
            
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task CreateIdDocumentVerificationAttempt_Should_Call_ApiClient_Post()
        {
            // Arrange
            var request = CreateIdDocumentVerificationAttemptRequest();
            var response = CreateIdDocumentVerificationAttemptResponse();
            
            _apiClient.Setup(apiClient =>
                    apiClient.Post<IdDocumentVerificationAttemptResponse>(
                        $"{IdDocumentVerificationsPath}/{IdDocumentVerificationId}/attempts",
                        _authorization,
                        request,
                        CancellationToken.None,
                        null))
                .ReturnsAsync(response);

            IIdDocumentVerificationClient client = new IdDocumentVerificationClient(_apiClient.Object, _configuration.Object);

            // Act
            IdDocumentVerificationAttemptResponse result = await client.CreateIdDocumentVerificationAttempt(IdDocumentVerificationId, request, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
            ValidateIdDocumentVerificationAttemptResponse(result);
        }

        [Fact]
        public async Task CreateIdDocumentVerificationAttempt_Should_Throw_When_IdDocumentVerificationId_Null()
        {
            // Arrange
            var request = CreateIdDocumentVerificationAttemptRequest();
            IIdDocumentVerificationClient client = new IdDocumentVerificationClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(async () =>
                await client.CreateIdDocumentVerificationAttempt(null, request, CancellationToken.None));
            
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task CreateIdDocumentVerificationAttempt_Should_Throw_When_Request_Null()
        {
            // Arrange
            IIdDocumentVerificationClient client = new IdDocumentVerificationClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(async () =>
                await client.CreateIdDocumentVerificationAttempt(IdDocumentVerificationId, null, CancellationToken.None));
            
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task GetIdDocumentVerificationAttempts_Should_Call_ApiClient_Get()
        {
            // Arrange
            var response = CreateIdDocumentVerificationAttemptsResponse();
            
            _apiClient.Setup(apiClient =>
                    apiClient.Get<IdDocumentVerificationAttemptsResponse>(
                        $"{IdDocumentVerificationsPath}/{IdDocumentVerificationId}/attempts",
                        _authorization,
                        CancellationToken.None))
                .ReturnsAsync(response);

            IIdDocumentVerificationClient client = new IdDocumentVerificationClient(_apiClient.Object, _configuration.Object);

            // Act
            IdDocumentVerificationAttemptsResponse result = await client.GetIdDocumentVerificationAttempts(IdDocumentVerificationId, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
            ValidateIdDocumentVerificationAttemptsResponse(result);
        }

        [Fact]
        public async Task GetIdDocumentVerificationAttempts_Should_Throw_When_Id_Null_Or_Empty()
        {
            // Arrange
            IIdDocumentVerificationClient client = new IdDocumentVerificationClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(async () =>
                await client.GetIdDocumentVerificationAttempts(null, CancellationToken.None));
            
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task GetIdDocumentVerificationAttempt_Should_Call_ApiClient_Get()
        {
            // Arrange
            var response = CreateIdDocumentVerificationAttemptResponse();
            
            _apiClient.Setup(apiClient =>
                    apiClient.Get<IdDocumentVerificationAttemptResponse>(
                        $"{IdDocumentVerificationsPath}/{IdDocumentVerificationId}/attempts/{AttemptId}",
                        _authorization,
                        CancellationToken.None))
                .ReturnsAsync(response);

            IIdDocumentVerificationClient client = new IdDocumentVerificationClient(_apiClient.Object, _configuration.Object);

            // Act
            IdDocumentVerificationAttemptResponse result = await client.GetIdDocumentVerificationAttempt(IdDocumentVerificationId, AttemptId, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
            ValidateIdDocumentVerificationAttemptResponse(result);
        }

        [Fact]
        public async Task GetIdDocumentVerificationAttempt_Should_Throw_When_IdDocumentVerificationId_Null()
        {
            // Arrange
            IIdDocumentVerificationClient client = new IdDocumentVerificationClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(async () =>
                await client.GetIdDocumentVerificationAttempt(null, AttemptId, CancellationToken.None));
            
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task GetIdDocumentVerificationAttempt_Should_Throw_When_AttemptId_Null()
        {
            // Arrange
            IIdDocumentVerificationClient client = new IdDocumentVerificationClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(async () =>
                await client.GetIdDocumentVerificationAttempt(IdDocumentVerificationId, null, CancellationToken.None));
            
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task GetIdDocumentVerificationReport_Should_Call_ApiClient_Get()
        {
            // Arrange
            var response = CreateIdDocumentVerificationReportResponse();
            
            _apiClient.Setup(apiClient =>
                    apiClient.Get<IdDocumentVerificationReportResponse>(
                        $"{IdDocumentVerificationsPath}/{IdDocumentVerificationId}/pdf-report",
                        _authorization,
                        CancellationToken.None))
                .ReturnsAsync(response);

            IIdDocumentVerificationClient client = new IdDocumentVerificationClient(_apiClient.Object, _configuration.Object);

            // Act
            IdDocumentVerificationReportResponse result = await client.GetIdDocumentVerificationReport(IdDocumentVerificationId, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
            ValidateIdDocumentVerificationReportResponse(result);
        }

        [Fact]
        public async Task GetIdDocumentVerificationReport_Should_Throw_When_Id_Null_Or_Empty()
        {
            // Arrange
            IIdDocumentVerificationClient client = new IdDocumentVerificationClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(async () =>
                await client.GetIdDocumentVerificationReport(null, CancellationToken.None));
            
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        private static IdDocumentVerificationRequest CreateIdDocumentVerificationRequest()
        {
            return new IdDocumentVerificationRequest
            {
                ApplicantId = "app_12345",
                UserJourneyId = "uj_67890",
                DeclaredData = new DeclaredData
                {
                    Name = "John Doe"
                }
            };
        }

        private static IdDocumentVerificationAttemptRequest CreateIdDocumentVerificationAttemptRequest()
        {
            return new IdDocumentVerificationAttemptRequest
            {
                DocumentFront = "base64-encoded-front-image",
                DocumentBack = "base64-encoded-back-image"
            };
        }

        private static IdDocumentVerificationResponse CreateIdDocumentVerificationResponse()
        {
            return new IdDocumentVerificationResponse
            {
                Id = IdDocumentVerificationId,
                UserJourneyId = "uj_67890",
                ApplicantId = "app_12345",
                Status = IdDocumentVerificationStatus.Created,
                DeclaredData = new DeclaredData
                {
                    Name = "John Doe"
                },
                Document = new DocumentDetails
                {
                    DocumentType = DocumentType.Passport,
                    DocumentIssuingCountry = "US",
                    FrontImageSignedUrl = "https://example.com/front-image.jpg",
                    FullName = "John Doe",
                    BirthDate = "1990-01-01"
                }
            };
        }

        private static IdDocumentVerificationAttemptResponse CreateIdDocumentVerificationAttemptResponse()
        {
            return new IdDocumentVerificationAttemptResponse
            {
                Id = AttemptId,
                Status = IdDocumentVerificationAttemptStatus.Completed
            };
        }

        private static IdDocumentVerificationAttemptsResponse CreateIdDocumentVerificationAttemptsResponse()
        {
            return new IdDocumentVerificationAttemptsResponse
            {
                TotalCount = 1,
                Skip = 0,
                Limit = 10,
                Data = new List<IdDocumentVerificationAttemptResponse>
                {
                    CreateIdDocumentVerificationAttemptResponse()
                }
            };
        }

        private static IdDocumentVerificationReportResponse CreateIdDocumentVerificationReportResponse()
        {
            return new IdDocumentVerificationReportResponse
            {
                SignedUrl = "https://example.com/report.pdf"
            };
        }

        private static void ValidateIdDocumentVerificationResponse(IdDocumentVerificationResponse response)
        {
            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNullOrEmpty();
            response.UserJourneyId.ShouldNotBeNullOrEmpty();
            response.ApplicantId.ShouldNotBeNullOrEmpty();
            response.Status.ShouldNotBeNull();
        }

        private static void ValidateIdDocumentVerificationAttemptResponse(IdDocumentVerificationAttemptResponse response)
        {
            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNullOrEmpty();
            response.Status.ShouldNotBeNull();
        }

        private static void ValidateIdDocumentVerificationAttemptsResponse(IdDocumentVerificationAttemptsResponse response)
        {
            response.ShouldNotBeNull();
            response.Data.ShouldNotBeNull();
            response.TotalCount.ShouldBeGreaterThanOrEqualTo(0);
            response.Skip.ShouldBeGreaterThanOrEqualTo(0);
            response.Limit.ShouldBeGreaterThanOrEqualTo(0);
        }

        private static void ValidateIdDocumentVerificationReportResponse(IdDocumentVerificationReportResponse response)
        {
            response.ShouldNotBeNull();
            response.SignedUrl.ShouldNotBeNullOrEmpty();
        }
    }
}