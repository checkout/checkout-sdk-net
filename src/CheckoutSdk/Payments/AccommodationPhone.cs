namespace Checkout.Payments
{
    /// <summary>
    /// Phone contact information for an accommodation property.
    /// </summary>
    public class AccommodationPhone
    {
        /// <summary>
        /// The phone country code.
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// The phone number.
        /// </summary>
        public string Number { get; set; }
    }
}
