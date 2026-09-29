using System;
using Newtonsoft.Json;

namespace Checkout.Payments.Contexts
{
    /// <summary>
    /// Contains information about the airline ticket.
    /// </summary>
    public class PaymentContextsTicket
    {
        /// <summary>
        /// The ticket's unique identifier.
        /// [Optional]
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Date the airline ticket was issued.
        /// [Optional]
        /// Format: yyyy-MM-dd
        /// </summary>
        [JsonConverter(typeof(ShortDateTimeConverter))]
        public DateTime? IssueDate { get; set; }

        /// <summary>
        /// Carrier code of the ticket issuer.
        /// [Optional]
        /// </summary>
        public string IssuingCarrierCode { get; set; }

        /// <summary>
        /// C = Car rental reservation, A = Airline flight reservation,
        /// B = Both car rental and airline flight reservations included, N = Unknown.
        /// [Optional]
        /// </summary>
        public string TravelPackageIndicator { get; set; }

        /// <summary>
        /// The name of the travel agency.
        /// [Optional]
        /// </summary>
        public string TravelAgencyName { get; set; }

        /// <summary>
        /// The unique identifier from IATA or ARC for the travel agency that issues the ticket.
        /// [Optional]
        /// </summary>
        public string TravelAgencyCode { get; set; }
    }
}
