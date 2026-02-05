using Checkout.Common;
using System;

namespace Checkout.HandlePaymentsAndPayouts.ApplePay.Responses
{
    public class UploadCertificateResponse : Resource
    {
        /// <summary>
        /// The identifier of the account domain
        /// </summary>
        /// [Required]
        public string Id { get; set; }

        /// <summary>
        /// Hash of the certificate public key
        /// </summary>
        /// [Required]
        public string PublicKeyHash { get; set; }

        /// <summary>
        /// When the certificate is valid from
        /// </summary>
        /// [Required]
        public DateTime? ValidFrom { get; set; }

        /// <summary>
        /// When the certificate is valid until
        /// </summary>
        /// [Required]
        public DateTime? ValidUntil { get; set; }
    }
}