namespace Checkout.Instruments.Four.Update
{
    public sealed class UpdateCardInstrumentResponse : UpdateInstrumentResponse
    {
        public UpdateCardInstrumentResponse() : base(InstrumentType.Card)
        {
        }

        public string Fingerprint { get; set; }
    }
}