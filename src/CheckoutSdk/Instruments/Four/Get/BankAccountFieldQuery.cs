using Checkout.Common.Four;
#if NET5_0_OR_GREATER
using System.Text.Json.Serialization;
#else
using Newtonsoft.Json;
#endif

namespace Checkout.Instruments.Four.Get
{
    public class BankAccountFieldQuery
    {
#if NET5_0_OR_GREATER
        [JsonPropertyName("account-holder-type")]
#else
        [JsonProperty(PropertyName = "account-holder-type")]
#endif
        public AccountHolderType? AccountHolderType { get; set; }

#if NET5_0_OR_GREATER
        [JsonPropertyName("payment-network")]
#else
        [JsonProperty(PropertyName = "payment-network")]
#endif
        public PaymentNetwork PaymentNetwork { get; set; }
    }
}