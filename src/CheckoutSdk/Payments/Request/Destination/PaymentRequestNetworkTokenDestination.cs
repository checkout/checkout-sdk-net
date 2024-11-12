using Checkout.Payments.Request.Source;

namespace Checkout.Payments.Request.Destination
{
    public class PaymentRequestNetworkTokenDestination : PaymentRequestDestination
    {
        public PaymentRequestNetworkTokenDestination() : base(PaymentDestinationType.NetworkToken)
        {
        }

        public string Token { get; set; }

        public int? ExpiryMonth { get; set; }

        public int? ExpiryYear { get; set; }

        public NetworkTokenType TokenType { get; set; }

        public string Cryptogram { get; set; }

        public string Eci { get; set; }
        
    }
}