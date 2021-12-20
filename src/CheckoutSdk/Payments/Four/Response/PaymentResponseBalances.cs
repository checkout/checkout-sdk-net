namespace Checkout.Payments.Four.Response
{
    public class PaymentResponseBalances
    {
        public long? TotalAuthorized { get; set; }

        public long? TotalVoided { get; set; }

        public long? AvailableToVoid { get; set; }

        public long? TotalCaptured { get; set; }

        public long? AvailableToCapture { get; set; }

        public long? TotalRefunded { get; set; }

        public long? AvailableToRefund { get; set; }
    }
}