namespace Checkout.Accounts
{
    public class InstrumentDetailsSepa : IInstrumentDetails
    {
        public string Iban { get; set; }

        public string SwiftBic { get; set; }
    }
}