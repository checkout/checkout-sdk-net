using Checkout.Common;

namespace Checkout.Payments
{
    public class PaymentActionSummary
    {
        public string Id { get; set; }

        public ActionType? Type { get; set; }

        public string ResponseCode { get; set; }

        public string ResponseSummary { get; set; }
    }
}