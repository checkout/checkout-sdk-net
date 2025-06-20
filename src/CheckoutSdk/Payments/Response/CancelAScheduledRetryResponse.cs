using Checkout.Common;

namespace Checkout.Payments.Response
{
    public class CancelAScheduledRetryResponse : Resource
    {
        public string ActionId { get; set; }
        
        public string Reference { get; set; }
    }
}