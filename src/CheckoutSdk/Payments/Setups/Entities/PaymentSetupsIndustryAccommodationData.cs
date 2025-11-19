using System.Collections.Generic;
using Checkout.Common;

namespace Checkout.Payments.Setups.Entities
{
    public class PaymentSetupsIndustryAccommodationData
    {
        public string Name { get; set; }

        public string BookingReference { get; set; }

        public string CheckInDate { get; set; }

        public string CheckOutDate { get; set; }

        public Address Address { get; set; }

        public int? NumberOfRooms { get; set; }

        public IList<PaymentSetupsAccommodationGuest> Guests { get; set; }

        public IList<PaymentSetupsAccommodationRoom> Room { get; set; }
    }
}