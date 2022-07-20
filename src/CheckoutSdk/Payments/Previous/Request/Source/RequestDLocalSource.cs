using Checkout.Common;

namespace Checkout.Payments.Previous.Request.Source
{
    public class RequestDLocalSource : AbstractRequestSource
    {
        public RequestDLocalSource() : base(PaymentSourceType.DLocal)
        {
        }

        public string Number { get; set; }

        public int? ExpiryMonth { get; set; }

        public int? ExpiryYear { get; set; }

        public string Name { get; set; }

        public string Cvv { get; set; }

        public bool? Stored { get; set; }

        public Address BillingAddress { get; set; }

        public Phone Phone { get; set; }
              
    }
}