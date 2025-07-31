namespace Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.Marketplace
{
    /// <summary>
    /// marketplace
    /// Information related to authentication for payfac payments
    /// </summary>
    public class Marketplace
    {
        /// <summary>
        /// The sub-entity that the authentication is being processed on behalf of
        /// [Optional]
        /// </summary>
        public string SubEntityId { get; set; }
    }
}