namespace Checkout.Instruments.Create
{
    /// <summary>
    /// Store SEPA account instrument response.
    /// The type, id and fingerprint are inherited from CreateInstrumentResponse.
    /// </summary>
    public class CreateSepaInstrumentResponse : CreateInstrumentResponse
    {
        public CreateSepaInstrumentResponse() : base(InstrumentType.Sepa)
        {
        }
    }
}
