using Checkout.Common;

namespace Checkout.Payments.Four.Request.Source
{
    public sealed class RequestCardSource : AbstractRequestSource
    {
        public RequestCardSource() : base(PaymentSourceType.Card)
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