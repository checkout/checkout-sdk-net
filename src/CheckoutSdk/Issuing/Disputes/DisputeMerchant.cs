using System.Collections.Generic;

namespace Checkout.Issuing.Disputes
{
    public class DisputeMerchant
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string CountryCode { get; set; }

        public string CategoryCode { get; set; }

        public IList<string> Evidence { get; set; }
    }
}