using Checkout.Common;

namespace Checkout.Issuing.Disputes.Common
{
    /// <summary>
    /// Amount information for an Issuing dispute, including the amount and currency.
    /// [Beta]
    /// </summary>
    public class DisputeAmount
    {
        /// <summary>
        /// The amount is provided in the minor currency unit.
        /// </summary>
        public long? Amount { get; set; }

        /// <summary>
        /// The issuing currency, as a three-letter ISO currency code.
        /// ^[a-zA-Z]{3}$
        /// 3 characters
        /// </summary>
        public Currency? Currency { get; set; }
    }
}