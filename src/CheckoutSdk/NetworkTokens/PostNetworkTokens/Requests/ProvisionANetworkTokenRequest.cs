using Checkout.NetworkTokens.PostNetworkTokens.Requests.Sources;

namespace Checkout.NetworkTokens.PostNetworkTokens.Requests
{
    public class ProvisionANetworkTokenRequest
    {
        /// <summary> The source object (Required) </summary>
        public AbstractSource Source { get; set; }
    }
}
