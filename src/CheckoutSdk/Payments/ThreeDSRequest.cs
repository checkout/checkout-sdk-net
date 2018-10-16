using Newtonsoft.Json;

namespace Checkout.Payments
{
    /// <summary>
    /// Information required for 3D-Secure payments
    /// </summary>
    public class ThreeDSRequest
    {
        /// <summary>
        /// Gets or sets a value that indicates whether to process this payment as a 3D-Secure.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether to attempt a 3D-Secure payment as non-3DS should the card issuer not be enrolled.
        /// </summary>
        [JsonProperty(PropertyName = "attempt_n3d")]
        public bool? AttemptN3D { get; set; }
        
        /// <summary>
        /// Gets or sets the Electronic Commerce Indicator security level associated with the 3D-Secure enrollment result. Required if using a third party MPI.
        /// </summary>
        public string Eci { get; set; }
        
        /// <summary>
        /// Gets or sets the cryptographic identifier used by the card schemes to validate the cardholder authentication result (3D-Secure). Required if using an external MPI.
        /// </summary>
        public string Cryptogram { get; set; }
        
        /// <summary>
        /// Gets or sets the 3D-Secure transaction identifier. Required if using an external MPI.
        /// </summary>
        public string Xid { get; set; }

        public static implicit operator ThreeDSRequest(bool enabled)
        {
            return new ThreeDSRequest { Enabled = enabled };
        }
    }
}
