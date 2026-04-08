using System.Collections.Generic;
using Checkout.Common;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// Contains information about the accommodation booked by the customer.
    /// </summary>
    public class AccommodationData
    {
        /// <summary>
        /// For lodging, the lodging name that appears on the storefront/customer receipts.
        /// For cruise, the ship name booked for the cruise.
        /// [Optional]
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A unique identifier for the booking.
        /// [Optional]
        /// </summary>
        public string BookingReference { get; set; }

        /// <summary>
        /// For lodging bookings, the customer's check-in date.
        /// For cruise bookings, the cruise departure date (yyyy-MM-dd).
        /// [Optional]
        /// Format: date
        /// </summary>
        public string CheckInDate { get; set; }

        /// <summary>
        /// For lodging bookings, the customer's check-out date.
        /// For cruise bookings, the cruise return date (yyyy-MM-dd).
        /// [Optional]
        /// Format: date
        /// </summary>
        public string CheckOutDate { get; set; }

        /// <summary>
        /// The accommodation's address.
        /// [Optional]
        /// </summary>
        public AccommodationAddress Address { get; set; }

        /// <summary>
        /// The total number of rooms booked for the accommodation.
        /// [Optional]
        /// </summary>
        public int? NumberOfRooms { get; set; }

        /// <summary>
        /// Details about the guests staying at the accommodation.
        /// [Optional]
        /// </summary>
        public IList<AccommodationGuest> Guests { get; set; }

        /// <summary>
        /// Details about the rooms booked by the customer.
        /// [Optional]
        /// </summary>
        public IList<AccommodationRoom> Room { get; set; }
    }

    public class AccommodationAddress
    {
        /// <summary>
        /// [Optional]
        /// </summary>
        public string AddressLine1 { get; set; }

        /// <summary>
        /// [Optional]
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// [Optional]
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// [Optional]
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// [Optional]
        /// </summary>
        public string Zip { get; set; }
    }

    public class AccommodationGuest
    {
        /// <summary>
        /// [Optional]
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// [Optional]
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The guest's date of birth (yyyy-MM-dd).
        /// [Optional]
        /// Format: date
        /// </summary>
        public string DateOfBirth { get; set; }
    }

    public class AccommodationRoom
    {
        /// <summary>
        /// The rate or cost of the room per day.
        /// [Optional]
        /// </summary>
        public decimal? Rate { get; set; }

        /// <summary>
        /// The number of nights the room is booked for.
        /// [Optional]
        /// </summary>
        public int? NumberOfNights { get; set; }
    }
}
