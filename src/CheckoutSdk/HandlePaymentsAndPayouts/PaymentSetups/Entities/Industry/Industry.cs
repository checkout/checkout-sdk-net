namespace Checkout.Payments.Setups.Entities
{
    public class Industry
    {
        /// <summary>
        /// Details about the airline ticket and flights the customer booked.
        /// [Optional]
        /// </summary>
        public AirlineData AirlineData { get; set; }

        /// <summary>
        /// Contains information about the accommodation booked by the customer.
        /// [Optional]
        /// </summary>
        public AccommodationData AccommodationData { get; set; }
    }
}