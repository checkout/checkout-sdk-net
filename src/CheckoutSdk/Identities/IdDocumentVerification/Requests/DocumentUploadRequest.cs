using Newtonsoft.Json;

namespace Checkout.Identities.IdDocumentVerification.Requests
{
    public class IdDocumentVerificationAttemptRequest
    {
        /// <summary>
        /// The image of the front of the document to upload
        /// [Required]
        /// </summary>
        public string DocumentFront { get; set; }

        /// <summary>
        /// The image of the back of the document to upload
        /// </summary>
        public string DocumentBack { get; set; }
    }
}