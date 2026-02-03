using Checkout.Common;
using System;

namespace Checkout.ApplePay.Responses
{
    public class UploadCertificateResponse : Resource
    {
        /// <summary>
        /// The identifier of the account domain
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Hash of the certificate public key
        /// </summary>
        public string PublicKeyHash { get; set; }

        /// <summary>
        /// When the certificate is valid from
        /// </summary>
        public DateTime? ValidFrom { get; set; }

        /// <summary>
        /// When the certificate is valid until
        /// </summary>
        public DateTime? ValidUntil { get; set; }
    }
}