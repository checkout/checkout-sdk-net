namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.
    EpsSource
{
    /// <summary>
    /// eps source Class
    /// The source of the payment
    /// </summary>
    public class EpsSource : AbstractSource
    {
        /// <summary>
        /// Initializes a new instance of the EpsSource class.
        /// </summary>
        public EpsSource() : base(SourceType.Eps)
        {
        }

        /// <summary>
        /// Purpose of the payment as appearing on customer's bank statement.
        /// [Optional]
        /// <= 27
        /// </summary>
        public string Purpose { get; set; }

        /// <summary>
        /// Bank Identifier Code (BIC). It can be exactly 8 characters or 11 characters long.
        /// [Optional]
        /// <= 11
        /// 8 characters
        /// 11 characters
        /// </summary>
        public string Bic { get; set; }

        /// <summary>
        /// International Bank Account Number (IBAN) without whitespaces.
        /// [Optional]
        /// <= 34
        /// </summary>
        public string Iban { get; set; }

        /// <summary>
        /// Account holder information.
        /// [Optional]
        /// </summary>
        public string AccountHolderName { get; set; }

        /// <summary>
        /// The account holder details
        /// [Optional]
        /// </summary>
        public AccountHolder.AccountHolder AccountHolder { get; set; }
    }
}