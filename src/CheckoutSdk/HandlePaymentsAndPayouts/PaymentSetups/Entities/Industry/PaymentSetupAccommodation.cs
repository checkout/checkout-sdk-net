using System.Collections.Generic;
using Checkout.Common;

namespace Checkout.Payments.Setups.Entities
{
    public class PaymentSetupAccommodation
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
        public string CheckInDate { get; set; }

        /// <summary>
        /// The check-out date in YYYY-MM-DD format
        /// </summary>
        public string CheckOutDate { get; set; }

        /// <summary>
        /// The address of the accommodation
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// The number of rooms booked
        /// </summary>
        public int? NumberOfRooms { get; set; }

        /// <summary>
        /// List of guests staying at the accommodation
        /// </summary>
        public IList<AccommodationGuest> Guests { get; set; }

        /// <summary>
        /// Details of the rooms booked
        /// </summary>
        public IList<AccommodationRoom> Room { get; set; }
    }

    public class AccommodationGuest
    {
        /// <summary>
        /// The guest's first name
        /// </summary>
        public string FirstName { get; set; }
       
        /// <summary>
        /// The guest's last name
        /// </summary>
        public string LastName { get; set; }
        
        /// <summary>
        /// The guest's date of birth in YYYY-MM-DD format
        /// </summary>
        public string DateOfBirth { get; set; }
    }

    public class AccommodationRoom
    {
        /// <summary>
        /// The nightly rate for the room
        /// </summary>
        public double? Rate { get; set; }

        /// <summary>
        /// The number of nights the room is booked for
        /// </summary>
        public int? NumberOfNights { get; set; }
    }
}