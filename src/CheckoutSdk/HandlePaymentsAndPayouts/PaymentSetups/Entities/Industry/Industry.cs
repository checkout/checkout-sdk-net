using System.Collections.Generic;

namespace Checkout.Payments.Setups.Entities
{
    public class Industry
    {
        public PaymentSetupAirline AirlineData { get; set; }

        public IList<PaymentSetupAccommodation> AccommodationData { get; set; }
    }
}