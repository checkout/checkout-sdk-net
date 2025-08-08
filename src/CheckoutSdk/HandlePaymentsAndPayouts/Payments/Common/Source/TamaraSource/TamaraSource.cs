namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.
    TamaraSource
{
    /// <summary>
    /// tamara source Class
    /// The source of the payment
    /// </summary>
    public class TamaraSource : AbstractSource
    {
        /// <summary>
        /// Initializes a new instance of the TamaraSource class.
        /// </summary>
        public TamaraSource() : base(SourceType.Tamara)
        {
        }
    }
}