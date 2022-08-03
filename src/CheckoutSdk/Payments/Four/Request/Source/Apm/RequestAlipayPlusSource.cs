using Checkout.Common;

namespace Checkout.Payments.Four.Request.Source.Apm
{
    public class RequestAlipayPlusSource : Payments.Request.Source.AbstractRequestSource
    {
        private RequestAlipayPlusSource(PaymentSourceType paymentSourceType) : base(paymentSourceType)
        {
        }

        public static RequestAlipayPlusSource RequestAlipayPlusCnSource()
        {
            return new RequestAlipayPlusSource(PaymentSourceType.AlipayCn);
        }

        public static RequestAlipayPlusSource RequestAlipayPlusGCashSource()
        {
            return new RequestAlipayPlusSource(PaymentSourceType.Gcash);
        }

        public static RequestAlipayPlusSource RequestAlipayPlusHkSource()
        {
            return new RequestAlipayPlusSource(PaymentSourceType.AlipayHk);
        }

        public static RequestAlipayPlusSource RequestAlipayPlusDanaSource()
        {
            return new RequestAlipayPlusSource(PaymentSourceType.Dana);
        }

        public static RequestAlipayPlusSource RequestAlipayPlusKakaoPaySource()
        {
            return new RequestAlipayPlusSource(PaymentSourceType.Kakaopay);
        }

        public static RequestAlipayPlusSource RequestAlipayPlusTrueMoneySource()
        {
            return new RequestAlipayPlusSource(PaymentSourceType.Truemoney);
        }

        public static RequestAlipayPlusSource RequestAlipayPlusTngSource()
        {
            return new RequestAlipayPlusSource(PaymentSourceType.Tng);
        }
    }
}