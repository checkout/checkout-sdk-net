using Checkout.Identities.Applicants.Requests;
using Checkout.Identities.Applicants.Responses;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Identities.Applicants
{
    public class ApplicantsClientTest : UnitTestFixture
    {
        private const string ApplicantsPath = "applicants";
        private const string ApplicantId = "aplt_7hr7swleu6guzjqesyxmyodnya";
        
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.Default, ValidDefaultSk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.Default);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public ApplicantsClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKeyOrOAuth))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);
        }

        [Fact]
        public async Task CreateApplicant_Should_Call_ApiClient_Post()
        {
            // Arrange
            var request = CreateApplicantRequest();
            var response = CreateApplicantResponse();
            
            _apiClient.Setup(apiClient =>
                    apiClient.Post<ApplicantResponse>(
                        ApplicantsPath,
                        _authorization,
                        request,
                        CancellationToken.None,
                        null))
                .ReturnsAsync(response);

            IApplicantsClient client = new ApplicantsClient(_apiClient.Object, _configuration.Object);

            // Act
            ApplicantResponse result = await client.CreateApplicant(request, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
            ValidateApplicantResponse(result);
        }

        [Fact]
        public async Task GetApplicant_Should_Call_ApiClient_Get()
        {
            // Arrange
            var response = CreateApplicantResponse();
            
            _apiClient.Setup(apiClient =>
                    apiClient.Get<ApplicantResponse>(
                        $"{ApplicantsPath}/{ApplicantId}",
                        _authorization,
                        CancellationToken.None))
                .ReturnsAsync(response);

            IApplicantsClient client = new ApplicantsClient(_apiClient.Object, _configuration.Object);

            // Act
            ApplicantResponse result = await client.GetApplicant(ApplicantId);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
            ValidateApplicantResponse(result);
        }

        [Fact]
        public async Task UpdateApplicant_Should_Call_ApiClient_Patch()
        {
            // Arrange
            var request = CreateUpdateApplicantRequest();
            var response = CreateApplicantResponse();
            
            _apiClient.Setup(apiClient =>
                    apiClient.Patch<ApplicantResponse>(
                        $"{ApplicantsPath}/{ApplicantId}",
                        _authorization,
                        request,
                        CancellationToken.None,
                        null))
                .ReturnsAsync(response);

            IApplicantsClient client = new ApplicantsClient(_apiClient.Object, _configuration.Object);

            // Act
            ApplicantResponse result = await client.UpdateApplicant(ApplicantId, request, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
            ValidateApplicantResponse(result);
        }

        [Fact]
        public async Task AnonymizeApplicant_Should_Call_ApiClient_Post()
        {
            // Arrange
            var response = CreateApplicantResponse();
            
            _apiClient.Setup(apiClient =>
                    apiClient.Post<ApplicantResponse>(
                        $"{ApplicantsPath}/{ApplicantId}/anonymize",
                        _authorization,
                        CancellationToken.None,
                        It.IsAny<CancellationToken>(),
                        null))
                .ReturnsAsync(response);

            IApplicantsClient client = new ApplicantsClient(_apiClient.Object, _configuration.Object);

            // Act
            ApplicantResponse result = await client.AnonymizeApplicant(ApplicantId);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
            ValidateApplicantResponse(result);
        }

        [Fact]
        public async Task CreateApplicant_Should_Throw_CheckoutArgumentException_When_Request_Is_Null()
        {
            // Arrange
            IApplicantsClient client = new ApplicantsClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(
                async () => await client.CreateApplicant(null)
            );
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task GetApplicant_Should_Throw_CheckoutArgumentException_When_ApplicantId_Is_Null()
        {
            // Arrange
            IApplicantsClient client = new ApplicantsClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(
                async () => await client.GetApplicant(null)
            );
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task GetApplicant_Should_Throw_CheckoutArgumentException_When_ApplicantId_Is_Empty()
        {
            // Arrange
            IApplicantsClient client = new ApplicantsClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(
                async () => await client.GetApplicant("")
            );
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task UpdateApplicant_Should_Throw_CheckoutArgumentException_When_ApplicantId_Is_Null()
        {
            // Arrange
            var request = CreateUpdateApplicantRequest();
            IApplicantsClient client = new ApplicantsClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(
                async () => await client.UpdateApplicant(null, request)
            );
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task UpdateApplicant_Should_Throw_CheckoutArgumentException_When_Request_Is_Null()
        {
            // Arrange
            IApplicantsClient client = new ApplicantsClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(
                async () => await client.UpdateApplicant(ApplicantId, null)
            );
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task AnonymizeApplicant_Should_Throw_CheckoutArgumentException_When_ApplicantId_Is_Null()
        {
            // Arrange
            IApplicantsClient client = new ApplicantsClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(
                async () => await client.AnonymizeApplicant(null)
            );
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task CreateApplicant_Should_Handle_CancellationToken()
        {
            // Arrange
            var request = CreateApplicantRequest();
            var response = CreateApplicantResponse();
            var cancellationToken = new CancellationToken();
            
            _apiClient.Setup(apiClient =>
                    apiClient.Post<ApplicantResponse>(
                        ApplicantsPath,
                        _authorization,
                        request,
                        cancellationToken,
                        null))
                .ReturnsAsync(response);

            IApplicantsClient client = new ApplicantsClient(_apiClient.Object, _configuration.Object);

            // Act
            ApplicantResponse result = await client.CreateApplicant(request, cancellationToken);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
        }

        [Fact]
        public void Constructor_Should_Initialize_Properly()
        {
            // Act
            var client = new ApplicantsClient(_apiClient.Object, _configuration.Object);

            // Assert
            client.ShouldNotBeNull();
        }

        private static CreateApplicantRequest CreateApplicantRequest()
        {
            return new CreateApplicantRequest
            {
                ExternalApplicantId = "ext_applicant_123",
                Email = "test.applicant@example.com",
                ExternalApplicantName = "John Doe"
            };
        }

        private static UpdateApplicantRequest CreateUpdateApplicantRequest()
        {
            return new UpdateApplicantRequest
            {
                Email = "updated.applicant@example.com",
                ExternalApplicantName = "John Updated Doe"
            };
        }

        private static ApplicantResponse CreateApplicantResponse()
        {
            return new ApplicantResponse
            {
                Id = ApplicantId,
                ExternalApplicantId = "ext_applicant_123",
                Email = "test.applicant@example.com",
                ExternalApplicantName = "John Doe",
                CreatedOn = DateTime.UtcNow.AddMinutes(-5),
                ModifiedOn = DateTime.UtcNow
            };
        }

        private static void ValidateApplicantResponse(ApplicantResponse response)
        {
            response.Id.ShouldNotBeNullOrEmpty();
            response.Email.ShouldNotBeNullOrEmpty();
            response.ExternalApplicantName.ShouldNotBeNullOrEmpty();
            response.CreatedOn.ShouldNotBeNull();
            response.ModifiedOn.ShouldNotBeNull();
        }
    }
}