namespace Checkout.Inventory.Requests
{
    /// <summary> Request body for adjusting a variant's on-hand stock. </summary>
    public class InventoryAdjustmentRequest
    {
        /// <summary>
        /// Identifier of the variant to adjust. Must already exist. (Required, constraints: maxLength 128)
        /// </summary>
        public string VariantId { get; set; }

        /// <summary>
        /// The signed change to apply to on_hand. A negative delta that would drive on_hand below
        /// zero is rejected with a 409. (Required, non-zero)
        /// </summary>
        public int? Delta { get; set; }

        /// <summary>
        /// Free-text reason recorded in the ledger. Must not contain personal data.
        /// (Required, constraints: minLength 1, maxLength 256)
        /// </summary>
        public string Reason { get; set; }
    }
}
