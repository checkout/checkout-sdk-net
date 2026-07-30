using Checkout.Identities.Entities;

namespace Checkout.Identities.AddressDocumentVerification.Requests
{
    public class AddressDocumentVerificationRequest
    {
        /// <summary>
        /// The applicant's unique identifier
        /// [Required]
        /// Pattern: ^aplt_\w+$
        /// </summary>
        public string ApplicantId { get; set; }

        /// <summary>
        /// Your configuration ID
        /// [Required]
        /// Pattern: ^usj_[a-z2-7]{26}$
        /// </summary>
        public string UserJourneyId { get; set; }

        /// <summary>
        /// The personal details provided by the applicant
        /// </summary>
        public DeclaredData DeclaredData { get; set; }
    }
}
