namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.
    DanaSource
{
    /// <summary>
    /// dana source Class
    /// The source of the payment
    /// </summary>
    public class DanaSource : AbstractSource
    {
        /// <summary>
        /// Initializes a new instance of the DanaSource class.
        /// </summary>
        public DanaSource() : base(SourceType.Dana)
        {
        }
    }
}