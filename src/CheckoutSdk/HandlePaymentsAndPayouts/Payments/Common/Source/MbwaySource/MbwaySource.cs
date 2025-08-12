namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.
    MbwaySource
{
    /// <summary>
    /// mbway source Class
    /// The source of the payment
    /// </summary>
    public class MbwaySource : AbstractSource
    {
        /// <summary>
        /// Initializes a new instance of the MbwaySource class.
        /// </summary>
        public MbwaySource() : base(SourceType.Mbway)
        {
        }
    }
}