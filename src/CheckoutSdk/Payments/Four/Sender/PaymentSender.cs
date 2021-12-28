using Checkout.Common.Four;

namespace Checkout.Payments.Four.Sender
{
    public class PaymentSender
    {
        public PaymentSenderType? Type { get; set; }

        public PaymentSender(PaymentSenderType type)
        {
            Type = type;
        }
    }
}