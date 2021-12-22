using Checkout.Common;

namespace Checkout.Payments.Four.Sender
{
    public sealed class PaymentIndividualSender : PaymentSender
    {
        public PaymentIndividualSender() : base(PaymentSenderType.Individual)
        {
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Address Address { get; set; }
              
    }
}