using System.Threading;
using System.Threading.Tasks;
using Checkout.Identities.AddressDocumentVerification.Requests;
using Checkout.Identities.AddressDocumentVerification.Responses;
using Checkout.Identities.Entities;

namespace Checkout.Identities.AddressDocumentVerification
{
    /// <summary>
    ///     Client for managing address document verifications in identity verification processes
    /// </summary>
    public interface IAddressDocumentVerificationClient
    {
        /// <summary>
        ///     Creates a new address document verification
        /// </summary>
        Task<AddressDocumentVerificationResponse> CreateAddressDocumentVerification(AddressDocumentVerificationRequest addressDocumentVerificationRequest, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Retrieves an existing address document verification by ID
        /// </summary>
        Task<AddressDocumentVerificationResponse> GetAddressDocumentVerification(string addressDocumentVerificationId, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Anonymizes an address document verification by removing personal data
        /// </summary>
        Task<AddressDocumentVerificationResponse> AnonymizeAddressDocumentVerification(string addressDocumentVerificationId, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Creates a new address document verification attempt
        /// </summary>
        Task<AddressDocumentVerificationAttemptResponse> CreateAddressDocumentVerificationAttempt(string addressDocumentVerificationId, AddressDocumentVerificationAttemptRequest attemptRequest, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Retrieves all attempts for an address document verification
        /// </summary>
        Task<AddressDocumentVerificationAttemptsResponse> GetAddressDocumentVerificationAttempts(string addressDocumentVerificationId, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Retrieves a page of attempts for an address document verification
        /// </summary>
        /// <param name="addressDocumentVerificationId">the address document verification ID</param>
        /// <param name="query">the pagination query parameters (skip and limit)</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the address document verification attempts response</returns>
        Task<AddressDocumentVerificationAttemptsResponse> GetAddressDocumentVerificationAttempts(string addressDocumentVerificationId, AttemptsQuery query, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Retrieves a specific attempt for an address document verification
        /// </summary>
        Task<AddressDocumentVerificationAttemptResponse> GetAddressDocumentVerificationAttempt(string addressDocumentVerificationId, string attemptId, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Retrieves the PDF report for an address document verification
        /// </summary>
        Task<AddressDocumentVerificationReportResponse> GetAddressDocumentVerificationReport(string addressDocumentVerificationId, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Retrieves the assets (the document image) uploaded for an address document verification attempt.
        ///     Beta.
        /// </summary>
        /// <param name="addressDocumentVerificationId">the address document verification ID</param>
        /// <param name="attemptId">the attempt ID</param>
        /// <param name="query">the pagination query parameters (skip and limit)</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the address document verification attempt assets response</returns>
        Task<AddressDocumentVerificationAttemptAssetsResponse> GetAddressDocumentVerificationAttemptAssets(string addressDocumentVerificationId, string attemptId, AttemptAssetsQuery query = null, CancellationToken cancellationToken = default);
    }
}
