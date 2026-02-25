using System.Runtime.Serialization;

namespace Checkout.Financial
{
    public enum Region
    {
        [EnumMember(Value = "Domestic")] Domestic,
        [EnumMember(Value = "Intra")] Intra,
        [EnumMember(Value = "IntraEEA")] IntraEea,
        [EnumMember(Value = "IntraEuropean_SEPA")] IntraEuropeanSepa,
        [EnumMember(Value = "International")] International,
        [EnumMember(Value = "EEA")] Eea,
    }
}
