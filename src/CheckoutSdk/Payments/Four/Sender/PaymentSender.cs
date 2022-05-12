namespace Checkout.Payments.Four.Sender
{
    public class PaymentSender : ISender
    {
        public PaymentSenderType? Type { get; set; }

        public string Reference { get; set; }

        public PaymentSender(PaymentSenderType type)
        {
            Type = type;
        }

        PaymentSenderType? ISender.Type()
        {
            return Type;
        }
    }
}