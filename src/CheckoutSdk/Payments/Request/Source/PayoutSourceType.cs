using System.Runtime.Serialization;

namespace Checkout.Payments.Request.Source
{
    public enum PayoutSourceType
    {
        [EnumMember(Value = "currency_account")]
        CurrencyAccount,
    }
}