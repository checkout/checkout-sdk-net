using Checkout.Common;

namespace Checkout.Instruments.Create
{
    /// <summary>
    /// Stores a payment instrument from a Checkout.com token.
    /// </summary>
    public class CreateTokenInstrumentRequest : CreateInstrumentRequest
    {
        public CreateTokenInstrumentRequest() : base(InstrumentType.Token)
        {
        }

        /// <summary>
        /// The Checkout.com token.
        /// [Required]
        /// Pattern: ^(tok)_(\w{26})$|^(card_tok)_(\w{12})$
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// The account holder details.
        /// [Optional]
        /// </summary>
        public AccountHolder AccountHolder { get; set; }

        /// <summary>
        /// The customer details. Associates the instrument with an existing or new customer.
        /// [Optional]
        /// </summary>
        public CreateCustomerInstrumentRequest Customer { get; set; }
    }
}
