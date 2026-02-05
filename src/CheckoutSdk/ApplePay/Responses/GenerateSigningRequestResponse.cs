using Checkout.Common;

namespace Checkout.ApplePay.Responses
{
    public class GenerateSigningRequestResponse : Resource
    {
        /// <summary>
        /// The signing request content
        /// </summary>
        /// [Required]
        public string Content { get; set; }
    }
}