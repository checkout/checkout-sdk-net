namespace Checkout.Balances
{
    /// <summary>
    /// The bank details for a single funding rail.
    /// BeneficiaryAccountName and BankName are the only fields always returned. The remaining
    /// fields vary by rail and the receiving bank's jurisdiction, and are omitted when they do
    /// not apply.
    /// </summary>
    public class TopUpFundingDetails
    {
        /// <summary>
        /// The name of the account that receives the funds.
        /// [Required]
        /// </summary>
        public string BeneficiaryAccountName { get; set; }

        /// <summary>
        /// The address of the beneficiary, if the rail requires it.
        /// [Optional]
        /// </summary>
        public string BeneficiaryAddress { get; set; }

        /// <summary>
        /// The name of the bank that receives the funds.
        /// [Required]
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// The address of the receiving bank, if the rail requires it.
        /// [Optional]
        /// </summary>
        public string BankAddress { get; set; }

        /// <summary>
        /// The account number of the receiving account.
        /// [Optional]
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// The sort code of the receiving bank. Returned for United Kingdom domestic transfers.
        /// [Optional]
        /// </summary>
        public string SortCode { get; set; }

        /// <summary>
        /// The routing number of the receiving bank. Returned for United States domestic transfers.
        /// [Optional]
        /// </summary>
        public string RoutingNumber { get; set; }

        /// <summary>
        /// The International Bank Account Number of the receiving account.
        /// [Optional]
        /// </summary>
        public string Iban { get; set; }

        /// <summary>
        /// The SWIFT or BIC code of the receiving bank. Returned for international transfers.
        /// [Optional]
        /// </summary>
        public string SwiftCode { get; set; }
    }
}
