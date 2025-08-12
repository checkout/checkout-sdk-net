namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.
    MobilepaySource
{
    /// <summary>
    /// mobilepay source Class
    /// The source of the payment
    /// </summary>
    public class MobilepaySource : AbstractSource
    {
        /// <summary>
        /// Initializes a new instance of the MobilepaySource class.
        /// </summary>
        public MobilepaySource() : base(SourceType.Mobilepay)
        {
        }
    }
}