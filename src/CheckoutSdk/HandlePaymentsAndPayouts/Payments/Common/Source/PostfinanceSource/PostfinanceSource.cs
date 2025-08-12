namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.
    PostfinanceSource
{
    /// <summary>
    /// postfinance source Class
    /// The source of the payment
    /// </summary>
    public class PostfinanceSource : AbstractSource
    {
        /// <summary>
        /// Initializes a new instance of the PostfinanceSource class.
        /// </summary>
        public PostfinanceSource() : base(SourceType.Postfinance)
        {
        }
    }
}