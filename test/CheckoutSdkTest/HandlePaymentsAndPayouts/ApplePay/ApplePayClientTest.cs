using Checkout.HandlePaymentsAndPayouts.ApplePay.Entities;
using Checkout.HandlePaymentsAndPayouts.ApplePay.Requests;
using Checkout.HandlePaymentsAndPayouts.ApplePay.Responses;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.HandlePaymentsAndPayouts.ApplePay
{
    public class ApplePayClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.DefaultOAuth, ValidDefaultSk);
        private readonly SdkAuthorization _oAuthAuthorization = new SdkAuthorization(PlatformType.DefaultOAuth, ValidDefaultSk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.DefaultOAuth);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public ApplePayClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKeyOrOAuth))
                .Returns(_authorization);
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.OAuth))
                .Returns(_oAuthAuthorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);
        }

        [Fact]
        public async Task UploadPaymentProcessingCertificate_WhenRequestIsValid_ShouldCallApiClientPost()
        {
            // Arrange
            var request = CreateValidUploadCertificateRequest();
            var expectedResponse = CreateUploadCertificateResponse();

            _apiClient.Setup(apiClient => apiClient.Post<UploadCertificateResponse>(
                    "applepay/certificates",
                    _authorization,
                    request,
                    CancellationToken.None,
                    null))
                .ReturnsAsync(expectedResponse);

            IApplePayClient applePayClient = new ApplePayClient(_apiClient.Object, _configuration.Object);

            // Act
            var response = await applePayClient.UploadPaymentProcessingCertificate(request);

            // Assert
            ValidateUploadCertificateResponse(response, expectedResponse);
        }

        [Fact]
        public async Task UploadPaymentProcessingCertificate_WhenRequestIsNull_ShouldThrowCheckoutArgumentException()
        {
            // Arrange
            IApplePayClient applePayClient = new ApplePayClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            await Should.ThrowAsync<CheckoutArgumentException>(async () => await applePayClient.UploadPaymentProcessingCertificate(null));
        }

        [Fact]
        public async Task EnrollDomain_WhenRequestIsValid_ShouldCallApiClientPost()
        {
            // Arrange
            var request = CreateValidEnrollDomainRequest();
            var expectedResponse = new EmptyResponse();

            _apiClient.Setup(apiClient => apiClient.Post<EmptyResponse>(
                    "applepay/enrollments",
                    _oAuthAuthorization,
                    request,
                    CancellationToken.None,
                    null))
                .ReturnsAsync(expectedResponse);

            IApplePayClient applePayClient = new ApplePayClient(_apiClient.Object, _configuration.Object);

            // Act
            var response = await applePayClient.EnrollDomain(request);

            // Assert
            response.ShouldNotBeNull();
            response.ShouldBeSameAs(expectedResponse);
        }

        [Fact]
        public async Task EnrollDomain_WhenRequestIsNull_ShouldThrowCheckoutArgumentException()
        {
            // Arrange
            IApplePayClient applePayClient = new ApplePayClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            await Should.ThrowAsync<CheckoutArgumentException>(async () => await applePayClient.EnrollDomain(null));
        }

        [Fact]
        public async Task GenerateCertificateSigningRequest_WhenRequestIsValid_ShouldCallApiClientPost()
        {
            // Arrange
            var request = CreateValidGenerateSigningRequestRequest();
            var expectedResponse = CreateGenerateSigningRequestResponse();

            _apiClient.Setup(apiClient => apiClient.Post<GenerateSigningRequestResponse>(
                    "applepay/signing-requests",
                    _authorization,
                    request,
                    CancellationToken.None,
                    null))
                .ReturnsAsync(expectedResponse);

            IApplePayClient applePayClient = new ApplePayClient(_apiClient.Object, _configuration.Object);

            // Act
            var response = await applePayClient.GenerateCertificateSigningRequest(request);

            // Assert
            ValidateGenerateSigningRequestResponse(response, expectedResponse);
        }

        [Fact]
        public async Task GenerateCertificateSigningRequest_WhenRequestIsNull_ShouldThrowCheckoutArgumentException()
        {
            // Arrange
            IApplePayClient applePayClient = new ApplePayClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            await Should.ThrowAsync<CheckoutArgumentException>(async () => await applePayClient.GenerateCertificateSigningRequest(null));
        }

        [Fact]
        public async Task UploadPaymentProcessingCertificate_WithCancellationToken_ShouldPassTokenToApiClient()
        {
            // Arrange
            var request = CreateValidUploadCertificateRequest();
            var expectedResponse = CreateUploadCertificateResponse();
            var cancellationToken = new CancellationToken();

            _apiClient.Setup(apiClient => apiClient.Post<UploadCertificateResponse>(
                    "applepay/certificates",
                    _authorization,
                    request,
                    cancellationToken,
                    null))
                .ReturnsAsync(expectedResponse);

            IApplePayClient applePayClient = new ApplePayClient(_apiClient.Object, _configuration.Object);

            // Act
            var response = await applePayClient.UploadPaymentProcessingCertificate(request, cancellationToken);

            // Assert
            ValidateUploadCertificateResponse(response, expectedResponse);
        }

        [Fact]
        public async Task EnrollDomain_WithCancellationToken_ShouldPassTokenToApiClient()
        {
            // Arrange
            var request = CreateValidEnrollDomainRequest();
            var expectedResponse = new EmptyResponse();
            var cancellationToken = new CancellationToken();

            _apiClient.Setup(apiClient => apiClient.Post<EmptyResponse>(
                    "applepay/enrollments",
                    _oAuthAuthorization,
                    request,
                    cancellationToken,
                    null))
                .ReturnsAsync(expectedResponse);

            IApplePayClient applePayClient = new ApplePayClient(_apiClient.Object, _configuration.Object);

            // Act
            var response = await applePayClient.EnrollDomain(request, cancellationToken);

            // Assert
            response.ShouldNotBeNull();
            response.ShouldBeSameAs(expectedResponse);
        }

        [Fact]
        public async Task GenerateCertificateSigningRequest_WithCancellationToken_ShouldPassTokenToApiClient()
        {
            // Arrange
            var request = CreateValidGenerateSigningRequestRequest();
            var expectedResponse = CreateGenerateSigningRequestResponse();
            var cancellationToken = new CancellationToken();

            _apiClient.Setup(apiClient => apiClient.Post<GenerateSigningRequestResponse>(
                    "applepay/signing-requests",
                    _authorization,
                    request,
                    cancellationToken,
                    null))
                .ReturnsAsync(expectedResponse);

            IApplePayClient applePayClient = new ApplePayClient(_apiClient.Object, _configuration.Object);

            // Act
            var response = await applePayClient.GenerateCertificateSigningRequest(request, cancellationToken);

            // Assert
            ValidateGenerateSigningRequestResponse(response, expectedResponse);
        }

        // Test helper methods - Setup/Builder methods
        private UploadCertificateRequest CreateValidUploadCertificateRequest()
        {
            return new UploadCertificateRequest
            {
                Content = "-----BEGIN CERTIFICATE-----\nMIIDFjCCAf4CAQAwDQYJKoZIhvcNAQEFBQAwUzELMAkGA1UEBhMCVUsxEDAOBgNV\n-----END CERTIFICATE-----"
            };
        }

        private EnrollDomainRequest CreateValidEnrollDomainRequest()
        {
            return new EnrollDomainRequest
            {
                Domain = "example.com"
            };
        }

        private GenerateSigningRequestRequest CreateValidGenerateSigningRequestRequest()
        {
            return new GenerateSigningRequestRequest
            {
                ProtocolVersion = ProtocolVersions.EcV1
            };
        }

        private UploadCertificateResponse CreateUploadCertificateResponse()
        {
            return new UploadCertificateResponse
            {
                Id = "cert_test_12345",
                PublicKeyHash = "abcd1234567890hash",
                ValidFrom = new DateTime(2024, 1, 1),
                ValidUntil = new DateTime(2025, 1, 1)
            };
        }

        private GenerateSigningRequestResponse CreateGenerateSigningRequestResponse()
        {
            return new GenerateSigningRequestResponse
            {
                Content = "-----BEGIN CERTIFICATE REQUEST-----\nMIICWjCCAUICAQAwFTETMBEGA1UEAwwKaXNzdWluZy5jb20wggEiMA0GCSqGSIb3\n-----END CERTIFICATE REQUEST-----"
            };
        }

        // Test helper methods - Validation/Assert methods
        private void ValidateUploadCertificateResponse(UploadCertificateResponse actual, UploadCertificateResponse expected)
        {
            actual.ShouldNotBeNull();
            actual.Id.ShouldBe(expected.Id);
            actual.PublicKeyHash.ShouldBe(expected.PublicKeyHash);
            actual.ValidFrom.ShouldBe(expected.ValidFrom);
            actual.ValidUntil.ShouldBe(expected.ValidUntil);
        }

        private void ValidateGenerateSigningRequestResponse(GenerateSigningRequestResponse actual, GenerateSigningRequestResponse expected)
        {
            actual.ShouldNotBeNull();
            actual.Content.ShouldBe(expected.Content);
        }
    }
}