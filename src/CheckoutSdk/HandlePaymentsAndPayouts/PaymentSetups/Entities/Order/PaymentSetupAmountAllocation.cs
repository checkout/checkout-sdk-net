namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// An amount allocation representing a sub-entity on whose behalf the payment is being processed.
    /// </summary>
    public class PaymentSetupAmountAllocation
    {
        /// <summary>
        /// The id of the sub-entity.
        /// [Required]
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The split amount - this will be credited to your sub-entity's currency account. The sum of all
        /// split amounts must be equal to the payment amount. Provided in the minor currency unit.
        /// [Required]
        /// &gt;= 0
        /// &lt;= 9999999999
        /// </summary>
        public long? Amount { get; set; }

        /// <summary>
        /// A reference you can later use to identify this split, such as an order number.
        /// [Optional]
        /// &lt;= 50 characters
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Commission you'd like to collect from this split.
        /// [Optional]
        /// </summary>
        public AmountAllocationCommission Commission { get; set; }
    }
}
