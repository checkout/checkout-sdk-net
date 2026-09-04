using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    /// <summary>
    /// Kakao Pay request source.
    /// </summary>
    public class RequestKakaopaySource : AbstractRequestSource
    {
        public RequestKakaopaySource() : base(PaymentSourceType.Kakaopay)
        {
        }
    }
}
