using Checkout.Common;

namespace Checkout.Payments.Setups.Entities
{
    public class AirlinePassenger
    {
        /// <summary>
        /// The passenger's first name as it appears on their travel document
        /// </summary>
        public string FirstName { get; set; }
        
        /// <summary>
        /// The passenger's last name as it appears on their travel document
        /// </summary>
        public string LastName { get; set; }
        
        /// <summary>
        /// The passenger's date of birth in YYYY-MM-DD format
        /// </summary>
        public string DateOfBirth { get; set; }
        
        /// <summary>
        /// The passenger's address information
        /// </summary>
        public Address Address { get; set; }
    }
}