namespace Checkout.Payments.Four.Request.Source
{
    public sealed class PayoutRequestCurrencyAccountSource : PayoutRequestSource
    {
        public PayoutRequestCurrencyAccountSource() : base(PayoutSourceType.CurrencyAccount)
        {
        }

        public string Id { get; set; }
    }
}