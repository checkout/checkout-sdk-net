using System.Runtime.Serialization;

namespace Checkout.Payments
{
    public enum FundTransferType
    {
        [EnumMember(Value = "AA")] AA,
        [EnumMember(Value = "PP")] PP,
        [EnumMember(Value = "FT")] FT,
        [EnumMember(Value = "FD")] FD,
        [EnumMember(Value = "PD")] PD,
        [EnumMember(Value = "LO")] LO,
        [EnumMember(Value = "OG")] OG,
    }
}