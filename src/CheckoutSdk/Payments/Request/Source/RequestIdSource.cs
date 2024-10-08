using Checkout.Common;

namespace Checkout.Payments.Request.Source
{
    public class RequestIdSource : AbstractRequestSource
    {
        public RequestIdSource() : base(PaymentSourceType.Id)
        {
        }

        public string Id { get; set; }

        public string Cvv { get; set; }

        public string PaymentMethod { get; set; }

        public bool? Stored { get; set; }

        public bool? StoreForFutureUse { get; set; }
        
        public AccountHolder AccountHolder { get; set; }
        
    }
}