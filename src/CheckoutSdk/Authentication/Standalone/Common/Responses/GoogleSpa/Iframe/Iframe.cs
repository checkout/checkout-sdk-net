namespace Checkout.Authentication.Standalone.Common.Responses.GoogleSpa.Iframe
{
    /// <summary>
    /// iframe
    /// Details of the challenge iframe displayed in the Cardholder browser window
    /// </summary>
    public class Iframe
    {
        /// <summary>
        /// Height of the challenge iframe displayed in the Cardholder browser window.
        /// [Optional]
        /// </summary>
        public string Height { get; set; }

        /// <summary>
        /// Width of the challenge iframe displayed in the Cardholder browser window.
        /// [Optional]
        /// </summary>
        public string Width { get; set; }
    }
}