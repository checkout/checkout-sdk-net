using Checkout.Identities.Entities;

namespace Checkout.Identities.IdDocumentVerification.Requests
{
    public class IdDocumentVerificationRequest
    {
        /// <summary>
        /// The applicant's unique identifier
        /// [Required]
        /// </summary>
        public string ApplicantId { get; set; }

        /// <summary>
        /// Your configuration ID
        /// [Required]
        /// </summary>
        public string UserJourneyId { get; set; }

        /// <summary>
        /// The personal details provided by the applicant
        /// </summary>
        public DeclaredData DeclaredData { get; set; }
    }
}