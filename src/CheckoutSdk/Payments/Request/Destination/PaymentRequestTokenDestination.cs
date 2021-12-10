using Checkout.Common;

namespace Checkout.Payments.Request.Destination
{
    public sealed class PaymentRequestTokenDestination : PaymentRequestDestination
    {
        public PaymentRequestTokenDestination() : base(PaymentDestinationType.Token)
        {
        }

        public string Token { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Address BillingAddress { get; set; }

        public Phone Phone { get; set; }

    }
}