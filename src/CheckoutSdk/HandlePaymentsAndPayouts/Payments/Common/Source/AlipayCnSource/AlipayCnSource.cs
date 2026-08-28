namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.
    AlipayCnSource
{
    /// <summary>
    /// alipay_cn source Class
    /// The source of the payment. PaymentResponseSource maps alipay_cn to
    /// PaymentDeclinedSourceResponse, which declares an id and a type only.
    /// </summary>
    public class AlipayCnSource : AbstractSource
    {
        /// <summary>
        /// Initializes a new instance of the AlipayCnSource class.
        /// </summary>
        public AlipayCnSource() : base(SourceType.AlipayCn)
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
