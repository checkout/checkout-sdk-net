using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Marketplace
{
    public class Company
    {
        public string BusinessRegistrationNumber { get; set; }

        public string LegalName { get; set; }

        public string TradingName { get; set; }

        public Address PrincipalAddress { get; set; }

        public Address RegisteredAddress { get; set; }

        public IList<Representative> Representatives { get; set; }
    }
}