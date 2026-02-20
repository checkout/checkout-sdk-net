using Checkout.Identities.Entities;
using Checkout.Identities.Entities.Responses;

namespace Checkout.Identities.IdentityVerification.Responses
{
    /// <summary>
    /// Response for identity verification operations
    /// </summary>
    public class IdentityVerificationAttemptResponse : BaseAttemptWithSessionResponse<AttemptVerificationStatus>
    {
        /// <summary>
        /// The personal details provided by the applicant
        /// [Required]
        /// </summary>
        public DeclaredData DeclaredData { get; set; }
    }
}