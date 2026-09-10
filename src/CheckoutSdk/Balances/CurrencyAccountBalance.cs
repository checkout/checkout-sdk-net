using System;
using Checkout.Common;

namespace Checkout.Balances
{
    /// <summary>
    /// The balances held by a single currency account (sub-account).
    /// </summary>
    public class CurrencyAccountBalance
    {
        /// <summary>
        /// The unique identifier of the currency account (sub-account).
        /// Returned only when the request is made with withCurrencyAccountId=true.
        /// [Optional]
        /// </summary>
        public string CurrencyAccountId { get; set; }

        /// <summary>
        /// A descriptor for the currency account.
        /// [Optional]
        /// </summary>
        public string Descriptor { get; set; }

        /// <summary>
        /// The holding currency of the currency account (the three character ISO 4217 code).
        /// [Optional]
        /// </summary>
        public Currency? HoldingCurrency { get; set; }

        /// <summary>
        /// The balance values for the currency account.
        /// [Optional]
        /// </summary>
        public Balances Balances { get; set; }

        /// <summary>
        /// The UTC datetime reflecting when the balance values were fetched. If the request includes a
        /// balancesAt query parameter, this matches that value; otherwise, it is the time the request was processed.
        /// [Optional]
        /// Format: date-time (RFC 3339)
        /// </summary>
        public DateTime? BalancesAsOf { get; set; }
    }
}
