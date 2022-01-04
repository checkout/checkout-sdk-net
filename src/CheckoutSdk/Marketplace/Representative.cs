using Checkout.Common;
using Newtonsoft.Json;

namespace Checkout.Marketplace
{
    public class Representative
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Address Address { get; set; }

        public Identification Identification { get; set; }

        public Phone Phone { get; set; }

        public DateOfBirth DateOfBirth { get; set; }
    }
}