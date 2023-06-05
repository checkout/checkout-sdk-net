using System.Runtime.Serialization;

namespace Checkout.Issuing.Testing.Requests
{
    public enum TransactionAuthorizationType
    {
        [EnumMember(Value = "authorization")] Authorization,

        [EnumMember(Value = "pre_authorization")]
        PreAuthorization
    }
}