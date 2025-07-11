using Checkout.Common;

namespace Checkout.NetworkTokens.Common.Responses
{
    public class NetworkTokenResponse : Resource
    {
        /// <summary> The card details (Required) </summary>
        public Card Card { get; set; }

        /// <summary> Network token details (Required) </summary>
        public NetworkToken NetworkToken { get; set; }

        /// <summary> Token requestor ID (Optional) </summary>
        public string TokenRequestorId { get; set; }
    }
}
