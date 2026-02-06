using Checkout.Identities.FaceAuthentication.Responses;

namespace Checkout.Identities.FaceAuthentication.Requests
{
    public class FaceAuthenticationRequest
    {
        /// <summary>
        /// The applicant's unique identifier (Required)
        /// </summary>
        public string ApplicantId { get; set; }

        /// <summary>
        /// Your configuration ID (Required)
        /// </summary>
        public string UserJourneyId { get; set; }
    }

    public class FaceAuthenticationAttemptRequest
    {
        /// <summary>
        /// The URL to redirect the applicant to after the attempt (Required)
        /// </summary>
        public string RedirectUrl { get; set; }

        /// <summary>
        /// The applicant's details (Optional)
        /// </summary>
        public ClientInformation ClientInformation { get; set; }
    }
}