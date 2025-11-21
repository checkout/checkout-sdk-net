using System.Collections.Generic;

namespace Checkout.Payments.Setups.Entities
{
    public class PaymentSetupAirline
    {
        /// <summary>
        /// The airline ticket information
        /// </summary>
        public AirlineTicket Ticket { get; set; }

        /// <summary>
        /// List of passengers on the flight
        /// </summary>
        public IList<AirlinePassenger> Passengers { get; set; }

        /// <summary>
        /// Details of each leg of the flight journey
        /// </summary>
        public IList<FlightLegDetails> FlightLegDetails { get; set; }
    }
}