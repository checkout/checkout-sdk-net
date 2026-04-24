using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    /// <summary>Trustly was removed as a payment method on 2024/09/17.</summary>
    [System.Obsolete("Trustly was removed as a payment method on 2024/09/17.")]
    public class RequestTrustlySource : AbstractRequestSource
    {
        public RequestTrustlySource() : base(PaymentSourceType.Trustly)
        {
        }

        public Address BillingAddress { get; set; }
    }
}