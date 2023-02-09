using System.Runtime.Serialization;

namespace Checkout.Financial
{
    public enum Region
    {
        [EnumMember(Value = "Domestic")] Domestic,
        [EnumMember(Value = "EEA")] Eea,
        [EnumMember(Value = "International")] International
    }
}
