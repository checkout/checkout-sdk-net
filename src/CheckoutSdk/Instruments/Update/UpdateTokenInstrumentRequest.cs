namespace Checkout.Instruments.Update
{
    /// <summary>
    /// Updates a stored instrument from a Checkout.com token.
    /// Not declared in the API specification: the UpdateInstrumentRequest discriminator maps card,
    /// bank_account, sepa, ach and bacs only. Retained for backwards compatibility.
    /// </summary>
    public class UpdateTokenInstrumentRequest : UpdateInstrumentRequest
    {
        public UpdateTokenInstrumentRequest() : base(InstrumentType.Token)
        {
        }

        /// <summary>
        /// The Checkout.com token.
        /// Not declared in the API specification.
        /// </summary>
        public string Token { get; set; }
    }
}
