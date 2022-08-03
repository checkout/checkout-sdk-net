using System.Collections.Generic;

namespace Checkout.Payments
{
    public class AirlineData
    {
        public Ticket Ticket { get; set; }
        
        public Passenger Passenger { get; set; }
        
        public IList<FlightLegDetails> FlightLegDetails { get; set; }
    }
}