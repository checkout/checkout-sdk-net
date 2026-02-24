namespace Checkout.Identities.FaceAuthentication.Requests
{
    public class FaceAuthenticationRequest
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
    }
}