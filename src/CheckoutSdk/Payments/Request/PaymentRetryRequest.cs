namespace Checkout.Payments.Request
{
    public class PaymentRetryRequest
    {
        public bool? Enabled { get; set; }

        public int? MaxAttempts { get; set; }

        public int? EndAfterDays { get; set; }
    }
}