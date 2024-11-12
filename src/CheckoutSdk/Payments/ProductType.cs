using System.Runtime.Serialization;

namespace Checkout.Payments
{
    public enum ProductType
    {
        [EnumMember(Value = "QR Code")]
        QrCode,

        [EnumMember(Value = "In-App")]
        InApp,

        [EnumMember(Value = "Official Account")]
        OfficialAccount,

        [EnumMember(Value = "Mini Program")]
        MiniProgram,

        [EnumMember(Value = "pay_in_full")]
        PayInFull,

        [EnumMember(Value = "pay_by_instalment")]
        PayByInstalment,

        [EnumMember(Value = "pay_by_instalment_2")]
        PayByInstalment2,

        [EnumMember(Value = "pay_by_instalment_3")]
        PayByInstalment3,

        [EnumMember(Value = "pay_by_instalment_4")]
        PayByInstalment4,

        [EnumMember(Value = "pay_by_instalment_6")]
        PayByInstalment6,

        [EnumMember(Value = "invoice")]
        Invoice,

        [EnumMember(Value = "pay_later")]
        PayLater
    }
}