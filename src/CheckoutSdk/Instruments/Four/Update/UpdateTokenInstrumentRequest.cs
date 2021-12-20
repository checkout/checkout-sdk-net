namespace Checkout.Instruments.Four.Update
{
    public class UpdateTokenInstrumentRequest : UpdateInstrumentRequest
    {
        public UpdateTokenInstrumentRequest() : base(InstrumentType.Token)
        {
        }

        public string Token { get; set; }
    }
}