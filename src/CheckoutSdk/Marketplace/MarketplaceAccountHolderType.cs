using System.Runtime.Serialization;

namespace Checkout.Marketplace
{
    public enum MarketplaceAccountHolderType
    {
        [EnumMember(Value = "individual")]
        Individual,

        [EnumMember(Value = "corporate")]
        Corporate,

        [EnumMember(Value = "government")]
        Government
    }
}