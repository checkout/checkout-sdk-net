using Checkout.Common;
#if NET5_0_OR_GREATER
using System.Text.Json.Serialization;
#else
using Newtonsoft.Json;
#endif


namespace Checkout.Apm.Sepa
{
    public class MandateResponse : Resource
    {
        public string MandateReference { get; set; }

        public string CustomerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

#if NET5_0_OR_GREATER
        [JsonPropertyName("address_line1")]
#else
        [JsonProperty(PropertyName = "address_line1")]
#endif
        public string AddressLine1 { get; set; }

        public string City { get; set; }

        public string Zip { get; set; }

        public CountryCode? Country { get; set; }

        public string MaskedAccountIban { get; set; }

        public string AccountCurrencyCode { get; set; }

        public CountryCode? AccountCountryCode { get; set; }

        public string MandateState { get; set; }

        public string BillingDescriptor { get; set; }

        public string MandateType { get; set; }
    }
}