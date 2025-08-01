namespace Checkout.Authentication.Standalone.Common.Responses.GoogleSpa.HostedGoogleSpa
{
    /// <summary>
    /// Hosted google_spa Class
    /// Details of Google SPA (Secure Payment Authentication)
    /// </summary>
    public class HostedGoogleSpa : AbstractGoogleSpa
    {
        /// <summary>
        /// Initializes a new instance of the HostedGoogleSpa class.
        /// </summary>
        public HostedGoogleSpa() : base(GoogleSpaType.Hosted)
        {
        }

        /// <summary>
        /// Token for the given PAN provisioned and authenticated
        /// [Optional]
        /// </summary>
        public Token.Token Token { get; set; }
    }
}