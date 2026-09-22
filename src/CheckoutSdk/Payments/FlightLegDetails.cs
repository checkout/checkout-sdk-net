using System;
using Newtonsoft.Json;

namespace Checkout.Payments
{
    /// <summary>
    /// Contains information about a flight leg booked by the customer.
    /// </summary>
    public class FlightLegDetails
    {
        /// <summary>
        /// The flight identifier.
        /// [Optional]
        /// </summary>
        public string FlightNumber { get; set; }

        /// <summary>
        /// The IATA 2-letter accounting code (PAX) that identifies the carrier.
        /// This field is required if the airline data includes leg details.
        /// [Optional]
        /// </summary>
        public string CarrierCode { get; set; }

        /// <summary>
        /// A one-letter travel class identifier. The following are common:
        /// F = First class, J = Business class, Y = Economy class, W = Premium economy.
        /// [Optional]
        /// </summary>
        public string ClassOfTravelling { get; set; }

        /// <summary>
        /// The date of the scheduled take off.
        /// [Optional]
        /// Format: yyyy-MM-dd
        /// </summary>
        [JsonConverter(typeof(ShortDateTimeConverter))]
        public DateTime? DepartureDate { get; set; }

        /// <summary>
        /// The time of the scheduled take off.
        /// [Optional]
        /// </summary>
        public string DepartureTime { get; set; }

        /// <summary>
        /// The IATA three-letter airport code of the departure airport.
        /// This field is required if the airline data includes leg details.
        /// [Optional]
        /// </summary>
        public string DepartureAirport { get; set; }

        /// <summary>
        /// The IATA 3-letter airport code of the destination airport.
        /// This field is required if the airline data includes leg details.
        /// [Optional]
        /// </summary>
        public string ArrivalAirport { get; set; }

        /// <summary>
        /// A one-letter code that indicates whether the passenger is entitled to make a stopover.
        /// Can be a space, O if the passenger is entitled to make a stopover, or X if they are not.
        /// [Optional]
        /// </summary>
        public string StopOverCode { get; set; }

        /// <summary>
        /// The fare basis code, alphanumeric.
        /// [Optional]
        /// </summary>
        public string FareBasisCode { get; set; }

        /// <summary>
        /// Not in the current spec, will be removed in a future version.
        /// Serializes as <c>service_class</c>, which the API does not define, so the value is
        /// discarded by the gateway. Use <see cref="ClassOfTravelling"/> instead, which maps the
        /// spec property <c>class_of_travelling</c>.
        /// </summary>
        [Obsolete("Not defined by the API, the gateway discards it. Use ClassOfTravelling, which maps class_of_travelling.")]
        public string ServiceClass { get; set; }
    }
}
