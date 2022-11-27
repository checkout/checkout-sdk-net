namespace Checkout.Accounts
{
    public class InstrumentDetailsFasterPayments : IInstrumentDetails
    {
        public string AccountNumber { get; set; }

        public string BankCode { get; set; }
    }
}