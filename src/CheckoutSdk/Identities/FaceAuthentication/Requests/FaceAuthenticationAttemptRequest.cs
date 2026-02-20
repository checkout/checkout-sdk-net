using Checkout.Identities.Entities;

namespace Checkout.Identities.FaceAuthentication.Requests
{
    public class FaceAuthenticationAttemptRequest
    {
        /// <summary>
        /// The URL to redirect the applicant to after the attempt
        /// [Required]
        /// </summary>
        public string RedirectUrl { get; set; }

        /// <summary>
        /// The applicant's details (Optional)
        /// </summary>
        public ClientInformation ClientInformation { get; set; }
    }
}