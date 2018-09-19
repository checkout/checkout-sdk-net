using Newtonsoft.Json;

namespace Checkout.Payments
{
    public class ThreeDsEnrollment
    {
        /// <summary>
        /// Indicates whether this was a 3D-Secure payment downgraded to Non-3D-Secure (when attempt_n3d is specified)
        /// </summary>
        public bool Downgraded { get; set; }
        /// <summary>
        /// Indicates the 3D-Secure enrollment status of the issuer
        /// Y - Issuer enrolled
        /// N - Customer not enrolled
        /// U - Unknown
        /// </summary>
        public string Enrolled { get; set; }
        /// <summary>
        /// Verification to ensure the integrity of the response.
        /// </summary>
        [JsonProperty(PropertyName = "signature_valid")]
        public string SignatureValid { get; set; }
        /// <summary>
        /// Indicates whether or not the cardholder was authenticated
        /// Y - Customer authenticated
        /// N - Customer not authenticated
        /// A - An authentication attempt occurred but could not be completed
        /// U - Unable to perform authentication
        /// </summary>
        public string AuthenticationResponse { get; set; }
        /// <summary>
        /// Defines the E-Commerce Indicator security level associated with the payment
        /// </summary>
        public string Eci { get; set; }
        /// <summary>
        /// Cryptographic identifier used by the card schemes to validate the integrity of the 3D secure payment data
        /// </summary>
        public string Cavv { get; set; }
        /// <summary>
        /// Unique identifier for the transaction assigned by the MPI
        /// </summary>
        public string Xid { get; set; }
    }
}