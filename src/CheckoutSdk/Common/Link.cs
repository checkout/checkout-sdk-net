namespace Checkout.Common
{
    public class Link
    {
        /// <summary>
        /// The URL for the link.
        /// [Optional]
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// The title for the link.
        /// [Optional]
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Mobile redirect links for Android and iOS, returned in 202 redirect responses.
        /// [Optional]
        /// </summary>
        public MobileRedirectLink Mobile { get; set; }

        /// <summary>
        /// QR code data for payment methods that require a QR code scan, returned in 202 responses.
        /// [Optional]
        /// </summary>
        public QrCode QrCode { get; set; }
    }
}
