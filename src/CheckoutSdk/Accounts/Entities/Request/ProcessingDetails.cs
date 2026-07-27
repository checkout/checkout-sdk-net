using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Accounts.Entities.Request
{
    public class ProcessingDetails
    {
        /// <summary>
        /// The country code (ISO 3166-1 alpha-2) where the settlement bank account is located.
        /// </summary>
        public string SettlementCountry { get; set; }

        /// <summary>
        /// The list of two-letter ISO 3166-1 alpha-2 target country codes with more than 10% expected
        /// volume processing with Checkout.com.
        /// </summary>
        public IList<string> TargetCountries { get; set; }

        /// <summary>
        /// The estimated annual processing volume in minor units without decimals.
        /// </summary>
        public int? AnnualProcessingVolume { get; set; }

        /// <summary>
        /// The expected average transaction value in minor units without decimals.
        /// </summary>
        public int? AverageTransactionValue { get; set; }

        /// <summary>
        /// The average time in days between accepting payment and fulfilling the order.
        /// </summary>
        public int? AverageOrderFulfillmentTime { get; set; }

        /// <summary>
        /// The expected highest transaction value in minor units without decimals.
        /// </summary>
        public int? HighestTransactionValue { get; set; }

        /// <summary>
        /// The currency used for the processing details provided.
        /// </summary>
        public Currency? Currency { get; set; }

        /// <summary>
        /// Payment method-specific processing details.
        /// </summary>
        public ProcessingDetailsPayments Payments { get; set; }
    }
}
