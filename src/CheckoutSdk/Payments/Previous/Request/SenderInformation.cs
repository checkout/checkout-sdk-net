using Checkout.Common;
using Newtonsoft.Json;

namespace Checkout.Payments.Previous.Request
{
    public class SenderInformation
    {
        public string Reference { get; set; }

        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        public string Dob { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public CountryCode? Country { get; set; }

        [JsonProperty(PropertyName = "postalCode")]
        public string PostalCode { get; set; }

        [JsonProperty(PropertyName = "sourceOfFunds")]
        public string SourceOfFunds { get; set; }

        public string Purpose { get; set; }
    }
}