namespace Checkout.Balances
{
    /// <summary>
    /// The balance values held by a currency account (sub-account).
    /// </summary>
    public class Balances
    {
        /// <summary>
        /// The total incoming funds that will be added to the Available balance once cleared.
        /// [Optional]
        /// </summary>
        public long? Pending { get; set; }

        /// <summary>
        /// The funds that are available for processing.
        /// [Optional]
        /// </summary>
        public long? Available { get; set; }

        /// <summary>
        /// The funds reserved from the Available balance for outgoing transactions that are yet to clear.
        /// [Optional]
        /// </summary>
        public long? Payable { get; set; }

        /// <summary>
        /// The funds held by Checkout.com to cover potential liabilities and risk events associated with your account.
        /// [Optional]
        /// </summary>
        public long? Collateral { get; set; }

        /// <summary>
        /// The funds held for processing Payouts and Issuing payments when the Available balance
        /// is insufficient.
        /// [Optional]
        /// </summary>
        public long? Operational { get; set; }

        /// <summary>
        /// A breakdown of the funds held in the collateral balance.
        /// [Optional]
        /// </summary>
        public CollateralBreakdown CollateralBreakdown { get; set; }
    }
}
