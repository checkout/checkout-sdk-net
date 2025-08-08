namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.
    IllicadoSource
{
    /// <summary>
    /// illicado source Class
    /// The source of the payment
    /// </summary>
    public class IllicadoSource : AbstractSource
    {
        /// <summary>
        /// Initializes a new instance of the IllicadoSource class.
        /// </summary>
        public IllicadoSource() : base(SourceType.Illicado)
        {
        }
    }
}