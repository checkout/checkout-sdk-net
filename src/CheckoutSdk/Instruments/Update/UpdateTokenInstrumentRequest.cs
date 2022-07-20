namespace Checkout.Instruments.Update
{
    public class UpdateTokenInstrumentRequest : UpdateInstrumentRequest
    {
        public UpdateTokenInstrumentRequest() : base(InstrumentType.Token)
        {
        }

        public string Token { get; set; }
    }
}