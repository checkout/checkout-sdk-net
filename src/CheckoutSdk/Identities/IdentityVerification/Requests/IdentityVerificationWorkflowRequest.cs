using Checkout.Identities.Entities;

namespace Checkout.Identities.IdentityVerification.Requests
{
    public class IdentityVerificationWorkflowRequest
    {
        /// <summary>
        /// Reference for the verification (Required, max 255 characters)
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Applicant ID to verify (Required)
        /// </summary>
        public string ApplicantId { get; set; }

        /// <summary>
        /// Client information for the verification (Optional)
        /// </summary>
        public ClientInformation ClientInformation { get; set; }

        /// <summary>
        /// Declared data for the verification (Optional)
        /// </summary>
        public DeclaredData DeclaredData { get; set; }
    }
}