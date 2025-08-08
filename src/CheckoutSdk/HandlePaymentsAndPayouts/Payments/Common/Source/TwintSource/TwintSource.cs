namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.
    TwintSource
{
    /// <summary>
    /// twint source Class
    /// The source of the payment
    /// </summary>
    public class TwintSource : AbstractSource
    {
        /// <summary>
        /// Initializes a new instance of the TwintSource class.
        /// </summary>
        public TwintSource() : base(SourceType.Twint)
        {
        }
    }
}