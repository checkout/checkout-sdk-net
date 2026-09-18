namespace Checkout.Inventory.Entities
{
    /// <summary>
    /// A single variant/quantity line within an inventory reservation. Shared between
    /// <see cref="Checkout.Inventory.Requests.InventoryReservationRequest"/> and
    /// <see cref="Checkout.Inventory.Responses.InventoryReservation"/>.
    /// </summary>
    public class InventoryReservationItem
    {
        /// <summary>
        /// Identifier of the variant to reserve. Must already exist. (Required, constraints: maxLength 128)
        /// </summary>
        public string VariantId { get; set; }

        /// <summary> Quantity of the variant to reserve. (Required, constraints: minimum 1) </summary>
        public int? Quantity { get; set; }
    }
}
