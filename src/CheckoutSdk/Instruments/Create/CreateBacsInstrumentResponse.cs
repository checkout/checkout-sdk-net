namespace Checkout.Instruments.Create
{
    /// <summary>
    /// Store Bacs Direct Debit account instrument response.
    /// The type, id and fingerprint are inherited from CreateInstrumentResponse.
    /// </summary>
    public class CreateBacsInstrumentResponse : CreateInstrumentResponse
    {
        public CreateBacsInstrumentResponse() : base(InstrumentType.Bacs)
        {
        }
    }
}
