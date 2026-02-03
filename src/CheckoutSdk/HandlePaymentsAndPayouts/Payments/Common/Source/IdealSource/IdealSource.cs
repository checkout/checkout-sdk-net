namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.
    IdealSource
{
    /// <summary>
    /// ideal source Class
    /// The source of the payment
    /// </summary>
    public class IdealSource : AbstractSource
    {
        /// <summary>
        /// Initializes a new instance of the IdealSource class.
        /// </summary>
        public IdealSource() : base(SourceType.Ideal)
        {
        }

        /// <summary>
        /// description
        /// [Required]
        /// &lt;= 27
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// BIC (8 or 11-digits) BIC of the bank where the Consumer account is held.  If governing law prevents Issuers
        /// outside the Netherlands from disclosing this information, field may be omitted.
        /// [Required]
        /// &lt;= 11
        /// </summary>
        public string Bic { get; set; }

        /// <summary>
        /// The IBAN of the Consumer Bank account used for payment.  If governing law prevents Issuers outside the
        /// Netherlands  from disclosing this information, field may be omitted.
        /// [Optional]
        /// &lt;= 34
        /// </summary>
        public string Iban { get; set; }

        /// <summary>
        /// Name of the Consumer according to the name of the account used for payment.  In the exceptional case that
        /// the consumerName cannot be retrieved by the Issuer,  this is filled with 'N/A'.  If governing law prevents
        /// Issuers outside the Netherlands from disclosing this information, field may be omitted.
        /// [Optional]
        /// </summary>
        public string AccountHolder { get; set; }
    }
}