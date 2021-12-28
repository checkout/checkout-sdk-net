using Checkout.Common.Four;

namespace Checkout.Payments.Four.Sender
{
    public class PaymentInstrumentSender : PaymentSender
    {
        public PaymentInstrumentSender() : base(PaymentSenderType.Instrument)
        {
        }
    }
}