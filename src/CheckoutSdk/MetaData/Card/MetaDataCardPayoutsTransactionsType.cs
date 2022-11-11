using System.Runtime.Serialization;

namespace Checkout.MetaData.Card
{
    public enum CardPayoutsTransferType
    {
        [EnumMember(Value = "Not Supported")] NotSupported,
        [EnumMember(Value = "Standard")] Standard,
        [EnumMember(Value = "Fast Founds")] FastFounds,
        [EnumMember(Value = "Unknown")] Unknown,
    }
}