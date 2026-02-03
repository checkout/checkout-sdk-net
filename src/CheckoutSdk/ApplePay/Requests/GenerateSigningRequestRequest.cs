namespace Checkout.ApplePay.Requests
{
    public class GenerateSigningRequestRequest
    {
        /// <summary>
        /// The protocol version of the encryption type used.
        /// Default: "ec_v1"
        /// </summary>
        public string ProtocolVersion { get; set; } = "ec_v1";
    }
}