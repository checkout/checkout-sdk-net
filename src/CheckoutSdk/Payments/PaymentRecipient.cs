using Checkout.Common;
#if NET5_0_OR_GREATER
using System.Text.Json.Serialization;
#else
using Newtonsoft.Json;
#endif

namespace Checkout.Payments
{
    public class PaymentRecipient
    {
#if NET5_0_OR_GREATER
        [JsonPropertyName("dob")]
#else
        [JsonProperty(PropertyName = "dob")]
#endif
        public string DateOfBirth { get; set; }

        public string AccountNumber { get; set; }

        public string Zip { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public CountryCode? Country { get; set; }
    }
}