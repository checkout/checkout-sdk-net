using System.Runtime.Serialization;

namespace Checkout.Payments.Previous.Request.Source
{
    public enum NetworkTokenType
    {
        [EnumMember(Value = "vts")] Vts,
        [EnumMember(Value = "mdes")] Mdes,
        [EnumMember(Value = "applepay")] ApplePay,
        [EnumMember(Value = "googlepay")] GooglePay
    }
}