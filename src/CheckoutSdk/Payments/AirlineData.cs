using System.Collections.Generic;
using Newtonsoft.Json;

namespace Checkout.Payments
{
    /// <summary>
    /// Contains information about the airline ticket and flights booked by the customer.
    /// </summary>
    public class AirlineData
    {
        /// <summary>
        /// Contains information about the airline ticket.
        /// [Optional]
        /// </summary>
        public Ticket Ticket { get; set; }

        /// <summary>
        /// Contains information about the passenger(s) on the flight.
        /// [Optional]
        /// </summary>
        /// <remarks>
        /// The <c>GET /payments/{id}</c> response returns this as an array, and some payment
        /// methods return a single object. Both shapes deserialize here; a single object becomes
        /// a one-element list.
        /// <para>
        /// Serialization emits an <b>object</b> for one passenger and an array only for several,
        /// because the live API accepts an object on every request surface but an array only on
        /// <c>POST /payments</c>. See <see cref="SingleOrArrayConverter{T}"/> for the
        /// sandbox-verified matrix.
        /// </para>
        /// </remarks>
        [JsonConverter(typeof(SingleOrArrayConverter<Passenger>))]
        public IList<Passenger> Passenger { get; set; }

        /// <summary>
        /// Contains information about the flight leg(s) booked by the customer.
        /// [Optional]
        /// </summary>
        public IList<FlightLegDetails> FlightLegDetails { get; set; }

        /// <summary>
        /// Omits <c>passenger</c> entirely when there are no passengers.
        /// </summary>
        /// <remarks>
        /// Newtonsoft honours <c>ShouldSerializePassenger</c>. Both an empty array and an explicit
        /// null are rejected with <c>processing_airline_data_0_passenger_invalid</c>, so the
        /// property has to be absent rather than empty.
        /// </remarks>
        public bool ShouldSerializePassenger()
        {
            return Passenger != null && Passenger.Count > 0;
        }
    }
}
