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
        /// array; it becomes a one-element list. Serialization always emits an array.
        /// </remarks>
        [JsonConverter(typeof(SingleOrArrayConverter<PaymentContextsPassenger>))]
        public IList<PaymentContextsPassenger> Passenger { get; set; }

        /// <summary>
        /// Contains information about the flight leg(s) booked by the customer.
        /// [Optional]
        /// </summary>
        public IList<PaymentContextsFlightLegDetails> FlightLegDetails { get; set; }
    }
}
