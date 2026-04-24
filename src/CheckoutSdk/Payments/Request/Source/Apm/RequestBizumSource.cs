using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public class RequestBizumSource : AbstractRequestSource
    {
        /// <summary>Removed from the API on 2025/02/10. Use the customer object instead.</summary>
        [System.Obsolete("Removed from the API on 2025/02/10. Use the customer object instead.")]
        public string MobileNumber { get; set; }

        public RequestBizumSource() : base(PaymentSourceType.Bizum)
        {
        }
    }
}