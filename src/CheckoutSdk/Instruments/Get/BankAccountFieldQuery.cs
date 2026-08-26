using Checkout.Common;
using Newtonsoft.Json;

namespace Checkout.Instruments.Get
{
    /// <summary>
    /// The query parameters for GET /validation/bank-accounts/{country}/{currency}.
    /// </summary>
    public class BankAccountFieldQuery
    {
        /// <summary>
        /// The type of account holder that will be used to filter the fields returned.
        /// [Optional]
        /// Enum: "individual" "corporate" "government"
        /// </summary>
        [JsonProperty(PropertyName = "account-holder-type")]
        public AccountHolderType? AccountHolderType { get; set; }

        /// <summary>
        /// The banking network that will be used to filter the fields returned.
        /// [Optional]
        /// </summary>
        [JsonProperty(PropertyName = "payment-network")]
        public PaymentNetwork PaymentNetwork { get; set; }
    }
}
