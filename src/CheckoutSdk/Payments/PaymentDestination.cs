namespace Checkout.Sdk.Payments
{
    public class PaymentDestination
    {
        public PaymentDestination(string id, int amount)
        {
            Id = id;
            Amount = amount;
        }

        public string Id { get; }
        public int Amount { get; }
    }
}