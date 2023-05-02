using Checkout.Common;

namespace Checkout.Payments.Request.Destination
{
    public abstract class PaymentRequestDestination
    {
        public PaymentDestinationType? Type { get; }

        public AccountHolder AccountHolder { get; set; }

        protected PaymentRequestDestination(PaymentDestinationType type)
        {
            Type = type;
        }
    }
}