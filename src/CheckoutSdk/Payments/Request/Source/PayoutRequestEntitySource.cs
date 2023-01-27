namespace Checkout.Payments.Request.Source
{
    public class PayoutRequestEntitySource : PayoutRequestSource
    {
        public PayoutRequestEntitySource() : base(PayoutSourceType.Entity)
        {
        }

        public string Id { get; set; }
    }
}