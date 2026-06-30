namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// Commission you'd like to collect from this split - this will be credited to your currency account.
    /// The commission cannot exceed the split amount.
    /// Commission = (amount * commission.percentage) + commission.amount
    /// </summary>
    public class AmountAllocationCommission
    {
        /// <summary>
        /// Optional fixed amount of commission to collect, in the minor currency unit.
        /// [Optional]
        /// &gt;= 0
        /// </summary>
        public long? Amount { get; set; }

        /// <summary>
        /// Optional percentage of commission to collect. Supports up to 8 decimal places.
        /// [Optional]
        /// &gt;= 0
        /// &lt;= 100
        /// </summary>
        public double? Percentage { get; set; }
    }
}
