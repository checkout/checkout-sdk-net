namespace Checkout.Inventory.Requests
{
    /// <summary> Request body for creating or updating a variant's inventory levels. </summary>
    public class InventorySetLevelsRequest
    {
        /// <summary> The physical stock quantity. (Required, constraints: minimum 0) </summary>
        public int? OnHand { get; set; }

        /// <summary>
        /// Buffer withheld from sale. Defaults to 0 on create; left unchanged on update if omitted.
        /// (Optional, constraints: minimum 0)
        /// </summary>
        public int? SafetyStock { get; set; }

        /// <summary>
        /// Free-text reason recorded in the ledger. Must not contain personal data.
        /// (Optional, constraints: maxLength 256)
        /// </summary>
        public string Reason { get; set; }
    }
}
