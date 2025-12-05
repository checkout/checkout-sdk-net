
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
        public AccommodationAddress Address { get; set; }

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
        public IList<Guest> Guests { get; set; }

        /// <summary>
        /// Contains information about the rooms booked by the customer.
        /// </summary>
        public IList<Room> Rooms { get; set; }
    }

    public class AccommodationAddress
    {
        /// <summary>
        /// The first line of the address.
        /// </summary>
        public string AddressLine1 { get; set; }

        /// <summary>
        /// The postal code for the address.
        /// </summary>
        public string Zip { get; set; }
    }

    public class Guest
    {
        /// <summary>
        /// The first name of the guest.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the guest.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The date of birth of the guest.
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
    }

    public class Room
    {
        /// <summary>
        /// For lodging, contains the nightly rate for one room. For cruise, contains the total cost of the cruise.
        /// </summary>
        public string Rate { get; set; }

        /// <summary>
        /// For lodging, contains the number of nights charged at the rate provided in the rate field. 
        /// For cruise, contains the length of the cruise in days.
        /// </summary>
        public string NumberOfNightsAtRoomRate { get; set; }
    }
}