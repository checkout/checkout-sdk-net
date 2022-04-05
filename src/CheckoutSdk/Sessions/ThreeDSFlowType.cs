using System.Runtime.Serialization;

namespace Checkout.Sessions
{
    public enum ThreeDSFlowType
    {
        [EnumMember(Value = "challenged")] Challenged,

        [EnumMember(Value = "frictionless")] Frictionless,

        [EnumMember(Value = "frictionless_delegated")]
        FrictionlessDelegated,
    }
}