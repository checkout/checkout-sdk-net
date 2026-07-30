namespace Checkout.Identities.Entities
{
    /// <summary>
    /// The address extracted from the document.
    /// </summary>
    public class AdvAddress
    {
        /// <summary>
        /// The first line of the address. (max 250 characters)
        /// </summary>
        public string AddressLine1 { get; set; }

        /// <summary>
        /// The second line of the address. (max 250 characters)
        /// </summary>
        public string AddressLine2 { get; set; }

        /// <summary>
        /// The city or town. (max 50 characters)
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The state, county, or province. (max 50 characters)
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// The postal or ZIP code. (max 50 characters)
        /// </summary>
        public string Zip { get; set; }

        /// <summary>
        /// The two-letter ISO country code of the address. (max 2 characters)
        /// </summary>
        public string Country { get; set; }
    }
}
