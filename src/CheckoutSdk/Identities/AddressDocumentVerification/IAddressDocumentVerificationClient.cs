using System.Threading;
using System.Threading.Tasks;
using Checkout.Identities.AddressDocumentVerification.Requests;
using Checkout.Identities.AddressDocumentVerification.Responses;

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
        ///     Retrieves a specific attempt for an address document verification
        /// </summary>
        Task<AddressDocumentVerificationAttemptResponse> GetAddressDocumentVerificationAttempt(string addressDocumentVerificationId, string attemptId, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Retrieves the PDF report for an address document verification
        /// </summary>
        Task<AddressDocumentVerificationReportResponse> GetAddressDocumentVerificationReport(string addressDocumentVerificationId, CancellationToken cancellationToken = default);
    }
}
