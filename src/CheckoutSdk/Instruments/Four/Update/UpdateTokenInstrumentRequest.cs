namespace Checkout.Instruments.Four.Update
{
    public sealed class UpdateTokenInstrumentRequest : UpdateInstrumentRequest
    {
        public UpdateTokenInstrumentRequest() : base(InstrumentType.Token)
        {
        }

        public string Token { get; set; }
    }
}