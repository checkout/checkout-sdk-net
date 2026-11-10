using Checkout.HandlePaymentsAndPayouts.ApplePay.Requests;
using Checkout.HandlePaymentsAndPayouts.ApplePay.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.HandlePaymentsAndPayouts.ApplePay
{
    public class ApplePayClient : AbstractClient, IApplePayClient
    {
        private const string ApplePayPath = "applepay";
        private const string CertificatesPath = "certificates";
        private const string EnrollmentsPath = "enrollments";
        private const string SigningRequestsPath = "signing-requests";

        public ApplePayClient(IApiClient apiClient, CheckoutConfiguration configuration)
            : base(apiClient, configuration, SdkAuthorizationType.SecretKeyOrOAuth)
        {
        }

        public Task<UploadCertificateResponse> UploadPaymentProcessingCertificate(UploadCertificateRequest request,
                                                                        CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("request", request);
            return ApiClient.Post<UploadCertificateResponse>(
                BuildPath(ApplePayPath, CertificatesPath),
                SdkAuthorization(),
                request,
                cancellationToken);
        }

        public Task<EmptyResponse> EnrollDomain(EnrollDomainRequest request,
                                                CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("request", request);
            return ApiClient.Post<EmptyResponse>(
                BuildPath(ApplePayPath, EnrollmentsPath),
                SdkAuthorization(SdkAuthorizationType.OAuth),
                request,
                cancellationToken);
        }

        public Task<GenerateSigningRequestResponse> GenerateCertificateSigningRequest(GenerateSigningRequestRequest request,
                                                                                CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("request", request);
            return ApiClient.Post<GenerateSigningRequestResponse>(
                BuildPath(ApplePayPath, SigningRequestsPath),
                SdkAuthorization(),
                request,
                cancellationToken);
        }
    }
}