namespace Checkout.Payments.Request.Source
{
    public abstract class PayoutRequestSource
    {
        public PayoutSourceType? Type { get; set; }

        public long? Amount { get; set; }

        protected PayoutRequestSource(PayoutSourceType type)
        {
            Type = type;
        }
    }
}