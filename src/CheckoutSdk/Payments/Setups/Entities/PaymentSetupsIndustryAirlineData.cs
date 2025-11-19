using System.Collections.Generic;

namespace Checkout.Payments.Setups.Entities
{
    public class PaymentSetupsIndustryAirlineData
    {
        public PaymentSetupsAirlineTicket Ticket { get; set; }

        public IList<PaymentSetupsAirlinePassenger> Passengers { get; set; }

        public IList<PaymentSetupsFlightLegDetails> FlightLegDetails { get; set; }
    }
}