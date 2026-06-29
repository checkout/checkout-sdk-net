using System.Runtime.Serialization;

namespace Checkout.Payments.Request
{
    /// <summary>
    /// The item type. Used to provide item level metadata where applicable.
    /// If source.type is sequra, this field is required.
    /// </summary>
    public enum ItemType
    {
        /// <summary>A digital or downloadable item.</summary>
        [EnumMember(Value = "digital")]
        Digital,

        /// <summary>A discount line item.</summary>
        [EnumMember(Value = "discount")]
        Discount,

        /// <summary>A physical item that requires shipping.</summary>
        [EnumMember(Value = "physical")]
        Physical,
    }
}