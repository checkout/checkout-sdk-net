using System.Runtime.Serialization;

namespace Checkout.Inventory.Entities
{
    /// <summary> The stock state of an inventory level. </summary>
    public enum InventoryLevelState
    {
        [EnumMember(Value = "in_stock")]
        InStock,

        [EnumMember(Value = "limited")]
        Limited,

        [EnumMember(Value = "out_of_stock")]
        OutOfStock
    }
}
