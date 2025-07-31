using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.Common.Responses.Card.Metadata
{
    public enum CardCategoryType
    {
        [EnumMember(Value = "CONSUMER")]
        CONSUMER,

        [EnumMember(Value = "COMMERCIAL")]
        COMMERCIAL,
    }
}