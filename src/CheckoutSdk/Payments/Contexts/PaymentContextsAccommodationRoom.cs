namespace Checkout.Payments.Contexts
{
    public class PaymentContextsAccommodationRoom
    {
        /// <summary>
        /// The nightly rate for the room
        /// </summary>
        public string Rate { get; set; }

        /// <summary>
        /// The number of nights the room is booked for
        /// </summary>
        public int? NumberOfNightsAtRoomRate { get; set; }
    }
}