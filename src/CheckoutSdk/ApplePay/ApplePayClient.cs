using Checkout.ApplePay.Requests;
using Checkout.ApplePay.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.ApplePay
{
    public class ApplePayClient : AbstractClient, IApplePayClient
    {
        private const string CertificatesPath = "applepay/certificates";
        private const string EnrollmentsPath = "applepay/enrollments";
        private const string SigningRequestsPath = "applepay/signing-requests";

        public ApplePayClient(IApiClient apiClient, CheckoutConfiguration configuration)
            : base(apiClient, configuration, SdkAuthorizationType.SecretKeyOrOAuth)
        {
        }

        public Task<UploadCertificateResponse> UploadPaymentProcessingCertificate(UploadCertificateRequest request,
                                                                        CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("request", request);
            return ApiClient.Post<UploadCertificateResponse>(
                CertificatesPath,
                SdkAuthorization(),
                request,
                cancellationToken);
        }

        public Task<EmptyResponse> EnrollDomain(EnrollDomainRequest request,
                                                CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("request", request);
            return ApiClient.Post<EmptyResponse>(
                EnrollmentsPath,
                SdkAuthorization(SdkAuthorizationType.OAuth),
                request,
                cancellationToken);
        }

        public Task<GenerateSigningRequestResponse> GenerateCertificateSigningRequest(GenerateSigningRequestRequest request,
                                                                                CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("request", request);
            return ApiClient.Post<GenerateSigningRequestResponse>(
                SigningRequestsPath,
                SdkAuthorization(),
                request,
                cancellationToken);
        }
    }
}