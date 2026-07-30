using System.Threading;
using System.Threading.Tasks;
using Checkout.Identities.AddressDocumentVerification.Requests;
using Checkout.Identities.AddressDocumentVerification.Responses;

namespace Checkout.Identities.AddressDocumentVerification
{
    public class AddressDocumentVerificationClient : AbstractClient, IAddressDocumentVerificationClient
    {
        private const string AddressDocumentVerificationsPath = "address-document-verifications";
        private const string AnonymizePath = "anonymize";
        private const string AttemptsPath = "attempts";
        private const string ReportPath = "pdf-report";

        public AddressDocumentVerificationClient(IApiClient apiClient, CheckoutConfiguration configuration) :
            base(apiClient, configuration, SdkAuthorizationType.SecretKeyOrOAuth)
        {
        }

        public Task<AddressDocumentVerificationResponse> CreateAddressDocumentVerification(AddressDocumentVerificationRequest addressDocumentVerificationRequest, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("addressDocumentVerificationRequest", addressDocumentVerificationRequest);
            return ApiClient.Post<AddressDocumentVerificationResponse>(AddressDocumentVerificationsPath,
                SdkAuthorization(), addressDocumentVerificationRequest, cancellationToken);
        }

        public Task<AddressDocumentVerificationResponse> GetAddressDocumentVerification(string addressDocumentVerificationId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("addressDocumentVerificationId", addressDocumentVerificationId);
            return ApiClient.Get<AddressDocumentVerificationResponse>(BuildPath(AddressDocumentVerificationsPath, addressDocumentVerificationId),
                SdkAuthorization(), cancellationToken);
        }

        public Task<AddressDocumentVerificationResponse> AnonymizeAddressDocumentVerification(string addressDocumentVerificationId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("addressDocumentVerificationId", addressDocumentVerificationId);
            return ApiClient.Post<AddressDocumentVerificationResponse>(BuildPath(AddressDocumentVerificationsPath, addressDocumentVerificationId, AnonymizePath),
                SdkAuthorization(), (object)null, cancellationToken);
        }

        public Task<AddressDocumentVerificationAttemptResponse> CreateAddressDocumentVerificationAttempt(string addressDocumentVerificationId, AddressDocumentVerificationAttemptRequest attemptRequest, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("addressDocumentVerificationId", addressDocumentVerificationId);
            CheckoutUtils.ValidateParams("attemptRequest", attemptRequest);
            return ApiClient.Post<AddressDocumentVerificationAttemptResponse>(BuildPath(AddressDocumentVerificationsPath, addressDocumentVerificationId, AttemptsPath),
                SdkAuthorization(), attemptRequest, cancellationToken);
        }

        public Task<AddressDocumentVerificationAttemptsResponse> GetAddressDocumentVerificationAttempts(string addressDocumentVerificationId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("addressDocumentVerificationId", addressDocumentVerificationId);
            return ApiClient.Get<AddressDocumentVerificationAttemptsResponse>(BuildPath(AddressDocumentVerificationsPath, addressDocumentVerificationId, AttemptsPath),
                SdkAuthorization(), cancellationToken);
        }

        public Task<AddressDocumentVerificationAttemptResponse> GetAddressDocumentVerificationAttempt(string addressDocumentVerificationId, string attemptId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("addressDocumentVerificationId", addressDocumentVerificationId);
            CheckoutUtils.ValidateParams("attemptId", attemptId);
            return ApiClient.Get<AddressDocumentVerificationAttemptResponse>(BuildPath(AddressDocumentVerificationsPath, addressDocumentVerificationId, AttemptsPath, attemptId),
                SdkAuthorization(), cancellationToken);
        }

        public Task<AddressDocumentVerificationReportResponse> GetAddressDocumentVerificationReport(string addressDocumentVerificationId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("addressDocumentVerificationId", addressDocumentVerificationId);
            return ApiClient.Get<AddressDocumentVerificationReportResponse>(BuildPath(AddressDocumentVerificationsPath, addressDocumentVerificationId, ReportPath),
                SdkAuthorization(), cancellationToken);
        }
    }
}
