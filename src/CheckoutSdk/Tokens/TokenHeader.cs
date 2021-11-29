using Newtonsoft.Json;

namespace Checkout.Tokens
{
    public sealed class TokenHeader
    {
        [JsonProperty("ephemeralPublicKey")] private string EphemeralPublicKey { get; set; }

        [JsonProperty("publicKeyHash")] private string PublicKeyHash { get; set; }

        [JsonProperty("transactionId")] private string TransactionId { get; set; }
    }
}