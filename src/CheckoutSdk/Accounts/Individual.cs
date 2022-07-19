using Checkout.Common;

namespace Checkout.Accounts
{
    public class Individual
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string LegalName { get; set; }

        public string TradingName { get; set; }

        public string NationalTaxId { get; set; }

        public Address RegisteredAddress { get; set; }

        public DateOfBirth DateOfBirth { get; set; }

        public Identification Identification { get; set; }
    }
}