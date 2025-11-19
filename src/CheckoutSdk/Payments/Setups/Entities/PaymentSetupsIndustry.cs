using System.Collections.Generic;

namespace Checkout.Payments.Setups.Entities
{
    public class PaymentSetupsIndustry
    {
        public PaymentSetupsIndustryAirlineData AirlineData { get; set; }

        public IList<PaymentSetupsIndustryAccommodationData> AccommodationData { get; set; }
    }
}