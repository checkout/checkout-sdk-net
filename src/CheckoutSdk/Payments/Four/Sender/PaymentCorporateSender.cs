using Checkout.Common;

namespace Checkout.Payments.Four.Sender
{
    public sealed class PaymentCorporateSender : PaymentSender
    {
        public PaymentCorporateSender() : base(PaymentSenderType.Corporate)
        {
        }

        public string CompanyName { get; set; }

        public Address Address { get; set; }
              
    }
}