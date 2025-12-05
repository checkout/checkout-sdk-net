namespace Checkout.HandlePaymentsAndPayouts.Flow.Entities
{
    public class AmountAllocation
    {
        /// <summary>
        /// The sub-entity's ID.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The split amount, in minor currency units. 
        /// The sum of all split amounts must be equal to the payment amount. 
        /// The split amount will be credited to your sub-entity's currency account.
        /// </summary>
        public long Amount { get; set; }

        /// <summary>
        /// A reference you can use to identify the split. For example, an order number.
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// The commission you'd like to collect from the split, calculated using the formula: 
        /// commission = (amount * commission.percentage) + commission.amount. 
        /// The commission cannot exceed the split amount. Commission will be credited to your currency account.
        /// </summary>
        public Commission Commission { get; set; }
    }

    public class Commission
    {
        /// <summary>
        /// An optional commission to collect, as a fixed amount in minor currency units.
        /// </summary>
        public long? Amount { get; set; }

        /// <summary>
        /// An optional commission to collect, as a percentage value with up to eight decimal places.
        /// </summary>
        public decimal? Percentage { get; set; }
    }
}