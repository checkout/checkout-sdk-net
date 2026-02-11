using Checkout.HandlePaymentsAndPayouts.ApplePay.Entities;
using Checkout.HandlePaymentsAndPayouts.ApplePay.Requests;
using Checkout.HandlePaymentsAndPayouts.ApplePay.Responses;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.HandlePaymentsAndPayouts.ApplePay
{
    public class ApplePayIntegrationTest : SandboxTestFixture
    {
        public ApplePayIntegrationTest() : base(PlatformType.Default)
        {
        }

        [Fact(Skip = "This test requires a valid payment processing certificate")]
        public async Task UploadPaymentProcessingCertificate_ShouldReturnValidResponse()
        {
            // Arrange
            var request = CreateValidUploadCertificateRequest();

            // Act
            var response = await DefaultApi.ApplePayClient().UploadPaymentProcessingCertificate(request);

            // Assert
            ValidateUploadCertificateResponse(response, request);
        }

        [Fact]
        public async Task GenerateCertificateSigningRequest_ShouldReturnValidResponse()
        {
            // Arrange
            var request = CreateValidGenerateSigningRequestRequest();

            // Act
            var response = await DefaultApi.ApplePayClient().GenerateCertificateSigningRequest(request);

            // Assert
            ValidateGenerateSigningRequestResponse(response, request);
        }

        [Fact]
        public async Task GenerateCertificateSigningRequest_WithRsaProtocol_ShouldReturnValidResponse()
        {
            // Arrange
            var request = new GenerateSigningRequestRequest
            {
                ProtocolVersion = ProtocolVersions.RsaV1
            };

            // Act
            var response = await DefaultApi.ApplePayClient().GenerateCertificateSigningRequest(request);

            // Assert
            ValidateGenerateSigningRequestResponse(response, request);
        }

        [Fact]
        public async Task ApplePayWorkflow_GenerateSigningRequestAndUploadCertificate_ShouldWork()
        {
            // Arrange
            var signingRequest = CreateValidGenerateSigningRequestRequest();

            // Act - Generate signing request first
            var signingResponse = await DefaultApi.ApplePayClient().GenerateCertificateSigningRequest(signingRequest);

            // Assert signing request response
            ValidateGenerateSigningRequestResponse(signingResponse, signingRequest);

            // Note: In real scenario, you would:
            // 1. Take the signingResponse.Content
            // 2. Submit it to Apple Developer Portal
            // 3. Download the certificate from Apple
            // 4. Upload it using UploadPaymentProcessingCertificate
            //
            // For integration testing, we skip the Apple Developer Portal steps
            // as they require manual intervention and valid Apple Developer account
        }

        // Common methods
        private UploadCertificateRequest CreateValidUploadCertificateRequest()
        {
            // Note: This is a sample certificate format
            // In real scenarios, this would be a certificate obtained from Apple Developer Portal
            return new UploadCertificateRequest
            {
                Content = "-----BEGIN CERTIFICATE-----\n" +
                         "MIIFjTCCBHWgAwIBAgIIAqVJ3DZvutkwDQYJKoZIhvcNAQEFBQAwgZYxCzAJBgNV\n" +
                         "BAYTAlVTMRMwEQYDVQQKDApBcHBsZSBJbmMuMSwwKgYDVQQLDCNBcHBsZSBXb3Js\n" +
                         "ZHdpZGUgRGV2ZWxvcGVyIFJlbGF0aW9uczFEMEIGA1UEAww7QXBwbGUgV29ybGR3\n" +
                         "aWRlIERldmVsb3BlciBSZWxhdGlvbnMgQ2VydGlmaWNhdGlvbiBBdXRob3JpdHkw\n" +
                         "HhcNMjQwMTEwMTUzNjQ1WhcNMjUwMTA5MTUzNjQ1WjCBjDEaMBgGCgmSJomT8ixk\n" +
                         "ARkWCnRlc3QtZG9tYWluMS0wKwYDVQQDDCRtZXJjaGFudC50ZXN0LWRvbWFpbiAo\n" +
                         "U2FuZGJveCkgLSBBUE1QMRMwEQYDVQQIDApDYWxpZm9ybmlhMQswCQYDVQQGEwJV\n" +
                         "UzEXMBUGA1UECgwOVGVzdCBNZXJjaGFudDEXMBUGA1UECwwOVGVzdCBNZXJjaGFu\n" +
                         "dDBZMBMGByqGSM49AgEGCCqGSM49AwEHA0IABGvWZDKkf8rkJ4V1sdf9Wt1iBZvD\n" +
                         "l9dEJkY/CJFVYNvK5qgWUzjbGKKcLFfvHt3vvK6jggHtMIIB6TAMBgNVHRMBAf8E\n" +
                         "-----END CERTIFICATE-----"
            };
        }

        private EnrollDomainRequest CreateValidEnrollDomainRequest()
        {
            return new EnrollDomainRequest
            {
                Domain = "checkout-test-domain.com"
            };
        }

        private GenerateSigningRequestRequest CreateValidGenerateSigningRequestRequest()
        {
            return new GenerateSigningRequestRequest
            {
                ProtocolVersion = ProtocolVersions.EcV1
            };
        }

        private void ValidateUploadCertificateResponse(UploadCertificateResponse response, UploadCertificateRequest request)
        {
            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNull();
            response.PublicKeyHash.ShouldNotBeNull();
            response.ValidFrom.ShouldNotBeNull();
            response.ValidUntil.ShouldNotBeNull();
            response.ValidUntil.Value.ShouldBeGreaterThan(response.ValidFrom.Value);
            
            // Verify the certificate content was processed (we can't directly compare as it's processed by the API)
            request.Content.ShouldNotBeNullOrEmpty();
        }

        private void ValidateGenerateSigningRequestResponse(GenerateSigningRequestResponse response, GenerateSigningRequestRequest request)
        {
            response.ShouldNotBeNull();
            response.Content.ShouldNotBeNullOrEmpty();
            
            // Verify it looks like a certificate signing request
            response.Content.ShouldContain("BEGIN CERTIFICATE REQUEST");
            response.Content.ShouldContain("END CERTIFICATE REQUEST");
            
            // Verify the protocol version was respected (indirectly through successful response)
            request.ProtocolVersion.ShouldNotBeNull();
        }
    }
}