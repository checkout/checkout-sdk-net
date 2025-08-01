namespace Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.Completion.HostedCompletion
{
    /// <summary>
    /// hosted completion Class
    /// The redirect information needed for callbacks or redirects after the payment is completed
    /// </summary>
    public class HostedCompletion : AbstractCompletion
    {
        /// <summary>
        /// Initializes a new instance of the HostedCompletion class.
        /// </summary>
        public HostedCompletion() : base(CompletionType.Hosted)
        {
        }

        /// <summary>
        /// For hosted sessions, this overrides the default success redirect URL configured on your account
        /// [Required]
        /// <uri>
        /// <= 256
        /// </summary>
        public string SuccessUrl { get; set; }

        /// <summary>
        /// For hosted sessions, this overrides the default failure redirect URL configured on your account
        /// [Required]
        /// <uri>
        /// <= 256
        /// </summary>
        public string FailureUrl { get; set; }
    }
}