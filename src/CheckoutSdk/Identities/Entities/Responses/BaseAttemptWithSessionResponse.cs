namespace Checkout.Identities.Entities.Responses
{
    /// <summary>
    /// Base class for attempt responses with session information
    /// </summary>
    public abstract class BaseAttemptWithSessionResponse<TStatus> : BaseAttemptResponse<TStatus>
    {
        /// <summary>
        /// The URL to redirect the applicant to after the attempt
        /// </summary>
        public string RedirectUrl { get; set; }

        /// <summary>
        /// The applicant's details
        /// </summary>
        public ClientInformation ClientInformation { get; set; }

        /// <summary>
        /// The details of the attempt
        /// </summary>
        public ApplicantSessionInformation ApplicantSessionInformation { get; set; }
    }
}