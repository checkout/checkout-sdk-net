using System.Collections.Generic;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// Industry-specific information.
    /// </summary>
    public class Industry
    {
        /// <summary>
        /// Details about the airline tickets and flights the customer booked.
        /// [Optional]
        /// </summary>
        /// <remarks>
        /// Maps the specification property <c>airline</c>, which is an array. This was previously
        /// a single object named <c>AirlineData</c>, so it serialized as the object
        /// <c>airline_data</c>: a key the API does not define, meaning the value never reached
        /// the gateway.
        /// </remarks>
        public IList<AirlineData> Airline { get; set; }

        /// <summary>
        /// Details about the accommodation the customer booked.
        /// [Optional]
        /// </summary>
        /// <remarks>
        /// Maps the specification property <c>accommodation</c>, which is an array. This was
        /// previously a single object named <c>AccommodationData</c>, so it serialized as the
        /// object <c>accommodation_data</c>: a key the API does not define, meaning the value
        /// never reached the gateway.
        /// </remarks>
        public IList<AccommodationData> Accommodation { get; set; }
    }
}
