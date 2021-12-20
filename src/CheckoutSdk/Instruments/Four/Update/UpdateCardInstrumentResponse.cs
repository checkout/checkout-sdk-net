namespace Checkout.Instruments.Four.Update
{
    public class UpdateCardInstrumentResponse : UpdateInstrumentResponse
    {
        public UpdateCardInstrumentResponse() : base(InstrumentType.Card)
        {
        }

        public string Fingerprint { get; set; }
    }
}