using Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseOk.Source;

namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponse201.Source.AlipayHkSource
{
    /// <summary>
    /// alipay_hk source Class
    /// The source of the payment
    /// </summary>
    public class AlipayHkSource : AbstractSource
    {

        /// <summary>
        /// Initializes a new instance of the AlipayHkSource class.
        /// </summary>
        public AlipayHkSource() : base(HkSourceType.Alipay)
        {
        }

    }
}
