using Checkout.Common;

namespace Checkout.Sessions
{
    public class SessionAddress : Address
    {
        public string AddressLine3 { get; set; }

        public SessionAddress()
        {
        }

        public SessionAddress(string addressLine1, string addressLine2, string addressLine3, string city, string state,
            string zip, in CountryCode country)
        {
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            City = city;
            State = state;
            Zip = zip;
            Country = country;
            AddressLine3 = addressLine3;
        }
    }
}