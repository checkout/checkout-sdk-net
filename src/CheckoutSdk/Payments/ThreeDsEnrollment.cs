namespace Checkout.Payments
{
    /// <summary>
    /// 3D-Secure Enrollment Data.
    /// </summary>
    public class ThreeDSEnrollment
    {
        /// <summary>
        /// Gets a value that indicates whether this was a 3D-Secure payment downgraded to Non-3D-Secure (when <see cref="PaymentRequest.AttemptN3D"/> is specified).
        /// </summary>
        public bool Downgraded { get; set; }
        
        /// <summary>
        /// Gets the 3D-Secure enrollment status of the issuer:
        /// Y - Issuer enrolled
        /// N - Customer not enrolled
        /// U - Unknown
        /// </summary>
        public string Enrolled { get; set; }

        /// <summary>
        /// Gets a value that indicates the validity of the signature.
        /// </summary>
        public string SignatureValid { get; set; }

        /// <summary>
        /// Gets a value that indicates whether or not the cardholder was authenticated:
        /// Y - Customer authenticated
        /// N - Customer not authenticated
        /// A - An authentication attempt occurred but could not be completed
        /// U - Unable to perform authentication
        /// </summary>
        public string AuthenticationResponse { get; set; }
               
        /// <summary>
        /// Gets the cryptographic identifier used by the card schemes to validate the integrity of the 3D secure payment data.
        /// </summary>
        public string Cryptogram { get; set; }
        
        /// <summary>
        /// Gets the unique identifier for the transaction assigned by the MPI.
        /// </summary>
        public string Xid { get; set; }
    }
}