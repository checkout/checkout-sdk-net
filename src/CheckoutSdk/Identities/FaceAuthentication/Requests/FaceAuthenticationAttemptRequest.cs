using Checkout.Identities.Entities;

namespace Checkout.Identities.FaceAuthentication.Requests
{
    /// <summary>
    /// Request to create a face authentication attempt.
    /// </summary>
    public class FaceAuthenticationAttemptRequest
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
        public ClientInformation ClientInformation { get; set; }
    }
}
