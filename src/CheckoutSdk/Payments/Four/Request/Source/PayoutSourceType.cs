using System.Runtime.Serialization;

namespace Checkout.Payments.Four.Request.Source
{
    public enum PayoutSourceType
    {
        [EnumMember(Value = "currency_account")]
        CurrencyAccount,
    }
}