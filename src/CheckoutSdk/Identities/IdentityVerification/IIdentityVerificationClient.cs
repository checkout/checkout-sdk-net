using System.Threading;
using System.Threading.Tasks;
using Checkout.Identities.IdentityVerification.Requests;
using Checkout.Identities.IdentityVerification.Responses;

namespace Checkout.Identities.IdentityVerification
{
    /// <summary>
    ///     Client for managing identity verifications
    /// </summary>
    public interface IIdentityVerificationClient
    {
        /// <summary>
        ///     Creates an identity verification and initial attempt
        /// </summary>
        /// <param name="createAndOpenRequest">the create and open IDV request</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the identity verification response with attempt URL</returns>
        Task<IdentityVerificationAndAttemptResponse> CreateIdentityVerificationAndAttempt(IdentityVerificationAndAttemptRequest createAndOpenRequest, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Creates a new identity verification
        /// </summary>
        /// <param name="identityVerificationRequest">the identity verification request</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the identity verification response</returns>
        Task<IdentityVerificationResponse> CreateIdentityVerification(IdentityVerificationRequest identityVerificationRequest, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Retrieves an existing identity verification by ID
        /// </summary>
        /// <param name="identityVerificationId">the identity verification ID</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the identity verification response</returns>
        Task<IdentityVerificationResponse> GetIdentityVerification(string identityVerificationId, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Anonymizes an identity verification by removing personal data
        /// </summary>
        /// <param name="identityVerificationId">the identity verification ID</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the anonymized identity verification response</returns>
        Task<IdentityVerificationResponse> AnonymizeIdentityVerification(string identityVerificationId, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Creates a new identity verification attempt
        /// </summary>
        /// <param name="identityVerificationId">the identity verification ID</param>
        /// <param name="identityVerificationAttemptRequest">the identity verification attempt request</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the identity verification attempt response</returns>
        Task<IdentityVerificationAttemptResponse> CreateIdentityVerificationAttempt(string identityVerificationId, IdentityVerificationAttemptRequest identityVerificationAttemptRequest, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Retrieves all attempts for an identity verification
        /// </summary>
        /// <param name="identityVerificationId">the identity verification ID</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the identity verification attempts response</returns>
        Task<IdentityVerificationAttemptsResponse> GetIdentityVerificationAttempts(string identityVerificationId, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Retrieves a specific attempt for an identity verification
        /// </summary>
        /// <param name="identityVerificationId">the identity verification ID</param>
        /// <param name="attemptId">the attempt ID</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the identity verification attempt response</returns>
        Task<IdentityVerificationAttemptResponse> GetIdentityVerificationAttempt(string identityVerificationId, string attemptId, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Retrieves the PDF report for an identity verification
        /// </summary>
        /// <param name="identityVerificationId">the identity verification ID</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the identity verification report response</returns>
        Task<IdentityVerificationReportResponse> GetIdentityVerificationReport(string identityVerificationId, CancellationToken cancellationToken = default);
    }
}