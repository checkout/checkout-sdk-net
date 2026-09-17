using System.Collections.Generic;
using Checkout.Common;
using Checkout.Payments.Contexts;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// Contains information about the airline ticket and flights booked by the customer.
    /// </summary>
    public class AirlineData
    {
        /// <summary>
        /// Details about the airline ticket.
        /// [Optional]
        /// </summary>
        public PaymentContextsTicket Ticket { get; set; }

        /// <summary>
        /// The list of passengers on the flight.
        /// [Optional]
        /// </summary>
        public IList<PaymentContextsPassenger> Passengers { get; set; }

        /// <summary>
        /// The list of flight legs booked by the customer.
        /// [Optional]
        /// </summary>
        public IList<PaymentContextsFlightLegDetails> FlightLegDetails { get; set; }

        /// <summary>
        /// The total number of passengers on the booking.
        /// [Optional]
        /// </summary>
        public int? TotalNumberOfPassengers { get; set; }

        /// <summary>
        /// The type of travel, for example "international" or "domestic".
        /// [Optional]
        /// </summary>
        public string TravelType { get; set; }

        /// <summary>
        /// The type of trip, for example "one_way" or "round_trip".
        /// [Optional]
        /// </summary>
        public string TripType { get; set; }

        /// <summary>
        /// Specifies whether the booking is refundable.
        /// [Optional]
        /// </summary>
        public bool? Refundable { get; set; }

        /// <summary>
        /// The recipient the ticket is delivered to.
        /// [Optional]
        /// </summary>
        public string DeliveryRecipient { get; set; }

        /// <summary>
        /// Any additional add-ons purchased with the booking, for example "extra_baggage".
        /// [Optional]
        /// </summary>
        public string Ancillaries { get; set; }

        /// <summary>
        /// Details about the travel insurance purchased with the booking.
        /// [Optional]
        /// </summary>
        public AirlineInsurance Insurance { get; set; }
    }

    public class AirlineInsurance
    {
        /// <summary>
        /// The type of insurance purchased.
        /// [Optional]
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The name of the insurance company.
        /// [Optional]
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// The price of the insurance.
        /// [Optional]
        /// </summary>
        public AirlineInsurancePrice Price { get; set; }
    }

    public class AirlineInsurancePrice
    {
        /// <summary>
        /// The insurance price amount.
        /// [Optional]
        /// </summary>
        public decimal? Amount { get; set; }

        /// <summary>
        /// The insurance price currency, as a three-letter ISO currency code.
        /// [Optional]
        /// </summary>
        public Currency? Currency { get; set; }
    }
}