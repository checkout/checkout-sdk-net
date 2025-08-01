using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.Common
{
    public enum AuthenticationCategoryType
    {
        [EnumMember(Value = "payment")]
        Payment,

        [EnumMember(Value = "non_payment")]
        NonPayment,
    }
}