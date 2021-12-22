using Checkout.Common;

namespace Checkout.Payments.Four.Request.Source
{
    public sealed class RequestIdSource : AbstractRequestSource
    {
        public RequestIdSource() : base(PaymentSourceType.Id)
        {
        }

        public string Id { get; set; }

        public string Cvv { get; set; }

    }
}