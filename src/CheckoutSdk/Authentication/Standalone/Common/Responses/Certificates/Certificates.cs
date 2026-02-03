using System.Collections.Generic;

namespace Checkout.Authentication.Standalone.Common.Responses.Certificates
{
    /// <summary>
    /// certificates
    /// Public certificates specific to a Directory Server (DS) for encrypting device data and verifying ACS signed
    /// content. Required when channel is app.
    /// </summary>
    public class Certificates
    {
        /// <summary>
        /// A public certificate provided by the DS for encrytion of device data. It is a base64 URL encoded JSON web
        /// key.
        /// [Required]
        /// &lt;= 1024
        /// </summary>
        public string DsPublic { get; set; }

        /// <summary>
        /// Certificate authority (CA) public certificate (root) of the DS-CA. This certificate is used to validate the
        /// ACS signed content JSON web signature (JWS) object. It is a base64 URL encoded DER encoded X.509.
        /// [Optional]
        /// &lt;= 1024
        /// </summary>
        public string CaPublic { get; set; }

        /// <summary>
        /// An array of available certificate authority (CA) public certificates (root) of the DS-CA.
        /// The certificates are used to validate the JSON web signature (JWS) object signed by the access control
        /// server (ACS). Each array element is a Base64 URL-encoded DER-encoded X.509 certificate.
        /// [Optional]
        /// </summary>
        public IList<string> CaPublicAll { get; set; }
    }
}