using Checkout.Common.Four;
using Newtonsoft.Json;

namespace Checkout.Instruments.Four.Get
{
    public class BankAccountFieldQuery
    {
        [JsonProperty(PropertyName = "account-holder-type")]
        public AccountHolderType? AccountHolderType { get; set; }

        [JsonProperty(PropertyName = "payment-network")]
        public PaymentNetwork PaymentNetwork { get; set; }
    }
}