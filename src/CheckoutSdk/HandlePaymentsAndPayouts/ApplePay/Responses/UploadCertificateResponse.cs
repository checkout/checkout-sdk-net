using Checkout.Common;
using System;

namespace Checkout.HandlePaymentsAndPayouts.ApplePay.Responses
{
    public class UploadCertificateResponse : Resource
    {
        /// <summary>
        /// The identifier of the account domain
        /// [Required]
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Hash of the certificate public key
        /// [Required]
        /// </summary>
        public string PublicKeyHash { get; set; }

        /// <summary>
        /// When the certificate is valid from
        /// [Required]
        /// </summary>
        public DateTime? ValidFrom { get; set; }

        /// <summary>
        /// When the certificate is valid until
        /// [Required]
        /// </summary>
        public DateTime? ValidUntil { get; set; }
    }
}