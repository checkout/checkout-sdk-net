using System.Runtime.Serialization;

namespace Checkout.Payments.Contexts
{
    public enum PaymentContextItemType
    {
        [EnumMember(Value = "physical")]
        Physical,
        
        [EnumMember(Value = "digital")]
        Digital,
    }
}