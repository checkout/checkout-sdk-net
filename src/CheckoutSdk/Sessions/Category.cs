using System.Runtime.Serialization;

namespace Checkout.Sessions
{
    public enum Category
    {
        [EnumMember(Value = "payment")] Payment,
        [EnumMember(Value = "non_payment")] NonPayment
    }
}