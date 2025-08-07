namespace Checkout.Authentication.Standalone.Common.Responses.GoogleSpa
{
    /// <summary>
    /// Non Hosted google_spa Class
    /// Details of Google SPA (Secure Payment Authentication)
    /// </summary>
    public class GoogleSpa
    {
        /// <summary>
        /// Fully qualified URL of the Google SPA (Secure Payment Authentication) frontend for user challenge/unlock
        /// [Optional]
        /// </summary>
        public string ChallengeUrl { get; set; }

        /// <summary>
        /// Indicates the amount of time (in minutes) allowed for establishing the initial connection to the user's
        /// specific authentication session, as determined by the challenge_url)
        /// [Optional]
        /// </summary>
        public string InitialTimeout { get; set; }

        /// <summary>
        /// Indicates maximum amount of time (in minutes) allowed for completing all PSP-Google message exchanges for
        /// current authentication session
        /// [Optional]
        /// </summary>
        public string MaxTimeout { get; set; }

        /// <summary>
        /// Details of the challenge iframe displayed in the Cardholder browser window
        /// [Optional]
        /// </summary>
        public Iframe.Iframe Iframe { get; set; }

        /// <summary>
        /// Token for the given PAN provisioned and authenticated
        /// [Optional]
        /// </summary>
        public Token.Token Token { get; set; }
    }
}