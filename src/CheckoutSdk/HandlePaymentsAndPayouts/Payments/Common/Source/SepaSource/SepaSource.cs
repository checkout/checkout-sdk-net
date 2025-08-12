namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.
    SepaSource
{
    /// <summary>
    /// sepa source Class
    /// The source of the payment
    /// </summary>
    public class SepaSource : AbstractSource
    {
        /// <summary>
        /// Initializes a new instance of the SepaSource class.
        /// </summary>
        public SepaSource() : base(SourceType.Sepa)
        {
        }

        /// <summary>
        /// The instrument ID
        /// [Required]
        /// </summary>
        public string Id { get; set; }
    }
}