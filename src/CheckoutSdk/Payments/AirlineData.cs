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
        /// The API returns this as an array. Some payment methods, PayPal among them, send a
        /// single object instead, which the specification allows on the payment sessions, hosted
        /// payments and payment links interfaces. Both shapes deserialize here; a single object
        /// becomes a one-element list. Serialization always emits an array.
        /// </remarks>
        [JsonConverter(typeof(SingleOrArrayConverter<Passenger>))]
        public IList<Passenger> Passenger { get; set; }

        /// <summary>
        /// Contains information about the flight leg(s) booked by the customer.
        /// [Optional]
        /// </summary>
        public IList<FlightLegDetails> FlightLegDetails { get; set; }
    }
}
