using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Accounts.Entities.Response
{
    /// <summary>
    /// The sub-entity's expected processing, as returned by GET /accounts/entities/{id}
    /// (<c>processing_details</c>, Accounts API v3.0).
    /// A response-only type, separate from the request's <see cref="Request.ProcessingDetails"/>: the
    /// amounts are <c>long</c> here because the API declares them as integers in minor units with no
    /// maximum, and an <c>int</c> would fail to read any value above 2,147,483,647.
    /// </summary>
    public class EntityProcessingDetails
    {
        /// <summary>
        /// The country code (iso-3166-1 alpha-2) where the settlement bank account is located.
        /// Format: iso-3166-1-alpha-2
        /// 2 characters
        /// </summary>
        public string SettlementCountry { get; set; }

        /// <summary>
        /// Target country codes (iso-3166-1 alpha-2) with more than 10% expected volume processing
        /// with Checkout.com.
        /// min 1 item, max 10 items
        /// </summary>
        public IList<string> TargetCountries { get; set; }

        /// <summary>
        /// The estimated annual processing volume. In minor units without decimals.
        /// min 0
        /// </summary>
        public long? AnnualProcessingVolume { get; set; }

        /// <summary>
        /// The expected average transaction value. In minor units without decimals.
        /// min 0
        /// </summary>
        public long? AverageTransactionValue { get; set; }

        /// <summary>
        /// The expected highest transaction value. In minor units without decimals.
        /// min 0
        /// </summary>
        public long? HighestTransactionValue { get; set; }

        /// <summary>
        /// The currency used for the processing details provided.
        /// </summary>
        public Currency? Currency { get; set; }
    }
}
