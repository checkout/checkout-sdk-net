using System.Runtime.Serialization;

namespace Checkout.Inventory.Entities
{
    /// <summary> The condition of the product a variant's product knowledge describes. Defaults to <c>new</c>. </summary>
    public enum InventoryProductCondition
    {
        [EnumMember(Value = "new")]
        New,

        [EnumMember(Value = "used")]
        Used,

        [EnumMember(Value = "refurbished")]
        Refurbished
    }
}
