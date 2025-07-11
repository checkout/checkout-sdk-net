using Checkout.Common;

namespace Checkout.NetworkTokens.PostCryptograms.Responses
{
    public class NetworkTokenCryptogramResponse : Resource
    {
        /// <summary>
        /// The cryptogram from the network token. Will only be refreshed for active network tokens and returned when
        /// the network token isn't declined or pending
        /// </summary>
        public string Cryptogram { get; set; }

        /// <summary> Electronic Commerce Indicator (ECI) from the issuer. </summary>
        public string Eci { get; set; }
    }
}