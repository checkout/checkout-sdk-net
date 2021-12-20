using Checkout.Common;

namespace Checkout.Payments.Request.Destination
{
    public class PaymentRequestCardDestination : PaymentRequestDestination
    {
        public PaymentRequestCardDestination() : base(PaymentDestinationType.Card)
        {
        }

        public string Number { get; set; }

        public int? ExpiryMonth { get; set; }

        public int? ExpiryYear { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Name { get; set; }

        public Address BillingAddress { get; set; }

        public Phone Phone { get; set; }
              
    }
}