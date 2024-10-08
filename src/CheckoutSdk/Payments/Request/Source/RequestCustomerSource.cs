using Checkout.Common;

namespace Checkout.Payments.Request.Source
{
    public class RequestCustomerSource : AbstractRequestSource
    {
        public RequestCustomerSource() : base(PaymentSourceType.Customer)
        {
        }

        public string Id { get; set; }
        
        public AccountHolder AccountHolder { get; set; }
      
    }
}