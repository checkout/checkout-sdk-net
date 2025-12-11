using System.Runtime.Serialization;

namespace Checkout.Payments.Request
{
    public enum ItemType
    {
        [EnumMember(Value = "digital")]
        Digital,
        
        [EnumMember(Value = "discount")]
        Discount,
        
        [EnumMember(Value = "physical")]
        Physical,
    }
}