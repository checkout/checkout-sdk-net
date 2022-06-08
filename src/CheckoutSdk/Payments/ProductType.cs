using System.Runtime.Serialization;

namespace Checkout.Payments
{
    public enum ProductType
    {
        [EnumMember(Value = "QR Code")] QrCode,
        [EnumMember(Value = "In-App")] InApp,
        [EnumMember(Value = "Official Account")]OfficialAccount,
        [EnumMember(Value = "Mini Program")] MiniProgram
    }
}