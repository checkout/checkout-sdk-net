using System.Threading;
using System.Threading.Tasks;
using Checkout.Identities.IdDocumentVerification.Requests;
using Checkout.Identities.IdDocumentVerification.Responses;

namespace Checkout.Identities.IdDocumentVerification
{
    /// <summary>
    ///     Client for managing ID document verifications in identity verification processes
    /// </summary>
    public interface IIdDocumentVerificationClient
    {
        /// <summary>
        ///     Creates a new ID document verification
        /// </summary>
        /// <param name="idDocumentVerificationRequest">the ID document verification request</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the ID document verification response</returns>
        Task<IdDocumentVerificationResponse> CreateIdDocumentVerification(IdDocumentVerificationRequest idDocumentVerificationRequest, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Retrieves an existing ID document verification by ID
        /// </summary>
        /// <param name="idDocumentVerificationId">the ID document verification ID</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the ID document verification response</returns>
        Task<IdDocumentVerificationResponse> GetIdDocumentVerification(string idDocumentVerificationId, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Anonymizes an ID document verification by removing personal data
        /// </summary>
        /// <param name="idDocumentVerificationId">the ID document verification ID</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the ID document verification response</returns>
        Task<IdDocumentVerificationResponse> AnonymizeIdDocumentVerification(string idDocumentVerificationId, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Creates a new ID document verification attempt
        /// </summary>
        /// <param name="idDocumentVerificationId">the ID document verification ID</param>
        /// <param name="attemptRequest">the ID document verification attempt request</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the ID document verification attempt response</returns>
        Task<IdDocumentVerificationAttemptResponse> CreateIdDocumentVerificationAttempt(string idDocumentVerificationId, IdDocumentVerificationAttemptRequest attemptRequest, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Retrieves all attempts for an ID document verification
        /// </summary>
        /// <param name="idDocumentVerificationId">the ID document verification ID</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the ID document verification attempts response</returns>
        Task<IdDocumentVerificationAttemptsResponse> GetIdDocumentVerificationAttempts(string idDocumentVerificationId, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Retrieves a specific attempt for an ID document verification
        /// </summary>
        /// <param name="idDocumentVerificationId">the ID document verification ID</param>
        /// <param name="attemptId">the attempt ID</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the ID document verification attempt response</returns>
        Task<IdDocumentVerificationAttemptResponse> GetIdDocumentVerificationAttempt(string idDocumentVerificationId, string attemptId, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Retrieves the PDF report for an ID document verification
        /// </summary>
        /// <param name="idDocumentVerificationId">the ID document verification ID</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the ID document verification report response</returns>
        Task<IdDocumentVerificationReportResponse> GetIdDocumentVerificationReport(string idDocumentVerificationId, CancellationToken cancellationToken = default);
    }
}