namespace Checkout.Identities.AddressDocumentVerification.Requests
{
    public class AddressDocumentVerificationAttemptRequest
    {
        /// <summary>
        /// The address document image to upload
        /// [Required]
        /// Format: binary
        /// </summary>
        public string Document { get; set; }
    }
}
