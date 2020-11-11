namespace Checkout.Instruments
{
    /// <summary>
    /// Represents a customer in an instrument response.
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Gets or sets the unique identifier of the customer.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the customer email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the customer full name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// True, if this instrument is set as the default for the customer.
        /// </summary>
        public bool Default { get; set; }
    }
}
