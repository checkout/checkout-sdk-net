using System.Collections.Generic;
using Newtonsoft.Json;

namespace Checkout.Payments.Contexts
{
    /// <summary>
    /// Contains information about the airline ticket and flights booked by the customer.
    /// </summary>
    public class PaymentContextsAirlineData
    {
        /// <summary>
        /// Contains information about the airline ticket.
        /// [Optional]
        /// </summary>
        public PaymentContextsTicket Ticket { get; set; }

        /// <summary>
        /// Contains information about the passenger(s) on the flight.
        /// [Optional]
        /// </summary>
        /// <remarks>
        /// Deserialization also accepts a single object, which PayPal returns in place of an
        /// array; it becomes a one-element list.
        /// <para>
        /// Serialization emits an <b>object</b> for one passenger and an array only for several:
        /// <c>POST /payment-contexts</c> rejects the array form with <c>passenger_required</c>.
        /// See <see cref="SingleOrArrayConverter{T}"/> for the sandbox-verified matrix.
        /// </para>
        /// </remarks>
        [JsonConverter(typeof(SingleOrArrayConverter<PaymentContextsPassenger>))]
        public IList<PaymentContextsPassenger> Passenger { get; set; }

        /// <summary>
        /// Contains information about the flight leg(s) booked by the customer.
        /// [Optional]
        /// </summary>
        public IList<PaymentContextsFlightLegDetails> FlightLegDetails { get; set; }

        /// <summary>
        /// Omits <c>passenger</c> entirely when there are no passengers.
        /// </summary>
        /// <remarks>nsoft hono
        /// Newtours <c>ShouldSerializePassenger</c>. Both an empty array and an explicit
        /// null are rejected with <c>processing_airline_data_0_passenger_invalid</c>, so the
        /// property has to be absent rather than empty.
        /// </remarks>
        public bool ShouldSerializePassenger()
        {
            return Passenger != null && Passenger.Count > 0;
        }
    }
}
