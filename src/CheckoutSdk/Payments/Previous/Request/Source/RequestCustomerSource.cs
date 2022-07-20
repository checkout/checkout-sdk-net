using Checkout.Common;

namespace Checkout.Payments.Previous.Request.Source
{
    public class RequestCustomerSource : AbstractRequestSource
    {
        public RequestCustomerSource() : base(PaymentSourceType.Customer)
        {
        }

        public string Id { get; set; }
      
    }
}