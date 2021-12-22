using Checkout.Common;

namespace Checkout.Payments.Request.Source
{
    public sealed class RequestCustomerSource : AbstractRequestSource
    {
        public RequestCustomerSource() : base(PaymentSourceType.Customer)
        {
        }

        public string Id { get; set; }
      
    }
}