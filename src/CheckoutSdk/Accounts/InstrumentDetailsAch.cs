namespace Checkout.Accounts
{
    public class InstrumentDetailsAch : IInstrumentDetails
    {
        /// <summary>
        /// The alphanumeric value that identifies the account.
        /// [Required]
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// The 9-digit American Bankers Association (ABA) routing number that identifies the financial institution.
        /// [Required]
        /// ^[0-9]{9}$
        /// </summary>
        public string RoutingNumber { get; set; }

        /// <summary>
        /// The type of bank account.
        /// [Required]
        /// Enum: "savings" "checking"
        /// </summary>
        public InstrumentAccountType? AccountType { get; set; }
    }
}
