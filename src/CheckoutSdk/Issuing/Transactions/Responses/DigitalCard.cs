namespace Checkout.Issuing.Transactions.Responses
{
    public class DigitalCard
    {
        public string Id { get; set; }
        
        public WalletType? WalletType { get; set; }
    }
}