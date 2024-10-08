using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public class RequestOctopusSource : AbstractRequestSource
    {
        public RequestOctopusSource() : base(PaymentSourceType.Octopus)
        {
        }
    }
}