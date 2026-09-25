using System;
using Newtonsoft.Json;

namespace Checkout.Payments.Contexts
{
    /// <summary>
    /// Contains information about a guest staying at the accommodation.
    /// </summary>
    public class PaymentContextsGuests
    {
        /// <summary>
        /// The first name of the guest.
        /// [Optional]
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the guest.
        /// [Optional]
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The date of birth of the guest.
        /// [Optional]
        /// Format: yyyy-MM-dd
        /// </summary>
        [JsonConverter(typeof(ShortDateTimeConverter))]
        public DateTime? DateOfBirth { get; set; }
    }
}
