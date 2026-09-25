namespace Checkout.Payments.Contexts
{
    /// <summary>
    /// Contains information about a room booked by the customer.
    /// </summary>
    public class PaymentContextsAccommodationRoom
    {
        /// <summary>
        /// For lodging, contains the nightly rate for one room. For cruise, contains the total
        /// cost of the cruise.
        /// [Optional]
        /// </summary>
        public string Rate { get; set; }

        /// <summary>
        /// For lodging, contains the number of nights charged at the rate provided in the rate
        /// field. For cruise, contains the length of the cruise in days.
        /// [Optional]
        /// </summary>
        /// <remarks>
        /// The specification declares this as a string, not an integer. Its example is
        /// <c>"3"</c>.
        /// </remarks>
        public string NumberOfNightsAtRoomRate { get; set; }
    }
}
