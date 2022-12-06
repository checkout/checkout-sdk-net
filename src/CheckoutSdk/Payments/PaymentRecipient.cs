using Checkout.Common;
using Newtonsoft.Json;

namespace Checkout.Payments
{
    public class PaymentRecipient
    {
        [JsonProperty("dob")] public string DateOfBirth { get; set; }

        public string AccountNumber { get; set; }

        public string Zip { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public CountryCode? Country { get; set; }
    }
}