using Checkout.HandlePaymentsAndPayouts.GooglePay.Requests;
using Checkout.HandlePaymentsAndPayouts.GooglePay.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.HandlePaymentsAndPayouts.GooglePay
{
    public class GooglePayClient : AbstractClient, IGooglePayClient
    {
        private const string GooglePayPath = "googlepay";
        private const string EnrollmentsPath = "enrollments";
        private const string DomainPath = "domain";
        private const string DomainsPath = "domains";
        private const string StatePath = "state";

        public GooglePayClient(IApiClient apiClient, CheckoutConfiguration configuration)
            : base(apiClient, configuration, SdkAuthorizationType.SecretKeyOrOAuth)
        {
        }

        public Task<GooglePayEnrollmentResponse> CreateEnrollment(GooglePayEnrollmentRequest request,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("request", request);
            return ApiClient.Post<GooglePayEnrollmentResponse>(
                BuildPath(GooglePayPath, EnrollmentsPath),
                SdkAuthorization(),
                request,
                cancellationToken);
        }

        public Task<EmptyResponse> RegisterDomain(string entityId, GooglePayRegisterDomainRequest request,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("entityId", entityId, "request", request);
            return ApiClient.Post<EmptyResponse>(
                BuildPath(GooglePayPath, EnrollmentsPath, entityId, DomainPath),
                SdkAuthorization(),
                request,
                cancellationToken);
        }

        public Task<GooglePayDomainListResponse> GetDomains(string entityId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("entityId", entityId);
            return ApiClient.Get<GooglePayDomainListResponse>(
                BuildPath(GooglePayPath, EnrollmentsPath, entityId, DomainsPath),
                SdkAuthorization(),
                cancellationToken);
        }

        public Task<GooglePayEnrollmentStateResponse> GetEnrollmentState(string entityId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("entityId", entityId);
            return ApiClient.Get<GooglePayEnrollmentStateResponse>(
                BuildPath(GooglePayPath, EnrollmentsPath, entityId, StatePath),
                SdkAuthorization(),
                cancellationToken);
        }
    }
}
