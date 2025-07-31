namespace Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.GoogleSpa
{
    /// <summary>
    /// google_spa
    /// This object contains the Google SPA properties (non-hosted only)
    /// </summary>
    public class GoogleSpa
    {
        /// <summary>
        /// Fully qualified URL for redirecting the user's browser session after authentication. For example, this field
        /// may be the merchant's website for purchase confirmation once payment is complete. Required if in full
        /// redirect (not iframe) mode.
        /// [Optional]
        /// </summary>
        public string ContinueUrl { get; set; }
    }
}
