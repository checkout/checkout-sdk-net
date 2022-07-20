using Checkout.Common;

namespace Checkout.Payments.Sender
{
    public class PaymentIndividualSender : PaymentSender
    {
        public PaymentIndividualSender() : base(PaymentSenderType.Individual)
        {
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Address Address { get; set; }

        public AccountHolderIdentification AccountHolderIdentification { get; set; }
    }
}