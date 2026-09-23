using Checkout.Identities.Entities;

namespace Checkout.Identities.IdentityVerification.Requests
{
    /// <summary>
    /// Request to create an identity verification attempt.
    /// </summary>
    public class IdentityVerificationAttemptRequest
    {
        /// <summary>
        /// The URL to redirect the applicant to after the attempt.
        /// [Required]
        /// Format: uri
        /// </summary>
        public string RedirectUrl { get; set; }

        /// <summary>
        /// The applicant's mobile phone number, if sharing the attempt URL via SMS.
        /// [Optional]
        /// </summary>
        public PhoneNumber PhoneNumber { get; set; }

        /// <summary>
        /// The applicant's details.
        /// [Optional]
        /// </summary>
        public IdentityVerificationClientInformation ClientInformation { get; set; }
    }
}
