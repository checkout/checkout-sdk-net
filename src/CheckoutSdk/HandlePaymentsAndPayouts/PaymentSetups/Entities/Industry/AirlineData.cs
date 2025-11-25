using System.Collections.Generic;
using Checkout.Payments.Contexts;

namespace Checkout.Payments.Setups.Entities
{
    public class AirlineData
    {
        /// <summary>
        /// The airline ticket information
        /// </summary>
        public PaymentContextsTicket Ticket { get; set; }

        /// <summary>
        /// List of passengers on the flight
        /// </summary>
        public IList<PaymentContextsPassenger> Passengers { get; set; }

        /// <summary>
        /// Details of each leg of the flight journey
        /// </summary>
        public IList<PaymentContextsFlightLegDetails> FlightLegDetails { get; set; }
    }
}