using System.Threading;
using System.Threading.Tasks;
using Checkout.Identities.AddressDocumentVerification.Requests;
using Checkout.Identities.AddressDocumentVerification.Responses;
using Checkout.Identities.Entities;

namespace Checkout.Identities.AddressDocumentVerification
{
    public class AddressDocumentVerificationClient : AbstractClient, IAddressDocumentVerificationClient
    {
        private const string AddressDocumentVerificationsPath = "address-document-verifications";
        private const string AnonymizePath = "anonymize";
        private const string AttemptsPath = "attempts";
        private const string ReportPath = "pdf-report";
        private const string AssetsPath = "assets";

        public AddressDocumentVerificationClient(IApiClient apiClient, CheckoutConfiguration configuration) :
            base(apiClient, configuration, SdkAuthorizationType.SecretKeyOrOAuth)
        {
        }

        /// <summary>
        /// Creates a new address document verification
        /// </summary>
        /// <param name="addressDocumentVerificationRequest">the address document verification request</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the address document verification response</returns>
        public Task<AddressDocumentVerificationResponse> CreateAddressDocumentVerification(AddressDocumentVerificationRequest addressDocumentVerificationRequest, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("addressDocumentVerificationRequest", addressDocumentVerificationRequest);
            return ApiClient.Post<AddressDocumentVerificationResponse>(AddressDocumentVerificationsPath,
                SdkAuthorization(), addressDocumentVerificationRequest, cancellationToken);
        }

        /// <summary>
        /// Retrieves an existing address document verification by ID
        /// </summary>
        /// <param name="addressDocumentVerificationId">the address document verification ID</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the address document verification response</returns>
        public Task<AddressDocumentVerificationResponse> GetAddressDocumentVerification(string addressDocumentVerificationId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("addressDocumentVerificationId", addressDocumentVerificationId);
            return ApiClient.Get<AddressDocumentVerificationResponse>(BuildPath(AddressDocumentVerificationsPath, addressDocumentVerificationId),
                SdkAuthorization(), cancellationToken);
        }

        /// <summary>
        /// Anonymizes an address document verification by removing personal data
        /// </summary>
        /// <param name="addressDocumentVerificationId">the address document verification ID</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the anonymized address document verification response</returns>
        public Task<AddressDocumentVerificationResponse> AnonymizeAddressDocumentVerification(string addressDocumentVerificationId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("addressDocumentVerificationId", addressDocumentVerificationId);
            return ApiClient.Post<AddressDocumentVerificationResponse>(BuildPath(AddressDocumentVerificationsPath, addressDocumentVerificationId, AnonymizePath),
                SdkAuthorization(), (object)null, cancellationToken);
        }

        /// <summary>
        /// Creates a new address document verification attempt
        /// </summary>
        /// <param name="addressDocumentVerificationId">the address document verification ID</param>
        /// <param name="attemptRequest">the address document verification attempt request</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the address document verification attempt response</returns>
        public Task<AddressDocumentVerificationAttemptResponse> CreateAddressDocumentVerificationAttempt(string addressDocumentVerificationId, AddressDocumentVerificationAttemptRequest attemptRequest, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("addressDocumentVerificationId", addressDocumentVerificationId);
            CheckoutUtils.ValidateParams("attemptRequest", attemptRequest);
            return ApiClient.Post<AddressDocumentVerificationAttemptResponse>(BuildPath(AddressDocumentVerificationsPath, addressDocumentVerificationId, AttemptsPath),
                SdkAuthorization(), attemptRequest, cancellationToken);
        }

        /// <summary>
        /// Retrieves all attempts for an address document verification
        /// </summary>
        /// <param name="addressDocumentVerificationId">the address document verification ID</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the address document verification attempts response</returns>
        public Task<AddressDocumentVerificationAttemptsResponse> GetAddressDocumentVerificationAttempts(string addressDocumentVerificationId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("addressDocumentVerificationId", addressDocumentVerificationId);
            return ApiClient.Get<AddressDocumentVerificationAttemptsResponse>(BuildPath(AddressDocumentVerificationsPath, addressDocumentVerificationId, AttemptsPath),
                SdkAuthorization(), cancellationToken);
        }

        /// <summary>
        /// Retrieves a page of attempts for an address document verification
        /// </summary>
        /// <param name="addressDocumentVerificationId">the address document verification ID</param>
        /// <param name="query">the pagination query parameters (skip and limit)</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the address document verification attempts response</returns>
        public Task<AddressDocumentVerificationAttemptsResponse> GetAddressDocumentVerificationAttempts(string addressDocumentVerificationId, AttemptsQuery query, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("addressDocumentVerificationId", addressDocumentVerificationId);
            return ApiClient.Query<AddressDocumentVerificationAttemptsResponse>(BuildPath(AddressDocumentVerificationsPath, addressDocumentVerificationId, AttemptsPath),
                SdkAuthorization(), query, cancellationToken);
        }

        /// <summary>
        /// Retrieves a specific attempt for an address document verification
        /// </summary>
        /// <param name="addressDocumentVerificationId">the address document verification ID</param>
        /// <param name="attemptId">the attempt ID</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the address document verification attempt response</returns>
        public Task<AddressDocumentVerificationAttemptResponse> GetAddressDocumentVerificationAttempt(string addressDocumentVerificationId, string attemptId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("addressDocumentVerificationId", addressDocumentVerificationId);
            CheckoutUtils.ValidateParams("attemptId", attemptId);
            return ApiClient.Get<AddressDocumentVerificationAttemptResponse>(BuildPath(AddressDocumentVerificationsPath, addressDocumentVerificationId, AttemptsPath, attemptId),
                SdkAuthorization(), cancellationToken);
        }

        /// <summary>
        /// Retrieves the PDF report for an address document verification
        /// </summary>
        /// <param name="addressDocumentVerificationId">the address document verification ID</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the address document verification report response</returns>
        public Task<AddressDocumentVerificationReportResponse> GetAddressDocumentVerificationReport(string addressDocumentVerificationId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("addressDocumentVerificationId", addressDocumentVerificationId);
            return ApiClient.Get<AddressDocumentVerificationReportResponse>(BuildPath(AddressDocumentVerificationsPath, addressDocumentVerificationId, ReportPath),
                SdkAuthorization(), cancellationToken);
        }

        /// <summary>
        /// Retrieves the assets (the document image) uploaded for an address document
        /// verification attempt. Beta.
        /// </summary>
        /// <param name="addressDocumentVerificationId">the address document verification ID</param>
        /// <param name="attemptId">the attempt ID</param>
        /// <param name="query">the pagination query parameters (skip and limit)</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the address document verification attempt assets response</returns>
        public Task<AddressDocumentVerificationAttemptAssetsResponse> GetAddressDocumentVerificationAttemptAssets(string addressDocumentVerificationId, string attemptId, AttemptAssetsQuery query = null, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("addressDocumentVerificationId", addressDocumentVerificationId);
            CheckoutUtils.ValidateParams("attemptId", attemptId);
            return ApiClient.Query<AddressDocumentVerificationAttemptAssetsResponse>(BuildPath(AddressDocumentVerificationsPath, addressDocumentVerificationId, AttemptsPath, attemptId, AssetsPath),
                SdkAuthorization(), query, cancellationToken);
        }
    }
}
