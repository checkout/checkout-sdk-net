using System.Collections.Generic;

namespace Checkout.Payments
{
    public class StoredCardConfiguration
    {
        /// <summary>
        /// The unique identifier for an existing customer.
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// The unique identifiers for card Instruments.
        /// </summary>
        public IList<string> InstrumentIds { get; set; }

        /// <summary>
        /// The unique identifier for the payment instrument to present to the customer as the default option.
        /// </summary>
        public string DefaultInstrumentId { get; set; }
    }
}