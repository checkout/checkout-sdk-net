using Checkout.HandlePaymentsAndPayouts.ApplePay.Entities;

namespace Checkout.HandlePaymentsAndPayouts.ApplePay.Requests
{
    public class GenerateSigningRequestRequest
    {
        /// <summary>
        /// The protocol version of the encryption type used.
        /// Default: "ec_v1"
        /// </summary>
        public ProtocolVersions ProtocolVersion { get; set; } = ProtocolVersions.EcV1;
    }
}