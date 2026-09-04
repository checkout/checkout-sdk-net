namespace Checkout.Instruments.Create
{
    /// <summary>
    /// Store ACH account instrument response.
    /// The type, id and fingerprint are inherited from CreateInstrumentResponse.
    /// </summary>
    public class CreateAchInstrumentResponse : CreateInstrumentResponse
    {
        public CreateAchInstrumentResponse() : base(InstrumentType.Ach)
        {
        }
    }
}
