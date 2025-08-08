namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.SequraSource
{
    /// <summary>
    /// sequra source Class
    /// The source of the payment
    /// </summary>
    public class SequraSource : AbstractSource
    {

        /// <summary>
        /// Initializes a new instance of the SequraSource class.
        /// </summary>
        public SequraSource() : base(SourceType.Sequra)
        {
        }

    }
}
