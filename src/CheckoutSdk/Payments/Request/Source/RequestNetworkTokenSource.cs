using Checkout.Common;

namespace Checkout.Payments.Request.Source
{
    public class RequestNetworkTokenSource : AbstractRequestSource
    {
        public RequestNetworkTokenSource() : base(PaymentSourceType.NetworkToken)
        {
        }

        public string Token { get; set; }

        public int? ExpiryMonth { get; set; }

        public int? ExpiryYear { get; set; }

        public NetworkTokenType? TokenType { get; set; }

        public string Cryptogram { get; set; }

        public string Eci { get; set; }

        public bool? Stored { get; set; }
        
        public bool? StoreForFutureUse { get; set; }

        public string Name { get; set; }

        public string Cvv { get; set; }

        public Address BillingAddress { get; set; }

        public Phone Phone { get; set; }
        
        public AccountHolder AccountHolder { get; set; }
        
    }
}