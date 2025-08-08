namespace Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.Completion
{
    /// <summary>
    /// Abstract completion Class
    /// The redirect information needed for callbacks or redirects after the payment is completed
    /// </summary>
    public abstract class AbstractCompletion
    {
        public CompletionType Type;

        protected AbstractCompletion(CompletionType type)
        {
            Type = type;
        }
    }
}