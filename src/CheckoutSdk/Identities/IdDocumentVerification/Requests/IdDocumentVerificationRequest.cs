using Newtonsoft.Json;

namespace Checkout.Identities.IdDocumentVerification.Requests
{
    public class IdDocumentVerificationRequest
    {
        /// <summary>
        /// The applicant's unique identifier
        /// </summary>
        [JsonProperty("applicant_id")]
        public string ApplicantId { get; set; }

        /// <summary>
        /// Your configuration ID
        /// </summary>
        [JsonProperty("user_journey_id")]
        public string UserJourneyId { get; set; }

        /// <summary>
        /// The personal details provided by the applicant
        /// </summary>
        [JsonProperty("declared_data")]
        public DeclaredData DeclaredData { get; set; }
    }

    public class DeclaredData
    {
        /// <summary>
        /// The applicant's name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}