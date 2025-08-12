namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.CvconnectSource
{
    /// <summary>
    /// cvconnect source Class
    /// The source of the payment
    /// </summary>
    public class CvconnectSource : AbstractSource
    {

        /// <summary>
        /// Initializes a new instance of the CvconnectSource class.
        /// </summary>
        public CvconnectSource() : base(SourceType.Cvconnect)
        {
        }

    }
}
