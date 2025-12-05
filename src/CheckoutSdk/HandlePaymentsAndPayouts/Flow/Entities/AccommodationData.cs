
using Checkout.Common;
using Checkout.Payments.Contexts;
using System;
using System.Collections.Generic;

namespace Checkout.HandlePaymentsAndPayouts.Flow.Entities
{
    public class AccommodationData
    {
        /// <summary>
        /// For lodging, contains the lodging name that appears on the storefront/customer receipts. 
        /// For cruise, contains the ship name booked for the cruise.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A unique identifier for the booking.
        /// </summary>
        public string BookingReference { get; set; }

        /// <summary>
        /// For lodging, contains the actual or scheduled date the guest checked-in. 
        /// For cruise, contains the cruise departure date also known as sail date.
        /// </summary>
        public DateTime? CheckInDate { get; set; }

        /// <summary>
        /// For lodging, contains the actual or scheduled date the guest checked-out. 
        /// For cruise, contains the cruise return date also known as sail end date.
        /// </summary>
        public DateTime? CheckOutDate { get; set; }

        /// <summary>
        /// The address details of the accommodation.
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// The state or province of the address country (ISO 3166-2 code of up to two alphanumeric characters).
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// The ISO country code of the address.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// The address city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The total number of rooms booked for the accommodation.
        /// </summary>
        public int? NumberOfRooms { get; set; }

        /// <summary>
        /// Contains information about the guests staying at the accommodation.
        /// </summary>
        public IList<AccountHolderBase> Guests { get; set; }

        /// <summary>
        /// Contains information about the rooms booked by the customer.
        /// </summary>
        public IList<PaymentContextsAccommodationRoom> Rooms { get; set; }
    }






}