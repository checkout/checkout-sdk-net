using Newtonsoft.Json;

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

    public class DeclaredData
    {
        /// <summary>
        /// The applicant's name
        /// [Required]
        /// </summary>
        public string Name { get; set; }
    }
}