namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.
    BankAccountSource
{
    /// <summary>
    /// bank_account source Class
    /// The source of the payment. PaymentResponseSource maps bank_account to
    /// PaymentDeclinedSourceResponse, which declares an id and a type only.
    /// </summary>
    public class BankAccountSource : AbstractSource
    {
        /// <summary>
        /// Initializes a new instance of the BankAccountSource class.
        /// </summary>
        public BankAccountSource() : base(SourceType.BankAccount)
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
