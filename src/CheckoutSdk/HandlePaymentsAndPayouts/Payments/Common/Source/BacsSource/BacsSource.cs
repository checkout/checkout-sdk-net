namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.
    BacsSource
{
    /// <summary>
    /// bacs source Class
    /// The source of the payment. PaymentResponseSource maps bacs to
    /// PaymentDeclinedSourceResponse, which declares an id and a type only.
    /// </summary>
    public class BacsSource : AbstractSource
    {
        /// <summary>
        /// Initializes a new instance of the BacsSource class.
        /// </summary>
        public BacsSource() : base(SourceType.Bacs)
        {
        }

        /// <summary>
        /// The payment instrument identifier.
        /// [Required]
        /// Pattern: ^(src)_(\w{26})$
        /// </summary>
        public string Id { get; set; }
    }
}
