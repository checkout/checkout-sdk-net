namespace Checkout.Instruments.Update
{
    public class UpdateBankInstrumentResponse : UpdateInstrumentResponse
    {
        public UpdateBankInstrumentResponse() : base(InstrumentType.BankAccount)
        {
        }

        public string Fingerprint { get; set; }
    }
}