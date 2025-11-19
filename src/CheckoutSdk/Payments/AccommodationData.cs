using Checkout.Common;
using Checkout.Payments.Contexts;
using System;
using System.Collections.Generic;

namespace Checkout.Payments
{
    public class AccommodationData
    {
        /// <summary>
        /// The name of the accommodation (hotel, resort, etc.)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The booking reference or confirmation number
        /// </summary>
        public string BookingReference { get; set; }

        /// <summary>
        /// The check-in date in YYYY-MM-DD format
        /// </summary>
        public DateTime CheckInDate { get; set; }

        /// <summary>
        /// The check-out date in YYYY-MM-DD format
        /// </summary>
        public DateTime CheckOutDate { get; set; }

        /// <summary>
        /// The address of the accommodation
        /// </summary>
        public Address Address { get; set; }

        public CountryCode State { get; set; }

        public CountryCode Country { get; set; }

        public string City { get; set; }

        /// <summary>
        /// The number of rooms booked
        /// </summary>
        public int? NumberOfRooms { get; set; }

        /// <summary>
        /// List of guests staying at the accommodation
        /// </summary>
        public List<PaymentContextsGuests> Guests { get; set; }

        /// <summary>
        /// Details of the rooms booked
        /// </summary>
        public List<PaymentContextsAccommodationRoom> Room { get; set; }
    }
}