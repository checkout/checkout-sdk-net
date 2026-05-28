using System;
using Checkout.Common;

namespace Checkout.Payments
{
    /// <summary>
    /// Contains information about a passenger on the flight.
    /// </summary>
    public class Passenger
    {
        /// <summary>
        /// The passenger's first name.
        /// [Optional]
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The passenger's last name.
        /// [Optional]
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The passenger's date of birth.
        /// [Optional]
        /// Format: yyyy-MM-dd
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Contains information about the passenger's address.
        /// [Optional]
        /// </summary>
        public PassengerAddress Address { get; set; }
    }

    /// <summary>
    /// Contains information about the passenger's address.
    /// </summary>
    public class PassengerAddress
    {
        /// <summary>
        /// The two-letter ISO country code of the passenger's country of residence.
        /// [Optional]
        /// </summary>
        public CountryCode? Country { get; set; }
    }
}
