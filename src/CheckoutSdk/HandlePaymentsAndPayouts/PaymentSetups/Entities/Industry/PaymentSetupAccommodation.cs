using System.Collections.Generic;
using Checkout.Common;

namespace Checkout.Payments.Setups.Entities
{
    public class PaymentSetupAccommodation
    {
        public string Name { get; set; }

        public string BookingReference { get; set; }

        public string CheckInDate { get; set; }

        public string CheckOutDate { get; set; }

        public Address Address { get; set; }

        public int? NumberOfRooms { get; set; }

        public IList<AccommodationGuest> Guests { get; set; }

        public IList<AccommodationRoom> Room { get; set; }
    }

    public class AccommodationGuest
    {
        public string FirstName { get; set; }
       
        public string LastName { get; set; }
        
        public string DateOfBirth { get; set; }
    }

    public class AccommodationRoom
    {
        public double? Rate { get; set; }

        public int? NumberOfNights { get; set; }
    }
}