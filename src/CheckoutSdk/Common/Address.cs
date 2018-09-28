namespace Checkout.Common
{
    /// <summary>
    /// Defines a postal address.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Gets or sets Line 1 of the address.
        /// </summary>
        public string AddressLine1 { get; set; }
        
        /// <summary>
        /// Gets or sets Line 2 of the address.
        /// </summary>
        public string AddressLine2 { get; set; }
        
        /// <summary>
        /// Gets or sets the address city.
        /// </summary>
        public string City { get; set; }
        
        /// <summary>
        /// Gets or sets the address state.
        /// </summary>
        public string State { get; set; }
        
        /// <summary>
        /// Gets or sets the address zip or postal code.
        /// </summary>
        public string Zip { get; set; }
        
        /// <summary>
        /// Gets or sets the address country.
        /// </summary>
        public string Country { get; set; }
    }
}