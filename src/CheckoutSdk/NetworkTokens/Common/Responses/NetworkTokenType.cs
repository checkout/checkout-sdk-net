using System.Runtime.Serialization;

namespace Checkout.NetworkTokens.Common.Responses
{
    public enum NetworkTokenType
    {
        [EnumMember(Value = "vts")] Vts,

        [EnumMember(Value = "mdes")] Mdes,

    }
}
