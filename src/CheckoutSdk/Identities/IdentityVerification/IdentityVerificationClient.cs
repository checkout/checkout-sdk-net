using System.Threading;
using System.Threading.Tasks;
using Checkout.Identities.IdentityVerification.Requests;
using Checkout.Identities.IdentityVerification.Responses;

namespace Checkout.Identities.IdentityVerification
{
    public class IdentityVerificationClient : AbstractClient, IIdentityVerificationClient
    {
        private const string CreateAndOpenPath = "create-and-open-idv";
        private const string IdentityVerificationsPath = "identity-verifications";
        private const string AnonymizePath = "anonymize";
        private const string AttemptsPath = "attempts";
        private const string ReportPath = "pdf-report";

        public IdentityVerificationClient(IApiClient apiClient, CheckoutConfiguration configuration) :
            base(apiClient, configuration, SdkAuthorizationType.SecretKeyOrOAuth)
        {
        }

        /// <summary>
        /// Creates an identity verification and initial attempt
        /// </summary>
        /// <param name="identityVerificationAndAttemptRequest">the create and open IDV request</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the identity verification response with attempt URL</returns>
        public Task<IdentityVerificationAndAttemptResponse> CreateIdentityVerificationAndAttempt(IdentityVerificationAndAttemptRequest identityVerificationAndAttemptRequest, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("identityVerificationAndAttemptRequest", identityVerificationAndAttemptRequest);
            return ApiClient.Post<IdentityVerificationAndAttemptResponse>(CreateAndOpenPath, 
                SdkAuthorization(), identityVerificationAndAttemptRequest, cancellationToken);
        }


        /// <summary>
        /// Creates a new identity verification
        /// </summary>
        /// <param name="identityVerificationRequest">the identity verification request</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the identity verification response</returns>
        public Task<IdentityVerificationResponse> CreateIdentityVerification(IdentityVerificationRequest identityVerificationRequest, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("identityVerificationRequest", identityVerificationRequest);
            return ApiClient.Post<IdentityVerificationResponse>(IdentityVerificationsPath, 
                SdkAuthorization(), identityVerificationRequest, cancellationToken);
        }

        /// <summary>
        /// Retrieves an existing identity verification by ID
        /// </summary>
        /// <param name="identityVerificationId">the identity verification ID</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the identity verification response</returns>
        public Task<IdentityVerificationResponse> GetIdentityVerification(string identityVerificationId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("identityVerificationId", identityVerificationId);
            return ApiClient.Get<IdentityVerificationResponse>(BuildPath(IdentityVerificationsPath, identityVerificationId), 
                SdkAuthorization(), cancellationToken);
        }

        /// <summary>
        /// Anonymizes an identity verification by removing personal data
        /// </summary>
        /// <param name="identityVerificationId">the identity verification ID</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the anonymized identity verification response</returns>
        public Task<IdentityVerificationResponse> AnonymizeIdentityVerification(string identityVerificationId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("identityVerificationId", identityVerificationId);
            return ApiClient.Post<IdentityVerificationResponse>(BuildPath(IdentityVerificationsPath, identityVerificationId, AnonymizePath), 
                SdkAuthorization(), (object)null, cancellationToken);
        }

        /// <summary>
        /// Creates a new identity verification attempt
        /// </summary>
        /// <param name="identityVerificationId">the identity verification ID</param>
        /// <param name="identityVerificationAttemptRequest">the identity verification attempt request</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the identity verification attempt response</returns>
        public Task<IdentityVerificationAttemptResponse> CreateIdentityVerificationAttempt(string identityVerificationId, IdentityVerificationAttemptRequest identityVerificationAttemptRequest, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("identityVerificationId", identityVerificationId, "identityVerificationAttemptRequest", identityVerificationAttemptRequest);
            return ApiClient.Post<IdentityVerificationAttemptResponse>(BuildPath(IdentityVerificationsPath, identityVerificationId, AttemptsPath), 
                SdkAuthorization(), identityVerificationAttemptRequest, cancellationToken);
        }

        /// <summary>
        /// Retrieves all attempts for an identity verification
        /// </summary>
        /// <param name="identityVerificationId">the identity verification ID</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the identity verification attempts response</returns>
        public Task<IdentityVerificationAttemptsResponse> GetIdentityVerificationAttempts(string identityVerificationId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("identityVerificationId", identityVerificationId);
            return ApiClient.Get<IdentityVerificationAttemptsResponse>(BuildPath(IdentityVerificationsPath, identityVerificationId, AttemptsPath), 
                SdkAuthorization(), cancellationToken);
        }

        /// <summary>
        /// Retrieves a specific attempt for an identity verification
        /// </summary>
        /// <param name="identityVerificationId">the identity verification ID</param>
        /// <param name="attemptId">the attempt ID</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the identity verification attempt response</returns>
        public Task<IdentityVerificationAttemptResponse> GetIdentityVerificationAttempt(string identityVerificationId, string attemptId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("identityVerificationId", identityVerificationId, "attemptId", attemptId);
            return ApiClient.Get<IdentityVerificationAttemptResponse>(BuildPath(IdentityVerificationsPath, identityVerificationId, AttemptsPath, attemptId), 
                SdkAuthorization(), cancellationToken);
        }

        /// <summary>
        /// Retrieves the PDF report for an identity verification
        /// </summary>
        /// <param name="identityVerificationId">the identity verification ID</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the identity verification report response</returns>
        public Task<IdentityVerificationReportResponse> GetIdentityVerificationReport(string identityVerificationId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("identityVerificationId", identityVerificationId);
            return ApiClient.Get<IdentityVerificationReportResponse>(BuildPath(IdentityVerificationsPath, identityVerificationId, ReportPath), 
                SdkAuthorization(), cancellationToken);
        }
    }
}