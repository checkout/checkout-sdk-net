namespace Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.Completion.NonHostedCompletion
{
    /// <summary>
    /// non_hosted completion Class
    /// The redirect information needed for callbacks or redirects after the payment is completed
    /// </summary>
    public class NonHostedCompletion : AbstractCompletion
    {
        /// <summary>
        /// Initializes a new instance of the NonHostedCompletion class.
        /// </summary>
        public NonHostedCompletion() : base(CompletionType.NonHosted)
        {
        }

        /// <summary>
        /// For non-hosted sessions, you can define a URL to be called once the session is complete
        /// [Optional]
        /// <uri>
        /// &lt;= 256
        /// </summary>
        public string CallbackUrl { get; set; }

        /// <summary>
        /// For non-hosted sessions, you can define a URL to be your own challenge notification endpoint. When not
        /// provided, a Checkout.com endpoint will be used.
        /// [Optional]
        /// <uri>
        /// &lt;= 256
        /// </summary>
        public string ChallengeNotificationUrl { get; set; }
    }
}