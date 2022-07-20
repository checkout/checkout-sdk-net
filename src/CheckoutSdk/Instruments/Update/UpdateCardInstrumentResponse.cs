namespace Checkout.Instruments.Update
{
    public class UpdateCardInstrumentResponse : UpdateInstrumentResponse
    {
        public UpdateCardInstrumentResponse() : base(InstrumentType.Card)
        {
        }

        public string Fingerprint { get; set; }
    }
}