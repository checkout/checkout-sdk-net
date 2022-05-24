using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public class RequestQPaySource : AbstractRequestSource
    {
        public int? Quantity { get; set; }
        
        public string Description { get; set; }
        
        public string Language { get; set; }
        
        public string NationalId { get; set; }
        
        public RequestQPaySource() : base(PaymentSourceType.QPay)
        {
        }
    }
}