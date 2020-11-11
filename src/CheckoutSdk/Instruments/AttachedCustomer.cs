namespace Checkout.Instruments
{
    /// <summary>
    /// To which customer to attach the payment instrument to.
    /// </summary>
    public class AttachedCustomer
    {
        /// <summary>
        /// Gets or sets the unique identifier of the customer attached to the instrument.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// If true, sets this instrument as the default for the customer (if false, no changes are actioned).
        /// </summary>
        public bool Default { get; set; }
    }
}
