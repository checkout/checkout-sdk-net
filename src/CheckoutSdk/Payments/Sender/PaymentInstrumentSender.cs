namespace Checkout.Payments.Sender
{
    public class PaymentInstrumentSender : PaymentSender
    {
        public PaymentInstrumentSender() : base(PaymentSenderType.Instrument)
        {
        }
    }
}