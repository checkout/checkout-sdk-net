namespace Checkout.Instruments
{
    /// <summary>
    /// Stored customer details.
    /// This deliberately does not derive from Checkout.Common.CustomerResponse, which adds a phone
    /// number that the instrument customer response does not return.
    /// </summary>
    public class InstrumentCustomerResponse
    {
        /// <summary>
        /// The customer's unique identifier. This can be passed as a source when making a payment.
        /// [Required]
        /// Pattern: ^(cus)_(\w{26})$
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The customer's email address.
        /// [Optional]
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The customer's name.
        /// [Optional]
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// This will be true if this instrument is set as the default for the customer.
        /// [Optional]
        /// </summary>
        public bool Default { get; set; }
    }
}
