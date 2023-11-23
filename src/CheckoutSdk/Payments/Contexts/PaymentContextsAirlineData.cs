using System.Collections.Generic;

namespace Checkout.Payments.Contexts
{
    public class PaymentContextsAirlineData
    {
        public IList<PaymentContextsTicket> Ticket { get; set; }

        public IList<PaymentContextsPassenger> Passenger { get; set; }

        public IList<PaymentContextsFlightLegDetails> FlightLegDetails { get; set; }
    }
}