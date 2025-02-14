using System.Runtime.Serialization;

namespace Checkout.Payments
{
    public enum LocaleType
    {
        [EnumMember(Value = "ar")]
        Ar,

        [EnumMember(Value = "da-DK")]
        DaDk,

        [EnumMember(Value = "de-DE")]
        DeDe,

        [EnumMember(Value = "el")]
        El,

        [EnumMember(Value = "en-GB")]
        EnGb,

        [EnumMember(Value = "es-ES")]
        EsEs,

        [EnumMember(Value = "fi-FI")]
        FiFi,

        [EnumMember(Value = "fil-PH")]
        FilPh,

        [EnumMember(Value = "fr-FR")]
        FrFr,

        [EnumMember(Value = "hi-IN")]
        HiIn,

        [EnumMember(Value = "id-ID")]
        IdId,

        [EnumMember(Value = "it-IT")]
        ItIt,

        [EnumMember(Value = "ja-JP")]
        JaJp,

        [EnumMember(Value = "ms-MY")]
        MsMy,

        [EnumMember(Value = "nb-NO")]
        NbNo,

        [EnumMember(Value = "nl-NL")]
        NlNl,

        [EnumMember(Value = "pt-PT")]
        PtPt,

        [EnumMember(Value = "sv-SE")]
        SvSe,

        [EnumMember(Value = "th-TH")]
        ThTh,

        [EnumMember(Value = "vi-VN")]
        ViVn,

        [EnumMember(Value = "zh-CN")]
        ZhCn,

        [EnumMember(Value = "zh-HK")]
        ZhHk,

        [EnumMember(Value = "zh-TW")]
        ZhTw
    }
}