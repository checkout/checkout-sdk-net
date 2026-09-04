namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.
    AchSource
{
    /// <summary>
    /// ach source Class
    /// The source of the payment. PaymentResponseSource maps ach to
    /// PaymentDeclinedSourceResponse, which declares an id and a type only.
    /// </summary>
    public class AchSource : AbstractSource
    {
        /// <summary>
        /// Initializes a new instance of the AchSource class.
        /// </summary>
        public AchSource() : base(SourceType.Ach)
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
