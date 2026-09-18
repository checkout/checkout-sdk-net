using System.Runtime.Serialization;

namespace Checkout.Inventory.Entities
{
    /// <summary>
    /// The state of an inventory reservation. A reservation still in the <c>held</c> state that has
    /// passed its <c>expires_at</c> reports as <c>expired</c>.
    /// </summary>
    public enum InventoryReservationState
    {
        [EnumMember(Value = "held")]
        Held,

        [EnumMember(Value = "committed")]
        Committed,

        [EnumMember(Value = "released")]
        Released,

        [EnumMember(Value = "expired")]
        Expired
    }
}
