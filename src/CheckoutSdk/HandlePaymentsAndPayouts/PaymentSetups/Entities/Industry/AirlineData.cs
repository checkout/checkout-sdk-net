using System.Collections.Generic;
using Checkout.Payments.Contexts;

namespace Checkout.Payments.Setups.Entities
{
    public class AirlineData
    {
        /// <summary>
        /// Details about the airline ticket
        /// </summary>
        public PaymentContextsTicket Ticket { get; set; }

        /// <summary>
        /// Details about the flight passenger(s)
        /// </summary>
        public IList<PaymentContextsPassenger> Passengers { get; set; }

        /// <summary>
        /// Details about the flight leg(s) booked by the customer
        /// </summary>
        public IList<PaymentContextsFlightLegDetails> FlightLegDetails { get; set; }
    }
}