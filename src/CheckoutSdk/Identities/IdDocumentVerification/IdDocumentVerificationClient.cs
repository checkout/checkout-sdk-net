using System.Threading;
using System.Threading.Tasks;
using Checkout.Identities.IdDocumentVerification.Requests;
using Checkout.Identities.IdDocumentVerification.Responses;

namespace Checkout.Identities.IdDocumentVerification
{
    public class IdDocumentVerificationClient : AbstractClient, IIdDocumentVerificationClient
    {
        private const string IdDocumentVerificationsPath = "id-document-verifications";

        public IdDocumentVerificationClient(IApiClient apiClient, CheckoutConfiguration configuration) :
            base(apiClient, configuration, SdkAuthorizationType.SecretKeyOrOAuth)
        {
        }

        /// <summary>
        ///     Creates a new ID document verification
        /// </summary>
        /// <param name="idDocumentVerificationRequest">the ID document verification request</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the ID document verification response</returns>
        public Task<IdDocumentVerificationResponse> CreateIdDocumentVerification(IdDocumentVerificationRequest idDocumentVerificationRequest, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("idDocumentVerificationRequest", idDocumentVerificationRequest);
            return ApiClient.Post<IdDocumentVerificationResponse>(IdDocumentVerificationsPath, 
                SdkAuthorization(), idDocumentVerificationRequest, cancellationToken);
        }

        /// <summary>
        ///     Retrieves an existing ID document verification by ID
        /// </summary>
        /// <param name="idDocumentVerificationId">the ID document verification ID</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the ID document verification response</returns>
        public Task<IdDocumentVerificationResponse> GetIdDocumentVerification(string idDocumentVerificationId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("idDocumentVerificationId", idDocumentVerificationId);
            return ApiClient.Get<IdDocumentVerificationResponse>(BuildPath(IdDocumentVerificationsPath, idDocumentVerificationId), 
                SdkAuthorization(), cancellationToken);
        }

        /// <summary>
        ///     Anonymizes an ID document verification by removing personal data
        /// </summary>
        /// <param name="idDocumentVerificationId">the ID document verification ID</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the ID document verification response</returns>
        public Task<IdDocumentVerificationResponse> AnonymizeIdDocumentVerification(string idDocumentVerificationId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("idDocumentVerificationId", idDocumentVerificationId);
            return ApiClient.Post<IdDocumentVerificationResponse>(BuildPath(IdDocumentVerificationsPath, idDocumentVerificationId, "anonymize"), 
                SdkAuthorization(), (object)null, cancellationToken);
        }

        /// <summary>
        ///     Creates a new ID document verification attempt
        /// </summary>
        /// <param name="idDocumentVerificationId">the ID document verification ID</param>
        /// <param name="attemptRequest">the ID document verification attempt request</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the ID document verification attempt response</returns>
        public Task<IdDocumentVerificationAttemptResponse> CreateIdDocumentVerificationAttempt(string idDocumentVerificationId, IdDocumentVerificationAttemptRequest attemptRequest, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("idDocumentVerificationId", idDocumentVerificationId);
            CheckoutUtils.ValidateParams("attemptRequest", attemptRequest);
            return ApiClient.Post<IdDocumentVerificationAttemptResponse>(BuildPath(IdDocumentVerificationsPath, idDocumentVerificationId, "attempts"), 
                SdkAuthorization(), attemptRequest, cancellationToken);
        }

        /// <summary>
        ///     Retrieves all attempts for an ID document verification
        /// </summary>
        /// <param name="idDocumentVerificationId">the ID document verification ID</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the ID document verification attempts response</returns>
        public Task<IdDocumentVerificationAttemptsResponse> GetIdDocumentVerificationAttempts(string idDocumentVerificationId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("idDocumentVerificationId", idDocumentVerificationId);
            return ApiClient.Get<IdDocumentVerificationAttemptsResponse>(BuildPath(IdDocumentVerificationsPath, idDocumentVerificationId, "attempts"), 
                SdkAuthorization(), cancellationToken);
        }

        /// <summary>
        ///     Retrieves a specific attempt for an ID document verification
        /// </summary>
        /// <param name="idDocumentVerificationId">the ID document verification ID</param>
        /// <param name="attemptId">the attempt ID</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the ID document verification attempt response</returns>
        public Task<IdDocumentVerificationAttemptResponse> GetIdDocumentVerificationAttempt(string idDocumentVerificationId, string attemptId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("idDocumentVerificationId", idDocumentVerificationId);
            CheckoutUtils.ValidateParams("attemptId", attemptId);
            return ApiClient.Get<IdDocumentVerificationAttemptResponse>(BuildPath(IdDocumentVerificationsPath, idDocumentVerificationId, "attempts", attemptId), 
                SdkAuthorization(), cancellationToken);
        }

        /// <summary>
        ///     Retrieves the PDF report for an ID document verification
        /// </summary>
        /// <param name="idDocumentVerificationId">the ID document verification ID</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the ID document verification report response</returns>
        public Task<IdDocumentVerificationReportResponse> GetIdDocumentVerificationReport(string idDocumentVerificationId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("idDocumentVerificationId", idDocumentVerificationId);
            return ApiClient.Get<IdDocumentVerificationReportResponse>(BuildPath(IdDocumentVerificationsPath, idDocumentVerificationId, "pdf-report"), 
                SdkAuthorization(), cancellationToken);
        }
    }
}