namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.
    OctopusSource
{
    /// <summary>
    /// octopus source Class
    /// The source of the payment
    /// </summary>
    public class OctopusSource : AbstractSource
    {
        /// <summary>
        /// Initializes a new instance of the OctopusSource class.
        /// </summary>
        public OctopusSource() : base(SourceType.Octopus)
        {
        }
    }
}