using Checkout.Common;
using System;

namespace Checkout.Accounts.Entities.Common.Company
{
    /// <summary>
    /// Seller financial questions (<c>financial_details</c>): on the company of EEA and US Company Full
    /// and Lite (2.0), and on the individual of US Sole Trader Full and Lite (2.0).
    /// </summary>
    public class FinancialDetails
    {
        /// <summary>
        /// The estimated annual processing volume. In minor units without decimals.
        /// [Required] on the Full (2.0) variants; [Optional] on the Lite (2.0) variants.
        /// min 0
        /// </summary>
        public long? AnnualProcessingVolume { get; set; }

        /// <summary>
        /// The expected average transaction value. In minor units without decimals.
        /// [Required] on the Full (2.0) variants; [Optional] on the Lite (2.0) variants.
        /// min 0
        /// </summary>
        public long? AverageTransactionValue { get; set; }

        /// <summary>
        /// The expected highest transaction value. In minor units without decimals.
        /// [Required] on the Full (2.0) variants; [Optional] on the Lite (2.0) variants.
        /// min 0
        /// </summary>
        public long? HighestTransactionValue { get; set; }

        /// <summary>
        /// The currency used for the financial details provided.
        /// [Required] on US Company Full and US Sole Trader Full (2.0); [Optional] on the other variants.
        /// </summary>
        public Currency? Currency { get; set; }

        /// <summary>
        /// Not defined by any Accounts API schema; the API does not read it. Supporting documents go on
        /// the top-level request documents instead. Retained so existing code keeps compiling.
        /// </summary>
        [Obsolete("Not defined by any Accounts API schema: financial_details carries the three amounts and the currency only. Will be removed in a future major version.")]
        public FinancialDocuments Documents { get; set; }
    }
}