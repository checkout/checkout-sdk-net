using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Issuing.Disputes
{
    /// <summary>
    /// The details of the merchant you raised the dispute with.
    /// [Beta]
    /// </summary>
    public class DisputeMerchant
    {
        /// <summary>
        /// The merchant's identifier. This can vary from one acquirer to another.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The merchant's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The city where the merchant is located.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The state where the merchant is located (US only).
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// The two-digit country code where the merchant is located.
        /// </summary>
        public CountryCode? CountryCode { get; set; }

        /// <summary>
        /// The merchant's category code for the disputed transaction.
        /// </summary>
        public string CategoryCode { get; set; }

        /// <summary>
        /// Any evidence submitted by the merchant during the dispute lifecycle.
        /// </summary>
        public IList<string> Evidence { get; set; }
    }
}