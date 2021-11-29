namespace Checkout.Payments.Four.Sender
{
    public sealed class PaymentInstrumentSender : PaymentSender
    {
        public PaymentInstrumentSender() : base(PaymentSenderType.Instrument)
        {
        }
    }
}