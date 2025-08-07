using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.Common.Responses
{
    public enum FlowType
    {
        [EnumMember(Value = "challenged")]
        Challenged,

        [EnumMember(Value = "frictionless")]
        Frictionless,

        [EnumMember(Value = "frictionless_delegated")]
        FrictionlessDelegated,
    }
}