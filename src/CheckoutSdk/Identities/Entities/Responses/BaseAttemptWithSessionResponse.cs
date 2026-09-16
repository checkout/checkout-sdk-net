namespace Checkout.Identities.Entities.Responses
{
    /// <summary>
    /// Base class for attempt responses with session information
    /// </summary>
    public abstract class BaseAttemptWithSessionResponse<TStatus> : BaseAttemptResponse<TStatus>
    {
        /// <summary>
        /// The URL to redirect the applicant to after the attempt
        /// [Required]
        /// </summary>
        public string RedirectUrl { get; set; }

        /// <summary>
        /// The applicant's mobile phone number, if sharing the attempt URL via SMS.
        /// [Optional]
        /// </summary>
        public PhoneNumber PhoneNumber { get; set; }

        /// <summary>
        /// The details of the attempt
        /// [Optional]
        /// </summary>
        public ApplicantSessionInformation ApplicantSessionInformation { get; set; }
    }
}
