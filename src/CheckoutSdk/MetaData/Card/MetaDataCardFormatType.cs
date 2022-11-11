using System.Runtime.Serialization;

namespace Checkout.MetaData.Card
{
    public enum MetaDataCardFormatType
    {
        [EnumMember(Value = "Basic")] Basic,
        [EnumMember(Value = "Card Payouts")] CardPayouts,
    }
}