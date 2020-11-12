using Checkout.Common;

namespace Checkout.Instruments
{
    /// <summary>
    /// The instrument details for the update.
    /// </summary>
    public class UpdateInstrumentRequest
    {
        /// <summary>
        /// Gets or sets the expiry month.
        /// </summary>
        public int ExpiryMonth { get; set; }

        /// <summary>
        /// Gets or sets the expiry year.
        /// </summary>
        public int ExpiryYear { get; set; }

        /// <summary>
        /// Gets or sets the name of the cardholder.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the account holder details.
        /// </summary>
        public AccountHolder AccountHolder { get; set; }

        /// <summary>
        /// Gets or sets the customer attached to the payment instrument.
        /// </summary>
        public AttachedCustomer Customer { get; set; }
    }
}
