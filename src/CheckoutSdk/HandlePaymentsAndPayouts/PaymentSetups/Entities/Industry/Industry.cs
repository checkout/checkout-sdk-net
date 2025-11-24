using System.Collections.Generic;

namespace Checkout.Payments.Setups.Entities
{
    public class Industry
    {
        /// <summary>
        /// Airline industry-specific data for flight bookings and related payments
        /// </summary>
        public PaymentSetupAirline AirlineData { get; set; }

        /// <summary>
        /// Accommodation industry-specific data for hotel bookings and related payments
        /// </summary>
        public IList<AccommodationData> AccommodationData { get; set; }
    }
}