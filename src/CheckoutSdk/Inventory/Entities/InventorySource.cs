using System.Runtime.Serialization;

namespace Checkout.Inventory.Entities
{
    /// <summary> Where an inventory level is managed. </summary>
    public enum InventorySource
    {
        [EnumMember(Value = "managed")]
        Managed,

        [EnumMember(Value = "sync")]
        Sync
    }
}
