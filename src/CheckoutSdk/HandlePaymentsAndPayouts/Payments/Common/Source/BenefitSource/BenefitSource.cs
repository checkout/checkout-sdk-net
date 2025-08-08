namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.
    BenefitSource
{
    /// <summary>
    /// benefit source Class
    /// The source of the payment
    /// </summary>
    public class BenefitSource : AbstractSource
    {
        /// <summary>
        /// Initializes a new instance of the BenefitSource class.
        /// </summary>
        public BenefitSource() : base(SourceType.Benefit)
        {
        }
    }
}