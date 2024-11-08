using Checkout.Common;
using System;
using System.Collections.Generic;

namespace Checkout.Payments.Contexts
{
    public class PaymentContextsAccommodationData
    {
        public string Name { get; set; }

        public string BookingReference { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public Address Address { get; set; }

        public CountryCode State { get; set; }

        public CountryCode Country { get; set; }

        public string City { get; set; }

        public int? NumberOfRooms { get; set; }

        public List<PaymentContextsGuests> Guests { get; set; }

        public List<PaymentContextsAccommodationRoom> Room { get; set; }
    }
}