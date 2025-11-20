using System.Collections.Generic;

namespace Checkout.Payments.Setups.Entities
{
    public class PaymentSetupAirline
    {
        public AirlineTicket Ticket { get; set; }

        public IList<AirlinePassenger> Passengers { get; set; }

        public IList<FlightLegDetails> FlightLegDetails { get; set; }
    }
}