namespace Checkout.Common
{
    /// <summary>
    /// QR code data returned for payment methods that require a QR code scan.
    /// </summary>
    public class QrCode
    {
        /// <summary>
        /// The QR code as a base64-encoded image.
        /// [Optional]
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// The raw text encoded in the QR code.
        /// [Optional]
        /// </summary>
        public string Text { get; set; }
    }
}
