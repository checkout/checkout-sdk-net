using Checkout.Common;

namespace Checkout.Instruments.Update
{
    /// <summary>
    /// Updates the details of a stored card instrument.
    /// </summary>
    public class UpdateCardInstrumentRequest : UpdateInstrumentRequest
    {
        public UpdateCardInstrumentRequest() : base(InstrumentType.Card)
        {
        }

        /// <summary>
        /// The expiry month of the card.
        /// [Optional]
        /// Minimum value 1. min 1 characters, max 2 characters
        /// </summary>
        public int? ExpiryMonth { get; set; }

        /// <summary>
        /// The expiry year of the card.
        /// [Optional]
        /// min 4 characters, max 4 characters
        /// </summary>
        public int? ExpiryYear { get; set; }

        /// <summary>
        /// Name of the cardholder.
        /// [Optional]
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The customer details.
        /// [Optional]
        /// </summary>
        public UpdateCustomerRequest Customer { get; set; }

        /// <summary>
        /// The account holder details.
        /// [Optional]
        /// </summary>
        public AccountHolder AccountHolder { get; set; }
    }
}
