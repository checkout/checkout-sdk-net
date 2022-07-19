using Checkout.Common;

namespace Checkout.Payments
{
    public class Passenger
    {
        public PassengerName Name { get; set; }

        public string DateOfBirth { get; set; }

        public CountryCode? CountryCode { get; set; }
    }
}