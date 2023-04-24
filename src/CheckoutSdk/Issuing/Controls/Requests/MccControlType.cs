using System.Runtime.Serialization;

namespace Checkout.Issuing.Controls.Requests
{
    public enum MccControlType
    {
        [EnumMember(Value = "allow")] Allow,
        [EnumMember(Value = "block")] Block,
    }
}