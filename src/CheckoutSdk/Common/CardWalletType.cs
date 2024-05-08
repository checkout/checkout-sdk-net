using System.Runtime.Serialization;

namespace Checkout.Common
{
    public enum CardWalletType
    {
        [EnumMember(Value = "applepay")] Applepay,
        [EnumMember(Value = "googlepay")] Googlepay,
    }
}