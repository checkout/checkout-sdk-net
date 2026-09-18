namespace Checkout.Inventory.Entities
{
    /// <summary>
    /// A monetary amount used by Inventory product knowledge (price / sale_price). Field names are
    /// intentionally identical to the shared Payments money shape (amount, currency), but this is
    /// its own type: the Inventory <c>InventoryMoney</c> schema is not the Payments <c>Money</c> schema.
    /// </summary>
    public class InventoryMoney
    {
        /// <summary> The amount in the minor currency unit. (Required) </summary>
        public int? Amount { get; set; }

        /// <summary> The 3-letter ISO 4217 currency code. (Required, constraints: length 3) </summary>
        public string Currency { get; set; }
    }
}
