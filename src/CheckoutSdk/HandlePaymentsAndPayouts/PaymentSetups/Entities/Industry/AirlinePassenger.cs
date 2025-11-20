using Checkout.Common;

namespace Checkout.Payments.Setups.Entities
{
    public class AirlinePassenger
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string DateOfBirth { get; set; }
        
        public Address Address { get; set; }
    }
}