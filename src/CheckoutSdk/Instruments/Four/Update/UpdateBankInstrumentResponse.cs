namespace Checkout.Instruments.Four.Update
{
    public class UpdateBankInstrumentResponse : UpdateInstrumentResponse
    {
        public UpdateBankInstrumentResponse() : base(InstrumentType.BankAccount)
        {
        }

        public string Fingerprint { get; set; }
    }
}