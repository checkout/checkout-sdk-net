using Checkout.Identities.Entities;
using Checkout.Identities.Entities.Responses;

namespace Checkout.Identities.IdentityVerification.Responses
{
    /// <summary>
    /// Response for an identity verification attempt
    /// </summary>
    public class IdentityVerificationAttemptResponse : BaseAttemptWithSessionResponse<AttemptVerificationStatus>
    {
        /// <summary>
        /// The applicant's details
        /// [Optional]
        /// </summary>
        public IdentityVerificationClientInformation ClientInformation { get; set; }

        /// <summary>
        /// The personal details provided by the applicant.
        /// </summary>
        /// <remarks>
        /// Not part of the current API specification: the identity-verification attempt response
        /// schema does not declare declared_data, so this property never populates. Retained for
        /// backward compatibility and scheduled for removal in a future major version. Read
        /// declared data from the identity verification response instead.
        /// </remarks>
        public DeclaredData DeclaredData { get; set; }
    }
}
