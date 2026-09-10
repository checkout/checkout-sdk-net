namespace Checkout.Balances
{
    /// <summary>
    /// The bank details for each available funding rail.
    /// Both Domestic and International are optional, and their availability depends on the
    /// sub-account's holding currency, jurisdiction, and banking partner. Do not assume that
    /// both rails are always available.
    /// </summary>
    public class TopUpBankDetails
    {
        /// <summary>
        /// The bank details for the domestic funding rail.
        /// [Optional]
        /// </summary>
        public TopUpFundingDetails Domestic { get; set; }

        /// <summary>
        /// The bank details for the international funding rail.
        /// [Optional]
        /// </summary>
        public TopUpFundingDetails International { get; set; }
    }
}
