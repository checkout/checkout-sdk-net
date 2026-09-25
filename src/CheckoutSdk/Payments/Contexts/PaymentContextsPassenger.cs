using System;
using Newtonsoft.Json;

namespace Checkout.Payments.Contexts
{
    /// <summary>
    /// Contains information about a passenger on the flight.
    /// </summary>
    public class PaymentContextsPassenger
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
        [JsonConverter(typeof(ShortDateTimeConverter))]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Contains information about the passenger's address.
        /// [Optional]
        /// </summary>
        public PassengerAddress Address { get; set; }
    }
}
