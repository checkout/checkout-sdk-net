using Checkout.Common;
using Checkout.Payments.Contexts;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Checkout.Payments
{
    /// <summary>
    /// Contains information about the accommodation booked by the customer.
    /// </summary>
    public class AccommodationData
    {
        /// <summary>
        /// For lodging, contains the lodging name that appears on the storefront/customer
        /// receipts. For cruise, contains the ship name booked for the cruise.
        /// [Optional]
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A unique identifier for the booking.
        /// [Optional]
        /// </summary>
        public string BookingReference { get; set; }

        /// <summary>
        /// For lodging, contains the actual or scheduled date the guest checked-in. For cruise,
        /// contains the cruise departure date, also known as the sail date.
        /// [Optional]
        /// Format: yyyy-MM-dd
        /// </summary>
        [JsonConverter(typeof(ShortDateTimeConverter))]
        public DateTime? CheckInDate { get; set; }

        /// <summary>
        /// For lodging, contains the actual or scheduled date the guest checked-out. For cruise,
        /// contains the cruise return date, also known as the sail end date.
        /// [Optional]
        /// Format: yyyy-MM-dd
        /// </summary>
        [JsonConverter(typeof(ShortDateTimeConverter))]
        public DateTime? CheckOutDate { get; set; }

        /// <summary>
        /// The address details of the accommodation.
        /// [Optional]
        /// </summary>
        /// <remarks>
        /// The specification defines only <c>address_line1</c> and <c>zip</c> on this object.
        /// The wider <see cref="Address"/> type is reused for consistency with the rest of the
        /// SDK; the remaining members are not read by the API on this property.
        /// </remarks>
        public Address Address { get; set; }

        /// <summary>
        /// The state or province of the address country
        /// (ISO 3166-2 code of up to two alphanumeric characters).
        /// [Optional]
        /// </summary>
        /// <remarks>
        /// A free-form string, not a country code. The specification's own example is
        /// <c>"FL"</c>, which is a US state rather than a country.
        /// </remarks>
        public string State { get; set; }

        /// <summary>
        /// The ISO country code of the address.
        /// [Optional]
        /// </summary>
        /// <remarks>
        /// A free-form string rather than an ISO 3166-1 alpha-2 enum: the specification's example
        /// is the three-letter code <c>"USA"</c>, which no alpha-2 enum can represent.
        /// </remarks>
        public string Country { get; set; }

        /// <summary>
        /// The address city.
        /// [Optional]
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The total number of rooms booked for the accommodation.
        /// [Optional]
        /// </summary>
        public int? NumberOfRooms { get; set; }

        /// <summary>
        /// Contains information about the guests staying at the accommodation.
        /// [Optional]
        /// </summary>
        public List<PaymentContextsGuests> Guests { get; set; }

        /// <summary>
        /// Contains information about the rooms booked by the customer.
        /// [Optional]
        /// </summary>
        public List<PaymentContextsAccommodationRoom> Room { get; set; }

        /// <summary>
        /// The property's phone information.
        /// [Optional]
        /// </summary>
        public List<AccommodationPhone> PropertyPhone { get; set; }

        /// <summary>
        /// The customer service phone information.
        /// [Optional]
        /// </summary>
        public List<AccommodationPhone> CustomerServicePhone { get; set; }
    }
}