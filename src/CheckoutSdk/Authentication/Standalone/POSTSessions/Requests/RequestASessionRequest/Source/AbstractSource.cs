namespace Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.Source
{
    /// <summary>
    /// Abstract source Class
    /// The source of the authentication.
    /// </summary>
    public abstract class AbstractSource
    {
        public SourceType Type;

        protected AbstractSource(SourceType type)
        {
            Type = type;
        }
    }
}