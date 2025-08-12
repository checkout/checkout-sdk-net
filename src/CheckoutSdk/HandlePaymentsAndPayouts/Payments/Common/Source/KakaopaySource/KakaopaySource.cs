namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.
    KakaopaySource
{
    /// <summary>
    /// kakaopay source Class
    /// The source of the payment
    /// </summary>
    public class KakaopaySource : AbstractSource
    {
        /// <summary>
        /// Initializes a new instance of the KakaopaySource class.
        /// </summary>
        public KakaopaySource() : base(SourceType.Kakaopay)
        {
        }
    }
}